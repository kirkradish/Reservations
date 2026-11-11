using Microsoft.AspNetCore.Mvc;
using Reservations.Models;
using Reservations.Services;

namespace Reservations.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactsService _contactsService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(
            IContactsService contactsService, 
            IHttpClientFactory httpClientFactory,
            ILogger<ContactsController> logger)
        {
            _contactsService = contactsService;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ContactsApi");
                var contacts = await client.GetFromJsonAsync<List<Person>>(
                    "api/contacts"
                );

                if (contacts == null)
                    return NotFound();

                return View(contacts);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error communicating with Contacts API");
                return StatusCode(503, "Unable to retrieve contacts.");
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Contacts API request timed out");
                return StatusCode(504, "The API request timed out.");
            }
        }

        public async Task<IActionResult> Profile(int id)
        {
            var client = _httpClientFactory.CreateClient("ContactsApi");
            var contact = await client.GetFromJsonAsync<NewPersonViewModel>(
                $"api/contacts/{id}");

            if (contact == null)
                return NotFound();

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContact(NewPersonViewModel editedModel)
        {
            var client = _httpClientFactory.CreateClient("ContactsApi");

            var response = await client.PutAsJsonAsync(
                $"api/contacts/{editedModel.Id}", editedModel);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Profile", new { id = editedModel.Id });

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            ModelState.AddModelError("", "Unable to update contacts.");

            return View(editedModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var client = _httpClientFactory.CreateClient("ContactsApi");

            var response = await client.DeleteAsync($"api/contacts/{id}");

            if (response.IsSuccessStatusCode)
                return Ok();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContact(NewPersonViewModel newContact)
        {
            if (!ModelState.IsValid)
                return View(newContact);

            await _contactsService.AddPersonAsync(newContact);

            return RedirectToAction("Index");
        }
    }
}
