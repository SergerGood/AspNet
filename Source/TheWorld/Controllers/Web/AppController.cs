using System;
using Microsoft.AspNet.Mvc;
using TheWorld.Services;
using TheWorld.ViewModels;
using TheWorld.Models;
using System.Linq;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly WorldContext context;
        private readonly IMailService service;

        public AppController(IMailService service, WorldContext context)
        {
            this.service = service;
            this.context = context;
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                string email = Startup.Configuration["AppSettings:SiteEmailAdress"];

                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("", "Could not send mail, configuration problem");
                }

                bool sendMail = service.SendMail(email,
                    email,
                    $"Contact Page From {model.Name} ({model.Email})",
                    model.Message);

                if (sendMail == true)
                {
                    ModelState.Clear();

                    ViewBag.Message = "Mail Sent. Thanks!";
                }
            }

            return View();
        }

        public IActionResult Index()
        {
            var trips = context.Trips.OrderBy(x => x.Name).ToList();

            return View(trips);
        }
    }
}
