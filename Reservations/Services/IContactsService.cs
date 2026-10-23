using Reservations.Models;

namespace Reservations.Services
{
    public interface IContactsService
    {
        public IEnumerable<Person> GetAllContacts();

        public void AddPerson(Person person);
    }
}
