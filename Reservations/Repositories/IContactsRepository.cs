using Reservations.Models;

namespace Reservations.Repositories
{
    public interface IContactsRepository
    {
        public IEnumerable<Person> GetAll();
        public void AddPerson(Person person);
    }
}
