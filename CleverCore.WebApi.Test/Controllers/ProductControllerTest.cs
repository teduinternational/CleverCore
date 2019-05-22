using CleverCore.Application.Interfaces;
using CleverCore.Application.ViewModels.Product;
using CleverCore.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CleverCore.WebApi.Test.Controllers
{
    public class ProductControllerTest
    {
        [Fact]
        public void Get_ValidRequest_OkResult()
        {
            var mockProductCategory = new Mock<IProductCategoryService>();
            mockProductCategory.Setup(x => x.GetAll())
                .Returns(new List<ProductCategoryViewModel>()
            {
                new ProductCategoryViewModel(){Id = 1, Name="test 1"},
                 new ProductCategoryViewModel(){Id = 2, Name="test 2"},
            });
            var controller = new ProductController(mockProductCategory.Object);
            var result = controller.Get();
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }

        [Fact]
        public void Get_ServiceException_BadRequestResult()
        {
            var mockProductCategory = new Mock<IProductCategoryService>();
            mockProductCategory.Setup(x => x.GetAll()).Throws<Exception>();
            var controller = new ProductController(mockProductCategory.Object);
            Assert.ThrowsAny<Exception>(() => { controller.Get(); });
        }
    }
}
