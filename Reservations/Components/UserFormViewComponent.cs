using Microsoft.AspNetCore.Mvc;
using Reservations.Models;
using Reservations.Services;
using System.Threading.Tasks;

namespace Reservations.Components
{
    public class UserFormViewComponent : ViewComponent
    {
        IContactsService _contactService;

        public UserFormViewComponent(IContactsService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? id, string formType)
        {
            NewPersonViewModel model = new NewPersonViewModel();

            ViewBag.FormType = formType;

            if (formType == "EditContact" && id.HasValue)
            {
                Person person = _contactService.GetContactById(id.Value);

                if (person != null)
                {
                    model = new NewPersonViewModel
                    {
                        Id = person.Id,
                        FirstName = person.FirstName,
                        MiddleName = person.MiddleName,
                        LastName = person.LastName,
                        Email = person.Email,
                        PhoneNumber = person.PhoneNumber,
                    };
                }
                else
                {
                    model = new NewPersonViewModel
                    {
                        Id = 0,
                        FirstName = "",
                        MiddleName = "",
                        LastName = "",
                        Email = "",
                        PhoneNumber = ""
                    };
                }
            }

            return View(model);
        }
    }
}
