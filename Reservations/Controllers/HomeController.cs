using Microsoft.AspNetCore.Mvc;
using Reservations.Models;
using Reservations.Services;
using System.Diagnostics;

namespace Reservations.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactsService _contactsService;

        public HomeController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        public IActionResult Index()
        {
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
