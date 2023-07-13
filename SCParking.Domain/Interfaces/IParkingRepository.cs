using System.Collections.Generic;
using System.Threading.Tasks;
using SCParking.Domain.SmartCitiesModels;
using SCParking.Domain.Views.DTOs;

namespace SCParking.Domain.Interfaces
{
    public interface IParkingRepository
    {

        Task<List<ParkingSpotResponseDto>> GetByStatus(string status, string placeType, string token);
        Task<ParkingSpot> GetById(string id, string token);
        Task<ParkingSpot> ChangeStatus(string id, ParkingSpot parkingSpot,string token);
    }
}
