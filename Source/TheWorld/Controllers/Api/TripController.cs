using Microsoft.AspNet.Mvc;

namespace TheWorld.Controllers.Api
{
    public class TripController : Controller
    {
        [HttpGet("api/trips")]
        public JsonResult Get()
        {
            return Json( new { name = "Shawn" });
        }
    }
}
