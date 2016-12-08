using System.Collections.Generic;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Repository
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        Trip GetTrip(string tripName);
        void AddTrip(Trip trip);
        void AddStopsToTrip(Trip trip, ICollection<Stop> stops);

        Task<int> SaveContext();
    }
}