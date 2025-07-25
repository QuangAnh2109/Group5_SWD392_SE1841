using System.Diagnostics;
using Group5_SWD392_SE1841.Models;
using Microsoft.AspNetCore.Mvc;

namespace Group5_SWD392_SE1841.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
