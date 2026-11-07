using Reservations.Models;

namespace Reservations.Services
{
    public interface IContactsService
    {
        public Task<IEnumerable<Person>> GetAllContactsAsync();

        public Task<Person> GetContactByIdAsync(int id);

        public Task AddPersonAsync(Person person);

        public Task EditContactAync(Person person);
    }
}
