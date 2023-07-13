using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SCParking.Core.Interfaces;
using SCParking.Domain.Common;
using SCParking.Domain.Interfaces;
using SCParking.Domain.Messages;
using SCParking.Domain.Models;
using SCParking.Domain.SmartCitiesModels;
using SCParking.Domain.Validation;
using SCParking.Domain.Views.DTOs;

namespace SCParking.Core.Services
{
    public class ParkingService : IParkingService
    {
        private readonly IHelpers _helpers;
        private readonly ILogger _logger;
        private readonly IParkingRepository _parkingRepository;
        private readonly ISmartCityRepository _smartCityRepository;
        private readonly ISettingRepository _settingRepository;
        private readonly IParkingReservationRepository _parkingReservationRepository;
        private readonly ICollection<Setting> _settings;
        private readonly IParkingReservationService _parkingReservationService;
       
        
        public ParkingService(ILogger<RateService> logger,
            IHelpers helpers, IParkingRepository parkingRepository, ISmartCityRepository smartCityRepository, ISettingRepository settingRepository,
            IParkingReservationRepository parkingReservationRepository, IParkingReservationService parkingReservationService)
        {
            _logger = logger;
            _helpers = helpers;
            _parkingRepository = parkingRepository;
            _smartCityRepository = smartCityRepository;
            _settingRepository = settingRepository;
            _settings = _settingRepository.Get();
            _parkingReservationRepository = parkingReservationRepository;
            _parkingReservationService = parkingReservationService;



        }

        public async Task<List<ParkingSpotResponseDto>> GetbyStatus(string status,string placeType)
        {
            var usrParking = _settings.Where(x => x.SettingCode == Constants.Setting_USRSCSERVICEPARKING).FirstOrDefault().Value;
            var passwordParking = _settings.Where(x => x.SettingCode == Constants.Setting_PASSSCSERVICEPARKING).FirstOrDefault().Value;
            var token = await _smartCityRepository.Authenticate("murcia", "/aparcamiento", usrParking, passwordParking);
            return await _parkingRepository.GetByStatus(status, placeType, token);
            // return null;

        }

        public async Task<ParkingReservation> Reserve(string parkingId, ReservationParkingDto reservationParking)
        {
            
            var error = new ErrorDto();
            dynamic errorField = new ExpandoObject();
            DateTime reservationStartAt = DateTime.ParseExact(reservationParking.reservationStartAt,
                "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            bool isValidStartDate = DateTime.TryParseExact(reservationParking.reservationStartAt, "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
            if (isValidStartDate)
            {
                var usrParking = _settings.Where(x => x.SettingCode == Constants.Setting_USRSCSERVICEPARKING).FirstOrDefault().Value;
                var passwordParking = _settings.Where(x => x.SettingCode == Constants.Setting_PASSSCSERVICEPARKING).FirstOrDefault().Value;
                var token = await _smartCityRepository.Authenticate("murcia", "/aparcamiento", usrParking, passwordParking);
                //Verificamos si existe el parkingId
                var parkingExists = await _parkingRepository.GetById(parkingId, token);
                if (parkingExists == null)//no existe el parkingId
                {
                    var arrCustomer = new List<ErrorDetailDto> { new ErrorDetailDto() { error = TokenError.Invalid } };
                    errorField.id = arrCustomer;
                    error.errors = errorField;
                    error.errors.message = string.Format("No existe parking Id {0}",parkingId);
                    error.status = 404;
                    throw new InvalidDynamicCommandException(error);
                }

                
                _logger.LogInformation("Dia inicio" + reservationStartAt.Date + " Hora inicio: " + reservationStartAt.Hour);
                DateTime reservationEndAt = reservationStartAt.AddMinutes(reservationParking.duration);
                reservationEndAt = reservationEndAt.AddSeconds(-1);
                _logger.LogInformation("Dia " + reservationEndAt.Date + " Hora: " + reservationEndAt.Hour);
                if (await CheckReservationValid(reservationParking, reservationEndAt,parkingId))
                {
                    return await _parkingReservationService.Insert(parkingId, reservationParking,reservationEndAt);
                }
                else
                {
                    var arrCustomer = new List<ErrorDetailDto> { new ErrorDetailDto() { error = TokenError.Invalid } };
                    errorField.id = arrCustomer;
                    error.errors = errorField;
                    error.errors.message =
                        $"Ya existe reserva para el parking id {parkingId} en el rango entre {reservationStartAt} y {reservationEndAt}";
                    error.status = 422;
                    throw new InvalidDynamicCommandException(error);
                }
                
                
            }
            else
            {
                var arrCustomer = new List<ErrorDetailDto> { new ErrorDetailDto() { error = TokenError.Format } };
                errorField.id = arrCustomer;
                error.errors = errorField;
                error.errors.message = $"La fecha hora ingresada no tiene el formato dd/MM/yyyy HH:mm";
                error.status = 422;
                throw new InvalidDynamicCommandException(error);
            }
            
        }

        private async Task<bool> CheckReservationValid(ReservationParkingDto reservationParking, DateTime reservationEndAt,string id)
        {
            var parkingsReserved = await _parkingReservationRepository.GetAllParkingById(id);
            parkingsReserved = parkingsReserved.Where(x => x.Status == Constants.PARKING_STATUS_RESERVED).ToList();
            
            foreach (var parkingReservation in parkingsReserved)
            {
                DateRange dateRange = new DateRange(parkingReservation.StartAt, parkingReservation.EndAt);
                DateTime reservationStartDate = DateTime.ParseExact(reservationParking.reservationStartAt,
                    "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                if (dateRange.Includes(reservationStartDate) || dateRange.Includes(reservationEndAt))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<ParkingSpot> GetbyId(string id)
        {
            var usrParking = _settings.Where(x => x.SettingCode == Constants.Setting_USRSCSERVICEPARKING).FirstOrDefault().Value;
            var passwordParking = _settings.Where(x => x.SettingCode == Constants.Setting_PASSSCSERVICEPARKING).FirstOrDefault().Value;
            var token = await _smartCityRepository.Authenticate("murcia", "/aparcamiento", usrParking, passwordParking);
            return await _parkingRepository.GetById(id, token);
        }

        public async Task<ParkingSpot> ChangeStatus(string parkingId,
            ParkingSlotStatusChangeDto parkingSlotStatusChange)
        {
            var error = new ErrorDto();
            dynamic errorField = new ExpandoObject();
            var usrParking = _settings.Where(x => x.SettingCode == Constants.Setting_USRSCSERVICEPARKING).FirstOrDefault().Value;
            var passwordParking = _settings.Where(x => x.SettingCode == Constants.Setting_PASSSCSERVICEPARKING).FirstOrDefault().Value;
            var token = await _smartCityRepository.Authenticate("murcia", "/aparcamiento", usrParking, passwordParking);
            var parkingExists = await _parkingRepository.GetById(parkingId,token);
            if (parkingExists == null)//no existe el parkingId
            {
                var arrCustomer = new List<ErrorDetailDto> { new ErrorDetailDto() { error = TokenError.Invalid } };
                errorField.id = arrCustomer;
                error.errors = errorField;
                error.errors.message = string.Format("No existe parking Id {0}", parkingId);
                error.status = 404;
                throw new InvalidDynamicCommandException(error);
            }

            parkingExists.Status.Value = parkingSlotStatusChange.status;
            parkingExists.Occupied.Value = parkingSlotStatusChange.status.Equals(Constants.PARKING_STATUS_FREE) ? 0 : 1;
            
            return await _parkingRepository.ChangeStatus(parkingId, parkingExists, token);
        }
    }
}
