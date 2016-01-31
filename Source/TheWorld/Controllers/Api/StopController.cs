using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private ILogger<StopController> logger;
        private IWorldRepository repository;

        public StopController(IWorldRepository repository, ILogger<StopController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try
            {
                var results = repository.GetTripByname(tripName);
                if(results == null)
                {
                    return Json(null);
                }

                return Json(Mapper.Map<IEnumerable<StopViewModel>>(results.Stops.OrderBy(x => x.Order)));
            }
            catch (Exception ex)
            {
                logger.LogError($"Faile to get stops for trip {tripName}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json("Error occurred finding trip name");
            }
        }
    }
}
