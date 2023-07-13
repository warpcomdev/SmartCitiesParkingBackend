using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SCParking.Domain.Models;
using SCParking.Domain.Views.DTOs;

namespace SCParking.Domain.Interfaces
{
    public interface IRateDetailsRepository
    {
        Task<RateDetails> Insert(RateDetails entity);
        Task<List<RateDetails>> InsertBulk(List<RateDetails> entities);
        Task<List<RateDetails>> UpdateBulk(List<RateDetails> entities, Guid rateId, Guid currentUserId);
        Task<List<RateDetailsPostDto>> GetByRateId(Guid rateId);
        Task Delete(Guid rateId);

    }
}
