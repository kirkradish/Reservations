using Microsoft.AspNetCore.Mvc;
using Reservations.Models;
using Reservations.Services;

namespace Reservations.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsAPIController : ControllerBase
    {
        private readonly IContactsService _contactsService;

        public ContactsAPIController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _contactsService.GetAllContactsAsync();

            if (contacts == null)
                return NotFound();

            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var contact = await _contactsService.GetContactByIdAsync(id);

            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditContact(int id, NewPersonViewModel editedModel)
        {
            if (id != editedModel.Id)
                return BadRequest();

            var existingContact = await _contactsService.GetContactByIdAsync(id);

            if (existingContact == null)
                return NotFound();

            await _contactsService.EditContactAync(editedModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var existingContact = await _contactsService.GetContactByIdAsync(id);

            if (existingContact == null)
                return NotFound();

            await _contactsService.DeleteContactAsync(id);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(NewPersonViewModel  contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _contactsService.AddPersonAsync(contact);

            return Ok();
        }
    }
}
