using Reservations.Models;
using Reservations.Repositories;

namespace Reservations.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _contactsRepository;

        public ContactsService(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }

        public async Task<IEnumerable<Person>> GetAllContactsAsync()
        {
            return await _contactsRepository.GetAllAsync();
        }

        public async Task<Person> GetContactByIdAsync(int id)
        {
            return await _contactsRepository.GetContactByIdAsync(id);
        }

        public async Task AddPersonAsync(Person newPerson)
        {
            await _contactsRepository.AddPersonAsync(newPerson);
        }

        public async Task EditContactAync(Person editedPerson)
        {
            await _contactsRepository.EditContactAsync(editedPerson);
        }

        public async Task DeleteContactAsync(int id)
        {
            await _contactsRepository.DeleteContactAsync(id);
        }
    }
}
