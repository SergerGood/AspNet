using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext context;
        private ILogger<WorldRepository> logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public void AddTrip(Trip newTrip)
        {
            context.Add(newTrip);
        }

        public bool SaveAll()
        {
            return context.SaveChanges() > 0;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return context.Trips.OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError("Could not get trips from database", ex);
                return null;
            }
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return context.Trips
                    .Include(x => x.Stops)
                    .OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError("Could not get trips with stops from database", ex);
                return null;
            }

        }

        public Trip GetTripByName(string tripName)
        {
            return context.Trips
                .Include(x => x.Stops)
                .FirstOrDefault(x => x.Name == tripName );
        }

        public void AddStop(string tripName, Stop newStop)
        {
            var theTrip = GetTripByName(tripName);

            int orderMax = 0;
            if (theTrip.Stops.Any())
            {
                orderMax = theTrip.Stops.Max(x => x.Order);
            }

            newStop.Order = orderMax + 1;

            context.Stops.Add(newStop);
        }
    }
}
