using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using SCParking.Domain.Interfaces;
using SCParking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using SCParking.Domain.Common;
using SCParking.Domain.SmartCitiesModels;


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

        public async Task<List<ParkingSpot>> GetEntities(string token)
        {
            try
            {
                var urlAuthentication = _settings.Where(x => x.SettingCode == Constants.Setting_URLSCENTITY).FirstOrDefault().Value;
                List<ParkingSpot> entity = new List<ParkingSpot>();
                var client = new RestClient(urlAuthentication + "/entities");
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
                    entity = ParkingSpots.FromJson(bodyResponse);

                    return entity;
                }

                return null;




            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }


        

    }
}
