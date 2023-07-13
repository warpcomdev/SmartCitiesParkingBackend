using System.Collections.Generic;
using System.Threading.Tasks;
using SCParking.Domain.Models;

namespace SCParking.Domain.Interfaces
{
    public interface IParkingReservationRepository
    {
        Task<ParkingReservation> Insert(ParkingReservation model);
        Task<ParkingReservation> Update(ParkingReservation model);
        Task<List<ParkingReservation>> GetParkingSlotReserved();
        Task<ParkingReservation> GetByParkingId(string parkingId);
        Task<List<ParkingReservation>> GetAllParkingById(string parkingId);
    }
}
