using Hospital.Services;
using HospitalWebMGT.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HospitalWebMGT.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }
      


        [HttpGet]
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult Description()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult Description1()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult Description2()
        {
            return View();
        }


        [Route("[action]")]
        public IActionResult ContactUs()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult AddLocation()
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