using Microsoft.AspNetCore.Mvc;

namespace CleverCore.WebApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}