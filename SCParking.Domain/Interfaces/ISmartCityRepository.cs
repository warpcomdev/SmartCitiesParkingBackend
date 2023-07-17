using System.Collections.Generic;
using System.Threading.Tasks;
using SCParking.Domain.SmartCitiesModels;
using SCParking.Domain.Views.DTOs;

namespace SCParking.Domain.Interfaces
{
    public interface ISmartCityRepository
    {
        Task<string> Authenticate(string service, string subservice, string user, string password);
        Task<List<ParkingSpot>> GetEntities(string token);
        Task<ICollection<BiciEntityDto>> GetMuyBicisEntity();
        Task SendBatchEntities(string token, BiciEntityRequestDto biciEntityRequestDto);





    }
}
