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

        public IEnumerable<Person> GetAllContacts()
        {
            return _contactsRepository.GetAll();
        }

        public Person GetContactById(int id)
        {
            return _contactsRepository.GetContactById(id);
        }

        public void AddPerson(Person newPerson)
        {
            _contactsRepository.AddPerson(newPerson);
        }
    }
}
