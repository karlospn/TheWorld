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
        void AddStopsToTrip(string tripName, ICollection<Stop> stops);
        void AddStopToTrip(string tripName,Stop stop);

        Task<int> SaveContext();
    }
}