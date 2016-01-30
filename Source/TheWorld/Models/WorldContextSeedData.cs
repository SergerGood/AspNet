using System;
using System.Collections.Generic;
using System.Linq;

namespace TheWorld.Models
{
    public class WorldContextSeedData
    {
        private readonly WorldContext context;

        public WorldContextSeedData(WorldContext context)
        {
            this.context = context;
        }

        public void EnsureSeedData()
        {
            if(context.Trips.Any() == false)
            {
                var usTrip = new Trip
                {
                    Name = "US Trip",
                    Created = DateTime.UtcNow,
                    UserName = "",
                    Stops = new List<Stop>
                    {
                        new Stop
                        {
                            Name ="Atlanta, GA",
                            Arrival = new DateTime(2016,6,4),
                            Latitude = 33.748995,
                            Longitude = -84.387982,
                            Order = 0
                        }
                    }
                };

                context.Trips.Add(usTrip);
                context.Stops.AddRange(usTrip.Stops);

                var worldTrip = new Trip
                {
                    Name = "World Trip",
                    Created = DateTime.UtcNow,
                    UserName = "",
                    Stops = new List<Stop>
                    {
                        new Stop
                        {
                            Order = 1,
                            Latitude = 33.748995,
                            Longitude = -84.387982,
                            Name ="Atlanta, Georgia",
                            Arrival = DateTime.Parse("Jun 4, 2016")
                        }
                    }
                };

                context.Trips.Add(worldTrip);
                context.Stops.AddRange(worldTrip.Stops);

                context.SaveChanges();
            }
        }
    }
}
