using Microsoft.Data.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext context;

        public WorldRepository(WorldContext context)
        {
            this.context = context;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            return context.Trips.OrderBy(x => x.Name).ToList();
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            return context.Trips
                .Include(x => x.Stops)
                .OrderBy(x => x.Name).ToList();
        }
    }
}
