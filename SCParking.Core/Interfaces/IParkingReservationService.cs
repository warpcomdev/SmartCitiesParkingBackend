using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SCParking.Domain.Models;
using SCParking.Domain.Views.DTOs;

namespace SCParking.Core.Interfaces
{
    public interface IParkingReservationService
    {
        Task<ParkingReservation> Insert(string parkingId, ReservationParkingDto reservationParking,DateTime reservationEndAt);
        Task<List<ParkingReservation>> GetParkingSlotReserved();
    }
}
