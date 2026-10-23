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
            try
            {
                var data = _contactsService.GetAllContacts();
                return View(data);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Could not load contacts: " + ex.Message;
                return View(new List<Reservations.Models.Person>());
            }
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

        [HttpPost]
        public IActionResult AddPerson(Person newPerson)
        {
            string firstName = newPerson.FirstName;
            string? middleName = newPerson.MiddleName;
            string lastName = newPerson.LastName;

            if (ModelState.IsValid)
            {
                _contactsService.AddPerson(newPerson);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
