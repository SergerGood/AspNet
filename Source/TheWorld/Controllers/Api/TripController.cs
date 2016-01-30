using Microsoft.AspNet.Mvc;
using System.Net;
using TheWorld.Models;
using TheWorld.ViewModels;

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
        public JsonResult Put([FromBody] TripViewModel newTrip)
        {
            if (ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.Created;
                return Json(true);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
        }
    }
}
