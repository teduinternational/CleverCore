using System.Threading.Tasks;
using CleverCore.Utilities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CleverCore.WebApp.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
