using Reservations.Models;

namespace Reservations.Services
{
    public interface IContactsService
    {
        IEnumerable<Person> GetAllContacts();
    }
}
