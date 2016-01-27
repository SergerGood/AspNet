using Microsoft.AspNet.Mvc;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController: Controller
    {
        private IMailService service;

        public AppController(IMailService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
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
            service.SendMail("",
                "",
                $"Contact Page From {model.Name} ({model.Email})",
                model.Message);

            return View();
        }
    }
}
