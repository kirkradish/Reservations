using Reservations.Models;

namespace Reservations.Repositories
{
    public interface IContactsRepository
    {
        IEnumerable<Person> GetAll();
    }
}
