using BootcampMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BootcampMVC.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View(Repository.Bootcamps);
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}
