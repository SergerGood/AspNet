using Microsoft.Extensions.Logging;
using System.Net;

namespace TheWorld.Services
{
    public class CoordService
    {
        private ILogger<CoordService> logger;

        public CoordService(ILogger<CoordService> logger)
        {
            this.logger = logger;
        }

        public CoordServiceResult Lookup(string location)
        {
            var result = new CoordServiceResult
            {
                Success = false,
                Message = "Undetermined"
            };

            var encodeName = WebUtility.UrlEncode(location);
            var bingKey = Startup.Configuration["AppSettings:BingKey"];

            var url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodeName}&key={bingKey}";

            return result;
        }
    }
}
