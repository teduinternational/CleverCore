using Microsoft.AspNetCore.Mvc;

namespace CleverCore.WebApp.Controllers
{
    public class AjaxContentController : Controller
    {
        public IActionResult HeaderCart()
        {
            return ViewComponent("HeaderCart");
        }
    }
}