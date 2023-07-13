using System.Collections.Generic;
using System.Threading.Tasks;
using SCParking.Domain.Models;
using SCParking.Domain.SmartCitiesModels;
using SCParking.Domain.Views.DTOs;

namespace SCParking.Core.Interfaces
{
    public interface IParkingService
    {
        Task<List<ParkingSpotResponseDto>> GetbyStatus(string status, string placeType);
        Task<ParkingReservation> Reserve(string parkingId, ReservationParkingDto reservationParking);
        Task<ParkingSpot> GetbyId(string id);
        Task<ParkingSpot> ChangeStatus(string parkingId,ParkingSlotStatusChangeDto parkingSlotStatusChange);

    }
}
