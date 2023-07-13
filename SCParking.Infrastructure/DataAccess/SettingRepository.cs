using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SCParking.Domain.Interfaces;
using SCParking.Domain.Models;
using SCParking.Infrastructure.ContextDb;
using System.Linq;
namespace SCParking.Infrastructure.DataAccess
{
    public class SettingRepository:ISettingRepository
    {
        private SmartCities_Context _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public SettingRepository(SmartCities_Context context, IConfiguration configuration, ILogger<SettingRepository> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }
      

        public ICollection<Setting> Get()
        {
            try
            {
                return _context.Settings.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
            
        }

        
    }
}
