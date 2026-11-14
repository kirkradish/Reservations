using Reservations.Models;

namespace Reservations.Services
{
    public interface IUsersService
    {
        public Task<IEnumerable<Person>> GetAllUsersAsync();

        public Task<Person> GetUserByIdAsync(int id);

        public Task AddUserAsync(NewPersonViewModel person);

        public Task EditUserAync(NewPersonViewModel person);

        public Task DeleteUserAsync(int id);
    }
}
