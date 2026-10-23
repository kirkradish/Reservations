using Reservations.Models;

namespace Reservations.Repositories
{
    public interface IContactsRepository
    {
        public IEnumerable<Person> GetAll();

        public Person GetContactById(int id);

        public void AddPerson(Person person);

        public void EditContact(Person person);
    }
}
