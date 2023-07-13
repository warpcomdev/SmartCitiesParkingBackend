using System.Collections.Generic;
using System.Threading.Tasks;
using SCParking.Domain.SmartCitiesModels;

namespace SCParking.Domain.Interfaces
{
    public interface ISmartCityRepository
    {
        Task<string> Authenticate(string service, string subservice, string user, string password);
        Task<List<ParkingSpot>> GetEntities(string token);
       



    }
}
