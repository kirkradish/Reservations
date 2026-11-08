using Reservations.Models;

namespace Reservations.Repositories
{
    public interface IContactsRepository
    {
        public Task<IEnumerable<Person>> GetAllAsync();

        public Task<Person> GetContactByIdAsync(int id);

        public Task AddPersonAsync(NewPersonViewModel person);

        public Task EditContactAsync(NewPersonViewModel person);

        public Task DeleteContactAsync(int id);
    }
}
