using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Reservations.Components
{
    public class UserFormViewComponent : ViewComponent
    {
        public UserFormViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync(string formType)
        {
            ViewBag.FormType = formType;
            return View();
        }
    }
}
