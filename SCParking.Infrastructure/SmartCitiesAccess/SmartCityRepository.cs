using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using SCParking.Domain.Interfaces;
using SCParking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Linq;
using SCParking.Domain.Common;
using SCParking.Domain.SmartCitiesModels;
using Newtonsoft.Json;
using RestSharp.Serialization.Json;
using SCParking.Domain.Views.DTOs;

namespace SCParking.Infrastructure.SmartCitiesAccess
{
    public class SmartCityRepository: ISmartCityRepository
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private ISettingRepository _settingRepository;
        private ICollection<Setting> _settings;

        public SmartCityRepository(IConfiguration configuration, ILogger<SmartCityRepository> logger, ISettingRepository settingRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _settingRepository = settingRepository;
            _settings = _settingRepository.Get();
        }

        public async Task<string> Authenticate(string service, string subservice, string user, string password)
        {
            try
            {

                var urlAuthentication = _settings.Where(x => x.SettingCode == Constants.Setting_URLSCAUTH).FirstOrDefault().Value;

                var client = new RestClient(urlAuthentication + "/auth/tokens");
                client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                var authenticationBody = new Authentication();
                authenticationBody.auth = new Auth();
                authenticationBody.auth.identity = new Identity();
                authenticationBody.auth.identity.methods = new List<string>();
                authenticationBody.auth.identity.methods.Add("password");
                authenticationBody.auth.identity.password = new Password();
                authenticationBody.auth.identity.password.user = new Domain.SmartCitiesModels.User();
                authenticationBody.auth.identity.password.user.domain = new Domain.SmartCitiesModels.Domain();
                authenticationBody.auth.identity.password.user.domain.name = service;
                authenticationBody.auth.identity.password.user.name = user;
                authenticationBody.auth.identity.password.user.password = password;
                authenticationBody.auth.scope = new Scope();
                authenticationBody.auth.scope.project = new Project();
                authenticationBody.auth.scope.project.domain = new Domain.SmartCitiesModels.Domain();
                authenticationBody.auth.scope.project.domain.name = service;
                authenticationBody.auth.scope.project.name = subservice;

                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(authenticationBody);
                IRestResponse response = await client.ExecuteAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var tokenHeader = response.Headers.Where(x => x.Name == "X-Subject-Token").FirstOrDefault();
                    if(tokenHeader != null)
                    {
                     return tokenHeader.Value.ToString();
                    }
                }
                return string.Empty;

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return string.Empty;
            }          
            
        }

        public async Task SendBatchEntities(string token,BiciEntityRequestDto biciEntityRequestDto)
        {
            try
            {
                var urlAuthentication = _settings.Where(x => x.SettingCode == Constants.Setting_URLSCENTITY).FirstOrDefault().Value;
                List<ParkingSpot> entity = new List<ParkingSpot>();
                var client = new RestClient(urlAuthentication + "/op/update");
                client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);

                request.AddHeader("Fiware-service", "murcia");
                request.AddHeader("Fiware-servicepath", "/bicicletas");
                request.AddHeader("X-Auth-Token", token);
                request.AddHeader("Content-Type", "application/json");
                request.JsonSerializer = new RestSharp.Serialization.Json.JsonSerializer();
                
                request.JsonSerializer.ContentType = "application/json; charset=utf-8";
                request.AddJsonBody(biciEntityRequestDto);
                _logger.LogInformation(JsonConvert.SerializeObject(biciEntityRequestDto));
                

                IRestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    var bodyResponse = response.Content;
                    _logger.LogInformation(response.ToString());
                }

                




            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                
            }
        }
        public async Task<List<ParkingSpot>> GetEntities(string token)
        {
            try
            {
                var urlAuthentication = _settings.Where(x => x.SettingCode == Constants.Setting_URLSCENTITY).FirstOrDefault().Value;
                _logger.LogInformation($"Url obtener parkings {urlAuthentication}");
                List<ParkingSpot> entity = new List<ParkingSpot>();
                var client = new RestClient(urlAuthentication + "/entities?options=count&limit=1000");
                client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);

                request.AddHeader("Fiware-service", "murcia");
                request.AddHeader("Fiware-servicepath", "/aparcamiento");
                request.AddHeader("X-Auth-Token", token);
                IRestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var bodyResponse = response.Content;
                    _logger.LogInformation(bodyResponse);
                    entity = ParkingSpots.FromJson(bodyResponse);
                    _logger.LogInformation(entity.Count.ToString());
                    return entity;
                }
                else
                {
                    _logger.LogInformation("Status de la invocación a telefónica "+response.StatusCode.ToString()+response.ErrorMessage); 
                }

                return null;




            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<ICollection<BiciEntityDto>> GetMuyBicisEntity()
        {
            try
            {
                var urlAuthentication = _settings.Where(x => x.SettingCode == Constants.Setting_URL_MUYBICI_ENTITY).FirstOrDefault().Value;
                List<MuyBici> entity = new List<MuyBici>();
                var client = new RestClient(urlAuthentication );
                client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);

                
                IRestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var bodyResponse = response.Content;
                    entity = JsonConvert.DeserializeObject<MuyBiciResponseDto>(bodyResponse).data;
                    await CreateOrUpdateBiciEntity(entity);
                    return await MuyBiciToMuyBiciDto(entity);
                }

                return null;




            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        private  async Task CreateOrUpdateBiciEntity(List<MuyBici> entity)
        {
            
        }

        private async Task<ICollection<BiciEntityDto>> MuyBiciToMuyBiciDto(List<MuyBici> entity)
        {
            List<BiciEntityDto> lista = new List<BiciEntityDto>();
            foreach(var muyBici in entity)
            {
                DateTime fechaHoraActual = DateTime.Now.ToUniversalTime();
                lista.Add(new BiciEntityDto()
                {
                    id = muyBici.IdAparcamiento.ToString(),
                    type = "BicycleParking",
                    TimeInstant = new TimeInstantBici()
                    {
                        type = "DateTime",
                        value = DateTime.Now.ToString(@"yyyy-MM-dd\THH:mm:ss.fff\Z")
                    },
                    availableSpot = new Spot()
                    {
                        value = muyBici.Libres,
                        type = "Number"
                    },
                        
                    description = new TimeInstantBici()
                    {
                        type = "Text",
                        value = muyBici.Descripcion,
                    },
                        
                    location = new LocationBici()
                    {
                        type = "geo:json",
                        value = new ValueLocationBici()
                        {
                            type = "Point",
                            coordinates = new List<double>()
                            {
                                muyBici.Longitude,
                                muyBici.Latitude
                            }
                        }
                        
                    },
                    municipality = new TimeInstantBici()
                    {
                        value = "NA",
                        type = "Text"
                    },
                        
                    occupiedSpot = new Spot()
                    {
                        type = "Number",
                        value = muyBici.Ocupados,
                    },
                    shortName = new TimeInstantBici()
                    {
                        type = "Text",
                        value = muyBici.Nombrecorto,
                    },
                    status = new TimeInstantBici()
                    {
                        value = muyBici.Eshabilitada==1? "free":"occupied",
                        type = "Text"
                    },
                    totalSpot = new Spot()
                    {
                        type = "Number",
                        value = muyBici.NumPuestos
                    }

                }) ;
            }
            return lista;
        }
    }
}
