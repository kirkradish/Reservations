using Microsoft.AspNetCore.Mvc;
using Reservations.Models;
using Reservations.Services;

namespace Reservations.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IUsersService usersService, 
            IHttpClientFactory httpClientFactory,
            ILogger<UsersController> logger)
        {
            _usersService = usersService;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("UsersApi");
                var users = await client.GetFromJsonAsync<List<Person>>(
                    "api/users"
                );

                if (users == null)
                    return NotFound();

                return View(users);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error communicating with Users API");
                return StatusCode(503, "Unable to retrieve Users.");
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Users API request timed out");
                return StatusCode(504, "The API request timed out.");
            }
        }

        public async Task<IActionResult> Profile(int id)
        {
            var client = _httpClientFactory.CreateClient("UsersApi");
            var user = await client.GetFromJsonAsync<NewPersonViewModel>(
                $"api/users/{id}");

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(NewPersonViewModel editedModel)
        {
            var client = _httpClientFactory.CreateClient("UsersApi");

            var response = await client.PutAsJsonAsync(
                $"api/users/{editedModel.Id}", editedModel);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Profile", new { id = editedModel.Id });

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            ModelState.AddModelError("", "Unable to update user.");

            return View(editedModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var client = _httpClientFactory.CreateClient("UsersApi");

            var response = await client.DeleteAsync($"api/users/{id}");

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
        public async Task<IActionResult> AddUser(NewPersonViewModel newUser)
        {
            if (!ModelState.IsValid)
                return View(newUser);

            await _usersService.AddUserAsync(newUser);

            return RedirectToAction("Index");
        }
    }
}
