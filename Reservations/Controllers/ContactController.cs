using Microsoft.AspNetCore.Mvc;
using Reservations.Models;
using Reservations.Services;

namespace Reservations.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactsService _contactsService;

        public ContactController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        public IActionResult Index(int id)
        {
            Person idPerson = _contactsService.GetContactById(id);
            return View(idPerson);
        }
    }
}
