using Microsoft.AspNetCore.Mvc;
using Reservations.Models;
using Reservations.Services;

namespace Reservations.Controllers
{
    public class ContactsListController : Controller
    {
        private readonly IContactsService _contactsService;

        public ContactsListController(IContactsService contactsService)
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

        [HttpPost]
        public IActionResult AddContact(Person newPerson)
        {
            if (ModelState.IsValid)
            {
                _contactsService.AddPerson(newPerson);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
