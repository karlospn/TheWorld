using System.Collections.Generic;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Repository
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetTripsByUsername(string user);
        Trip GetTrip(string tripName);
        Trip GetUserTrip(string tripName, string user);
        void AddTrip(Trip trip);
        void AddStopsToTrip(string tripName, ICollection<Stop> stops);
        void AddStopToTrip(string tripName,Stop stop);
        void AddStopToUserTrip(string tripName, Stop stopEntity, string identityName);

        Task<int> SaveContext();

    }
}