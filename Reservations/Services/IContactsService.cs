using Reservations.Models;

namespace Reservations.Services
{
    public interface IContactsService
    {
        public Task<IEnumerable<Person>> GetAllContactsAsync();

        public Task<Person> GetContactByIdAsync(int id);

        public Task AddPersonAsync(NewPersonViewModel person);

        public Task EditContactAync(NewPersonViewModel person);

        public Task DeleteContactAsync(int id);
    }
}
