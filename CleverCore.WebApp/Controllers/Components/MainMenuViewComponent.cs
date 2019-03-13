using System.Threading.Tasks;
using CleverCore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleverCore.WebApp.Controllers.Components
{
    public class MainMenuViewComponent : ViewComponent
    {

        private readonly IProductCategoryService _productCategoryService;

        public MainMenuViewComponent(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_productCategoryService.GetAll());
        }
    }
}
