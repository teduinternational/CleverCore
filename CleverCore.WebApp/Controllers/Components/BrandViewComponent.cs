using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CleverCore.WebApp.Controllers.Components
{
    public class BrandViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
