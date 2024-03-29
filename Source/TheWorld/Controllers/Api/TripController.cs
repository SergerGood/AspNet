﻿using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using TheWorld.Models;
using TheWorld.ViewModels;
using Microsoft.AspNet.Authorization;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    [Route("api/trips")]
    public class TripController : Controller
    {
        private ILogger<TripController> logger;
        private IWorldRepository repository;

        public TripController(IWorldRepository repository, ILogger<TripController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var results = repository.GetUserTripsWithStops(User.Identity.Name);

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
                    newTrip.UserName = User.Identity.Name;

                    logger.LogInformation("Attempting to save a new trip");
                    repository.AddTrip(newTrip);

                    if (repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(newTrip));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Faile to save new trip", ex);

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
        }
    }
}
