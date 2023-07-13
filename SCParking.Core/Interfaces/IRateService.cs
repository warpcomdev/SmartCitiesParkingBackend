using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SCParking.Domain.Views.DTOs;

namespace SCParking.Core.Interfaces
{
    public interface IRateService
    {
        Task<RatePostDto> Insert(RatePostDto ratePostDto,Guid currentUserId);
        Task<RatePostDto> Update(RatePostDto ratePostDto, Guid currentUserId);
        Task<RatePostDto> GetByPlaceType(string placeType);
        Task<RatePostDto> GetById(Guid id);
        Task Delete(Guid id);
        Task<List<RatePostDto>> GetRates();
    }
}
