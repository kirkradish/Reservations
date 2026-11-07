using Microsoft.AspNetCore.Mvc;
using Reservations.Models;
using Reservations.Services;

namespace Reservations.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactsService _contactsService;

        public ContactsController(IContactsService contactsService)
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

        public IActionResult View(int id)
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
                Email = editedModel.Email,
                PhoneNumber = editedModel.PhoneNumber
            };

            _contactsService.EditContact(domainPerson);

            return RedirectToAction("Index", new { id = domainPerson.Id });
        }

        public IActionResult Add()
        {
            return View();
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
