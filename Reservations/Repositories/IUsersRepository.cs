using Reservations.Models;

namespace Reservations.Repositories
{
    public interface IUsersRepository
    {
        public Task<IEnumerable<Person>> GetAllUsersAsync();

        public Task<Person> GetUserByIdAsync(int id);

        public Task AddUserAsync(NewPersonViewModel person);

        public Task EditUserAsync(NewPersonViewModel person);

        public Task DeleteUserAsync(int id);
    }
}
