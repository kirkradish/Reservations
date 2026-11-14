using Microsoft.AspNetCore.Mvc;
using Reservations.Models;
using Reservations.Services;

namespace Reservations.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersAPIController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersAPIController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _usersService.GetAllUsersAsync();

            if (users == null)
                return NotFound();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _usersService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(int id, NewPersonViewModel editedModel)
        {
            if (id != editedModel.Id)
                return BadRequest();

            var existingUser = await _usersService.GetUserByIdAsync(id);

            if (existingUser == null)
                return NotFound();

            await _usersService.EditUserAync(editedModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existingUser = await _usersService.GetUserByIdAsync(id);

            if (existingUser == null)
                return NotFound();

            await _usersService.DeleteUserAsync(id);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(NewPersonViewModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _usersService.AddUserAsync(user);

            return Ok();
        }
    }
}
