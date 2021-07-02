using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientService clientService;

        public HomeController(IClientService clientService)
        {
            this.clientService = clientService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetClients()
        {
            var data = clientService.GetAllClients();
            return View(data);
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
