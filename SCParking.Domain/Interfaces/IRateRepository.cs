using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SCParking.Domain.Models;

namespace SCParking.Domain.Interfaces
{
    public interface IRateRepository
    {
        Task<Rate> Insert(Rate entity);
        Task<Rate> GetById(Guid id);
        Task<Rate> Update(Rate entity,Guid currentUserId);
        Task<Rate> GetRateByPlaceType(string placeType);
        Task<Rate> GetRateByName(string name);
        Task Delete(Guid id);
        Task<List<Rate>> GetRates();

    }
}
