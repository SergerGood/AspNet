using AutoMapper;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
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

            return Json(Mapper.Map<IEnumerable<TripViewModel>>(results));
        }

        [HttpPost("")]
        public JsonResult Put([FromBody] TripViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTrip = Mapper.Map<Trip>(vm);

                    Response.StatusCode = (int)HttpStatusCode.Created;
                    return Json(Mapper.Map<TripViewModel>(newTrip));
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
        }
    }
}
