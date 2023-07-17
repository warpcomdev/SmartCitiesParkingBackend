using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SCParking.Domain.Interfaces;
using SCParking.Domain.Models;
using SCParking.Infrastructure.ContextDb;
using System.Linq;
namespace SCParking.Infrastructure.DataAccess
{
    public class RateRepository:IRateRepository
    {
        private SmartCities_Context _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public RateRepository(SmartCities_Context context, IConfiguration configuration, ILogger<RateRepository> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        public async  Task<Rate> Insert(Rate entity)
        {
            await _context.Rates.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Rate> GetById(Guid id)
        {
            return await _context.Rates.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<Rate> Update(Rate entity,Guid currentUserId)
        {
            var rateExist = await _context.Rates.Where(x => x.Id == entity.Id).SingleOrDefaultAsync();
            rateExist.Name = entity.Name;
            rateExist.EndDate = entity.EndDate;
            rateExist.PlaceType = entity.PlaceType;
            rateExist.Price = entity.Price;
            rateExist.StartDate = entity.StartDate;
            rateExist.ModifiedBy = currentUserId;
            rateExist.UpdatedAt  = DateTime.Now;
            _context.Rates.Update(rateExist);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Rate> GetRateByPlaceType(string placeType)
        {

            var resp = await _context.Rates.Where(x=>x.PlaceType == placeType).SingleOrDefaultAsync();
            return resp;
        }

        public async Task Delete(Guid id)
        {
            var entity = await _context.Rates.Where(x => x.Id == id).SingleOrDefaultAsync();
            _context.Remove(entity);
            await _context.SaveChangesAsync();

        }

        public async Task<Rate> GetRateByName(string name)
        {
            return await _context.Rates.Where(x => x.Name == name).SingleOrDefaultAsync();
        }

        public async Task<List<Rate>> GetRates()
        {
            return await _context.Rates.ToListAsync();
        }
    }
}
