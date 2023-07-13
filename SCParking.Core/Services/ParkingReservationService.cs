using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using SCParking.Core.Interfaces;
using SCParking.Domain.Interfaces;
using SCParking.Domain.Models;
using SCParking.Domain.Views.DTOs;

namespace SCParking.Core.Services
{
    public class ParkingReservationService:IParkingReservationService
    {
        private readonly IParkingReservationRepository _parkingReservationRepository;
        public ParkingReservationService(IParkingReservationRepository parkingReservationRepository)
        {
            _parkingReservationRepository = parkingReservationRepository;
        }

        public async Task<List<ParkingReservation>> GetParkingSlotReserved()
        {
            return await _parkingReservationRepository.GetParkingSlotReserved();
        }

        public async Task<ParkingReservation> Insert(string parkingId, ReservationParkingDto reservationParking,DateTime reservationEndAt)
        {
            var entity = new ParkingReservation()
            {
                Id = Guid.NewGuid(),
                ParkingSpotId = parkingId,
                Status = "reserved",
                StartAt = reservationParking.reservationStartAt == null?DateTime.Now : DateTime.ParseExact(reservationParking.reservationStartAt+":00","dd/MM/yyyy HH:mm:ss",CultureInfo.InvariantCulture),
                Duration = reservationParking.duration,
                EndAt = reservationEndAt,
                UserToken = reservationParking.tokenUser

            };
            return await _parkingReservationRepository.Insert(entity);
        }
    }
}
