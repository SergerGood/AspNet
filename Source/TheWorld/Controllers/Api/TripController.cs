using Microsoft.AspNet.Mvc;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
    public class TripController : Controller
    {
        private IWorldRepository repository;

        public TripController(IWorldRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("api/trips")]
        public JsonResult Get()
        {
            var results = repository.GetAllTripsWithStops();

            return Json(results);
        }
    }
}
