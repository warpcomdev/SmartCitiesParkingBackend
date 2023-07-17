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
    public class ParkingReservationRepository : IParkingReservationRepository
    {
        private SmartCities_Context _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public ParkingReservationRepository(SmartCities_Context context, IConfiguration configuration, ILogger<ParkingReservationRepository> logger)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
        }
        public async Task<ParkingReservation> Insert(ParkingReservation model)
        {
            await _context.ParkingReservations.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<ParkingReservation> Update(ParkingReservation model)
        {
            _context.ParkingReservations.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public Task<List<ParkingReservation>> GetParkingSlotReserved()
        {
            return _context.ParkingReservations.ToListAsync();
        }

        public async Task<ParkingReservation> GetByParkingId(string parkingId)
        {
            var parkingSlot = await _context.ParkingReservations.Where(x => x.ParkingSpotId == parkingId).FirstOrDefaultAsync();
            return parkingSlot;
        }

        public async Task<List<ParkingReservation>> GetAllParkingById(string parkingId)
        {
            var parkingSlot = await _context.ParkingReservations.Where(x => x.ParkingSpotId == parkingId).ToListAsync();
            return parkingSlot;
        }
    }
}
