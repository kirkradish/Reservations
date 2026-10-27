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

        [HttpPost]
        public IActionResult EditContact(NewPersonViewModel editedModel)
        {
            Person domainPerson = new Person
            {

                Id = Convert.ToInt32(editedModel.Id),
                FirstName = editedModel.FirstName,
                MiddleName = editedModel.MiddleName,
                LastName = editedModel.LastName,
                Email = editedModel.Email
            };

            _contactsService.EditContact(domainPerson);

            return RedirectToAction("Index", new { id = domainPerson.Id });
        }
    }
}
