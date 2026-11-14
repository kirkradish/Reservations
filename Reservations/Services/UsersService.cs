using Reservations.Models;
using Reservations.Repositories;

namespace Reservations.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<IEnumerable<Person>> GetAllUsersAsync()
        {
            return await _usersRepository.GetAllUsersAsync();
        }

        public async Task<Person> GetUserByIdAsync(int id)
        {
            return await _usersRepository.GetUserByIdAsync(id);
        }

        public async Task AddUserAsync(NewPersonViewModel newPerson)
        {
            await _usersRepository.AddUserAsync(newPerson);
        }

        public async Task EditUserAync(NewPersonViewModel editedPerson)
        {
            await _usersRepository.EditUserAsync(editedPerson);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _usersRepository.DeleteUserAsync(id);
        }
    }
}
