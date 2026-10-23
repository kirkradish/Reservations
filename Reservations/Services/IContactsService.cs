using Reservations.Models;

namespace Reservations.Services
{
    public interface IContactsService
    {
        public IEnumerable<Person> GetAllContacts();

        public Person GetContactById(int id);

        public void AddPerson(Person person);
    }
}
