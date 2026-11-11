using Microsoft.AspNetCore.Mvc;
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

            return Ok(contacts);
        }
    }
}
