using System;
using System.Threading.Tasks;
using CleverCore.Application.Interfaces;
using CleverCore.Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CleverCore.WebApp.Controllers.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IMemoryCache _memoryCache;
        public CategoryMenuViewComponent(IProductCategoryService productCategoryService,IMemoryCache memoryCache)
        {
            _productCategoryService = productCategoryService;
            _memoryCache = memoryCache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _memoryCache.GetOrCreate(CacheKeys.ProductCategories, entry => {
                entry.SlidingExpiration = TimeSpan.FromHours(2);
                return _productCategoryService.GetAll();
            });
            
            return View(categories);
        }
    }
}
