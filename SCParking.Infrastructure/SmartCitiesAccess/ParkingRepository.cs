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
using Newtonsoft.Json;
using SCParking.Domain.Common;
using SCParking.Domain.SmartCitiesModels;
using SCParking.Domain.Views.DTOs;
using TimeInstant = SCParking.Domain.Views.DTOs.TimeInstant;
using Location = SCParking.Domain.Views.DTOs.Location;
using Occupied = SCParking.Domain.Views.DTOs.Occupied;
using Metadata = SCParking.Domain.Views.DTOs.Metadata;

namespace SCParking.Infrastructure.SmartCitiesAccess
{
    public class ParkingRepository : IParkingRepository
    {

        
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private ISettingRepository _settingRepository;
        private ICollection<Setting> _settings;
        private readonly ISmartCityRepository _smartCityRepository;

        private readonly IParkingReservationRepository _parkingReservationRepository;

        public ParkingRepository(IConfiguration configuration, ILogger<ParkingRepository> logger, ISettingRepository settingRepository,ISmartCityRepository smartCityRepository,IParkingReservationRepository parkingReservationRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _settingRepository = settingRepository;
            _settings = _settingRepository.Get();
            _smartCityRepository = smartCityRepository;
            _parkingReservationRepository = parkingReservationRepository;
        }
       


        

        public async Task<List<ParkingSpotResponseDto>> GetByStatus(string status, string placeType,string token)
        {
            try
            {

                var parkingSpotResponse = await _smartCityRepository.GetEntities(token);
                if (!string.IsNullOrEmpty(status) && !Constants.PARKING_STATUS_RESERVED.Equals(status))//Si debo obtener los free o occupied
                {
                    //Excluir los reservados
                    parkingSpotResponse = await  ExcludeParkingSlotsReserved(parkingSpotResponse);
                }
                else if (!string.IsNullOrEmpty(status))
                {
                    //Status == reserved
                    return await SetResponseParkingSpotToParkingSpotResponseDto(await GetReservations(parkingSpotResponse));
                }
                _logger.Log(LogLevel.Information,"Cantidad de registros {0}",parkingSpotResponse.Count);
                if (!string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(placeType))
                {
                    _logger.Log(LogLevel.Information,"Obtenemos los datos status {0} placeType {1}",status,placeType);
                    var resp =  parkingSpotResponse.Where(x =>
                        x.Status != null && x.Status.Value.ToString().Equals(status) && placeType != null &&
                        x.Name.Value.ToString().Contains(placeType)).ToList();
                    _logger.Log(LogLevel.Information, "Cantidad de registros {0}", resp.Count);
                    return await SetResponseParkingSpotToParkingSpotResponseDto(resp);
                }

                if (!string.IsNullOrEmpty(status))
                {
                    var resp =  parkingSpotResponse.Where(x => x.Status != null && x.Status.Value.ToString().Equals(status)).ToList();
                    _logger.Log(LogLevel.Information, "Cantidad de registros {0}", resp.Count);
                    //excluimos los parkings reservados solo si lo que se pide es status free
                    return await SetResponseParkingSpotToParkingSpotResponseDto(resp);
                }

                if (string.IsNullOrEmpty(status))
                {
                    parkingSpotResponse = await SetParkingReserved(parkingSpotResponse);
                }
                return await SetResponseParkingSpotToParkingSpotResponseDto(parkingSpotResponse);
                
                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        private async Task<List<ParkingSpot>> SetParkingReserved(List<ParkingSpot> parkingSpotResponse)
        {
            var parkingReserve = await _parkingReservationRepository.GetParkingSlotReserved();
            if (parkingReserve != null)
            {
                foreach (var parkingReservation in parkingReserve)
                {
                    var parkingSlot = parkingSpotResponse.Find(x => x.Id == parkingReservation.ParkingSpotId);
                    if (parkingSlot != null)
                    {
                        parkingSlot.Status.Value = Constants.PARKING_STATUS_RESERVED;
                        parkingSlot.Occupied.Value = 3;
                        
                    }

                }
            }

            return parkingSpotResponse;
            
        }

        private async Task<List<ParkingSpot>> GetReservations(List<ParkingSpot> parkingSpotResponse)
        {
            List<ParkingSpot> resp = new List<ParkingSpot>();
            var parkingReserve = await _parkingReservationRepository.GetParkingSlotReserved();
            if (parkingReserve != null)
            {
                foreach (var parkingReservation in parkingReserve)
                {
                    var parkingSlot = parkingSpotResponse.Find(x => x.Id == parkingReservation.ParkingSpotId);
                    if (parkingSlot != null)
                    {
                        parkingSlot.Status.Value = Constants.PARKING_STATUS_RESERVED;
                        parkingSlot.Occupied.Value = 3;
                        resp.Add(parkingSlot);
                    }

                }
            }

            return resp;
        }

        private async Task<List<ParkingSpot>> ExcludeParkingSlotsReserved(List<ParkingSpot> parkingSpotResponse)
        {
            var parkingReserve = await _parkingReservationRepository.GetParkingSlotReserved();
            if (parkingReserve != null)
            {
                foreach (var parkingReservation in parkingReserve)
                {
                    var parkingSlot = parkingSpotResponse.Find(x => x.Id == parkingReservation.ParkingSpotId);
                    if (parkingSlot != null)
                    {
                        parkingSpotResponse.Remove(parkingSlot);
                    }

                }
            }

            return parkingSpotResponse;
        }

        public async Task<List<ParkingSpotResponseDto>> SetResponseParkingSpotToParkingSpotResponseDto(List<ParkingSpot> parkingSpots)
        {
            List<ParkingSpotResponseDto> respuesta = new List<ParkingSpotResponseDto>();
            foreach (var reg in parkingSpots)
            {
                if (reg.Category != null && reg.Type == "ParkingSpot")
                {
                    
                    respuesta.Add(await ParkingSpotToParkingSpotResponseDto(reg));
                }
                 
            }
            return respuesta;
        }

        public async Task<ParkingSpotResponseDto> ParkingSpotToParkingSpotResponseDto(ParkingSpot parkingSpot)
        {
            _logger.LogInformation("Objeto a convertir "+JsonConvert.SerializeObject(parkingSpot));
            ParkingSpotResponseDto parkingSpotResponse = new ParkingSpotResponseDto();
            parkingSpotResponse.Category = new List<string>();
            parkingSpotResponse.Category.Add(parkingSpot.Category.Value);
            parkingSpotResponse.Status = parkingSpot.Status.Value;
            parkingSpotResponse.Id = parkingSpot.Id;
            parkingSpotResponse.Location = new Domain.Views.DTOs.Location();
            parkingSpotResponse.Location.Coordinates = parkingSpot.Location.Value.Coordinates;
            parkingSpotResponse.Name = parkingSpot.Name.Value;
            parkingSpotResponse.RefParkingSite = parkingSpot.RefOnStreetParking.Value;
            if (Constants.PARKING_STATUS_RESERVED.Equals(parkingSpotResponse.Status))
            {
                //Si está reservado agrego token usuario y fecha de reserva
                var parkingReserved = await _parkingReservationRepository.GetByParkingId(parkingSpotResponse.Id);
                if (parkingReserved != null)
                {
                    parkingSpotResponse.reservationStartAt = parkingReserved.StartAt.ToString("dd/MM/yyyy HH:mm:ss");
                    parkingSpotResponse.reservationEndAt = parkingReserved.EndAt.ToString("dd/MM/yyyy HH:mm:ss");
                    parkingSpotResponse.tokenUsuario = parkingReserved.UserToken;

                }
            }
            return parkingSpotResponse;

        }
        public async Task<ParkingSpot> GetById(string id,string token)
        {
            try
            {
                var urlAuthentication = _settings.Where(x => x.SettingCode == Constants.Setting_URLSCENTITY).FirstOrDefault().Value;
                List<ParkingSpot> entity = new List<ParkingSpot>();
                var client = new RestClient(urlAuthentication + "/entities");
                client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);

                request.AddParameter("id", id);
                request.AddHeader("Fiware-service", "murcia");
                request.AddHeader("Fiware-servicepath", "/aparcamiento");
                request.AddHeader("X-Auth-Token", token);
                IRestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var bodyResponse = response.Content;
                    entity = ParkingSpots.FromJson(bodyResponse);


                }

                var parkingSpot = entity[0];
                if (parkingSpot != null)
                {
                    var parkingReserve = await _parkingReservationRepository.GetParkingSlotReserved();
                    if (parkingReserve != null)
                    {
                        foreach (var parkingReservation in parkingReserve)
                        {
                            
                            if (parkingSpot.Id == parkingReservation.ParkingSpotId)
                            {
                                parkingSpot.Status.Value = Constants.PARKING_STATUS_RESERVED;
                                parkingSpot.Occupied.Value = 3;
                                return parkingSpot;
                            }

                        }
                    }
                    return parkingSpot;
                }

                return null;

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<ParkingSpot> ChangeStatus(string id, ParkingSpot parkingSpotData,string token)
        {
            try
            {
                var urlAuthentication = _settings.Where(x => x.SettingCode == Constants.Setting_URLSCENTITY).FirstOrDefault().Value;
                List<ParkingSpot> entity = new List<ParkingSpot>();
                var client = new RestClient(urlAuthentication + "/entities?options=upsert");
                Console.WriteLine(JsonConvert.SerializeObject( parkingSpotData));
                
                client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);

                JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
                serializerSettings.NullValueHandling = NullValueHandling.Ignore;

                request.AddHeader("Fiware-service", "murcia");
                request.AddHeader("Fiware-servicepath", "/aparcamiento");
                request.AddHeader("X-Auth-Token", token);
                var requestBody = await SetValuesForUpdate(parkingSpotData);
                
                request.AddJsonBody(JsonConvert.SerializeObject(requestBody, serializerSettings));
                Console.WriteLine(JsonConvert.SerializeObject(requestBody,serializerSettings));
                IRestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {

                    return await GetById(parkingSpotData.Id, token);


                }

                

                return null;

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        private async Task<ParkingSpotUpdateDto> SetValuesForUpdate(ParkingSpot parkingSpotData)
        {
            ParkingSpotUpdateDto parkingSpotUpdate = new ParkingSpotUpdateDto();
            parkingSpotUpdate.Id = parkingSpotData.Id;
            parkingSpotUpdate.Type = parkingSpotData.Type;
            parkingSpotUpdate.TimeInstant = new TimeInstant()
            {
                Type = "DateTime",
                Value = parkingSpotData.TimeInstant.Value,
                Metadata = new Metadata()
                

            };
            
            parkingSpotUpdate.OccupancyModified = new TimeInstant()
            {
                Type = "DateTime",
                Value = parkingSpotData.OccupancyModified.Value,
                Metadata = new Metadata()
            };
            parkingSpotUpdate.Occupied = new Occupied()
            {
                Value = parkingSpotData.Occupied.Value,
                Type = "Number",
                Metadata = new Metadata()
            };

           
            parkingSpotUpdate.Status = new TimeInstant()
            {
                Type = "Text",
                Value = parkingSpotData.Status.Value,
                Metadata = new Metadata()
            };


            return parkingSpotUpdate;

        }
    }


}
