using System.Collections.Generic;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Repository
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        void AddTripToDbContext(Trip trip);

        Task<int> SaveContext();
    }
}