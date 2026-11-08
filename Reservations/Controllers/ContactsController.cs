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

        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _contactsService.GetAllContactsAsync();
                return View(data);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Could not load contacts: " + ex.Message;
                return View(new List<Reservations.Models.Person>());
            }
        }

        public async Task<IActionResult> View(int id)
        {
            Person idPerson = await _contactsService.GetContactByIdAsync(id);

            var validPerson = new NewPersonViewModel
            {
                Id = idPerson.Id ?? 0,
                FirstName = idPerson.FirstName,
                MiddleName = idPerson.MiddleName,
                LastName = idPerson.LastName,
                Email = idPerson.Email,
                PhoneNumber = idPerson.PhoneNumber
            };

            return View(validPerson);
        }

        [HttpPost]
        public async Task<IActionResult> EditContact(NewPersonViewModel editedModel)
        {
            if (!ModelState.IsValid)
            {
                return View("View", editedModel);
            }

            await _contactsService.EditContactAync(editedModel);

            return RedirectToAction("Index", new { id = editedModel.Id });

            //Person domainPerson = new Person
            //{
            //    Id = Convert.ToInt32(editedModel.Id),
            //    FirstName = editedModel.FirstName,
            //    MiddleName = editedModel.MiddleName,
            //    LastName = editedModel.LastName,
            //    Email = editedModel.Email,
            //    PhoneNumber = editedModel.PhoneNumber
            //};

            //await _contactsService.EditContactAync(domainPerson);

            //return RedirectToAction("Index", new { id = domainPerson.Id });
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContact(NewPersonViewModel newPerson)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", newPerson);
            }

            await _contactsService.AddPersonAsync(newPerson);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteContact(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A valid contact ID must be provided.");
            }

            await _contactsService.DeleteContactAsync(id);
            return RedirectToAction("Index");
        }
    }
}
