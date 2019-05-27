using System.Collections.Generic;
using System.IO;
using CleverCore.Application.Interfaces;
using CleverCore.Application.ViewModels.Product;
using CleverCore.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace CleverCore.WebApp.Test.Controller
{
    public class ProductControllerTest
    {
        [Fact]
        public void Index_GetCategory_ValidModel()
        {

            var mockProductService = new Mock<IProductService>();
            var mockBillService = new Mock<IBillService>();
            var mockProductCategoryService = new Mock<IProductCategoryService>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            mockProductCategoryService.Setup(x => x.GetAll()).Returns(new List<ProductCategoryViewModel>()
            {
                new ProductCategoryViewModel()
                {
                    Name = "Test"
                }
            });
            
            ProductController controller = new ProductController(mockProductService.Object, configuration,
                mockBillService.Object,mockProductCategoryService.Object);

            var result = controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<ProductCategoryViewModel>>(
                viewResult.ViewData.Model);
            Assert.Single(model);
        }
    }
}
