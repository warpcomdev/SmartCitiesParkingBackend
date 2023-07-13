using System;
using System.Collections.Generic;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SCParking.Domain.Interfaces;
using SCParking.Domain.Models;
using SCParking.Infrastructure.ContextDb;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SCParking.Domain.Views.DTOs;

namespace SCParking.Infrastructure.DataAccess
{
    public class RateDetailsRepository : IRateDetailsRepository
    {
        private SmartCities_Context _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public RateDetailsRepository(SmartCities_Context context, IConfiguration configuration, ILogger<RateDetailsRepository> logger)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        public async  Task<RateDetails> Insert(RateDetails entity)
        {
            await _context.RatesDetails.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<RateDetails>> InsertBulk(List<RateDetails> entities)
        {
            await _context.RatesDetails.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<List<RateDetails>> UpdateBulk(List<RateDetails> entities,Guid rateId,Guid currentUserId)
        {
            List<RateDetails> listaRate = await _context.RatesDetails.Where(x => x.RateId == rateId).ToListAsync();
            foreach (var reg in entities)
            {
                var rateDetail = listaRate.SingleOrDefault(x => x.Hour == reg.Hour && x.Day == reg.Day);
                rateDetail.Price = reg.Price;
            }
            _context.RatesDetails.UpdateRange(listaRate);
            await _context.SaveChangesAsync();
           
            return entities;
        }

        public async Task<List<RateDetailsPostDto>> GetByRateId(Guid rateId)
        {
            return await RateDetailsToRateDetailsPostDto(await _context.RatesDetails.Where(x => x.RateId == rateId)
                .ToListAsync());
        }

        private async Task<List<RateDetailsPostDto>> RateDetailsToRateDetailsPostDto(List<RateDetails> rateDetails)
        {
            List<RateDetailsPostDto> lista = new List<RateDetailsPostDto>();
            foreach (var reg in rateDetails)
            {
                RateDetailsPostDto rateDetailsPostDto = new RateDetailsPostDto()
                {
                    id = reg.Id.ToString(),
                    day = reg.Day,
                    hour = reg.Hour,
                    price = reg.Price

                };
                lista.Add(rateDetailsPostDto);
            }

            return lista;
        }

        public async Task Delete(Guid rateId)
        {
            var details = await _context.RatesDetails.Where(x => x.RateId == rateId).ToListAsync();
            _context.RatesDetails.RemoveRange(details);
            await _context.SaveChangesAsync();
            
        }
    }
}
