using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
          _logger.LogInformation("Getting All Trips from the Context");

          return _context.Trips.ToList();
        }

        public Trip GetTrip(string tripName)
        {
            _logger.LogInformation("Getting All Trips from the Context");
            return _context.Trips
                .Include(x => x.Stops).FirstOrDefault(x => x.Name == tripName);
        }

        public void AddTrip(Trip trip)
        {
            _logger.LogInformation("Add Trip to the Context");
            _context.Trips.Add(trip);
        }


        public void AddStopsToTrip(string tripName, ICollection<Stop> stops)
        {
            _logger.LogInformation("Add Stops to the Context");
            _context.Stops.AddRange(stops);
        }

        public void AddStopToTrip(string tripName, Stop stop)
        {
            _logger.LogInformation("Add Stops to the Context");
            var trip = GetTrip(tripName);
            if (trip != null)
            {
                trip.Stops.Add(stop);
                _context.Stops.Add(stop);
            }
        }

        public async Task<int> SaveContext()
        {
            _logger.LogInformation("Save context to BBDD");
            return await _context.SaveChangesAsync();
        }



    }
}
