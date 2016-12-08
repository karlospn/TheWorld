using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheWorld.Models;

namespace TheWorld.Repository
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
          _context = context;
          _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
          _logger.LogInformation("Getting All Trips from the Database");

          return _context.Trips.ToList();
        }

        public void AddTripToDbContext(Trip trip)
        {
            _context.Trips.Add(trip);
            _context.Stops.AddRange(trip.Stops);
        }

        public Task<int> SaveContext()
        {
            return _context.SaveChangesAsync();
        }
   
    }
}
