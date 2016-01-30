using Microsoft.AspNet.Mvc;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripController : Controller
    {
        private IWorldRepository repository;

        public TripController(IWorldRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var results = repository.GetAllTripsWithStops();

            return Json(results);
        }

        [HttpPost("")]
        public JsonResult Put([FromBody] Trip newTrip)
        {
            return Json(true);
        }
    }
}
