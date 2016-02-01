using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace TheWorld.Models
{
    public class WorldContextSeedData
    {
        private readonly WorldContext context;
        private UserManager<WorldUser> userManager;

        public WorldContextSeedData(WorldContext context, UserManager<WorldUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task EnsureSeedDataAsync()
        {
            if(await userManager.FindByEmailAsync("sam@theworld.com") == null)
            {
                var newUser = new WorldUser
                {
                    UserName = "Sam",
                    Email = "sam@theworld.com"
                };

                var result = await userManager.CreateAsync(newUser, "P@ssw0rd");
                if (result.Succeeded == false)
                {
                    var errors = result.Errors;
                }
            }

            if (context.Trips.Any() == false)
            {
                var usTrip = new Trip
                {
                    Name = "US Trip",
                    Created = DateTime.UtcNow,
                    UserName = "Sam",
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
                    UserName = "Sam",
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
