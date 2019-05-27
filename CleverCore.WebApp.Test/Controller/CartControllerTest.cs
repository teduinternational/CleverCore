using System;
using System.Collections.Generic;
using System.Security.Claims;
using CleverCore.Application.Interfaces;
using CleverCore.Application.ViewModels.Product;
using CleverCore.Data.Enums;
using CleverCore.Utilities.Constants;
using CleverCore.WebApp.Controllers;
using CleverCore.WebApp.Models;
using CleverCore.WebApp.Test.Mock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace CleverCore.WebApp.Test.Controller
{
    public class CartControllerTest
    {
        public CartControllerTest()
        {
            _mockBillService = new Mock<IBillService>();
            _mockProductService = new Mock<IProductService>();
        }

        private readonly Mock<IBillService> _mockBillService;
        private readonly Mock<IProductService> _mockProductService;


        [Fact]
        public void Checkout_NullSession_RedirectResult()
        {
            var data = new List<ShoppingCartViewModel>
            {
                new ShoppingCartViewModel
                {
                    Color = null,
                    Price = 1000,
                    Product = new ProductViewModel
                    {
                        Id = 1,
                        Name = "test"
                    },
                    Quantity = 1,
                    Size = null
                }
            };

            var mockHttpContext = new Mock<HttpContext>();
            var mockSession = new MockHttpSession();
            mockSession[CommonConstants.CartSession] = JsonConvert.SerializeObject(data);
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);


            var controller = new CartController(_mockProductService.Object, _mockBillService.Object);

            controller.ControllerContext.HttpContext = mockHttpContext.Object;
            var result = controller.Checkout();

            // Assert
            Assert.IsType<RedirectResult>(result);
        }

        [Fact]
        public void Checkout_PostValid_OkResult()
        {
            var data = new List<ShoppingCartViewModel>
            {
                new ShoppingCartViewModel
                {
                    Color = new ColorViewModel
                    {
                        Id = 1,
                        Name = "Red",
                        Code = "RED"
                    },
                    Price = 1000,
                    Product = new ProductViewModel
                    {
                        Id = 1,
                        Name = "test"
                    },
                    Quantity = 1,
                    Size = new SizeViewModel
                    {
                        Id = 1,
                        Name = "M"
                    }
                }
            };

            var mockHttpContext = new Mock<HttpContext>();
            var mockSession = new MockHttpSession();
            mockSession[CommonConstants.CartSession] = JsonConvert.SerializeObject(data);
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);

            var claims = new List<Claim>
            {
                new Claim("UserId", Guid.NewGuid().ToString())
            };
            var identity = new ClaimsIdentity(claims, "Test");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            mockHttpContext.Setup(x => x.User).Returns(claimsPrincipal);

            _mockBillService.Setup(x => x.Create(It.IsAny<BillViewModel>()));

            _mockBillService.Setup(x => x.Save());

            var controller = new CartController(_mockProductService.Object, _mockBillService.Object);

            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var viewModel = new CheckoutViewModel();
            viewModel.Carts = data;
            viewModel.PaymentMethod = PaymentMethod.PaymentGateway;
            viewModel.CustomerName = "test";
            viewModel.CustomerAddress = "test";
            viewModel.CustomerMobile = "23123";

            var result = controller.Checkout(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(true, viewResult.ViewData["Success"]);
        }

        [Fact]
        public void Checkout_ValidRequest_OkResult()
        {
            var data = new List<ShoppingCartViewModel>
            {
                new ShoppingCartViewModel
                {
                    Color = new ColorViewModel
                    {
                        Id = 1,
                        Name = "Red",
                        Code = "RED"
                    },
                    Price = 1000,
                    Product = new ProductViewModel
                    {
                        Id = 1,
                        Name = "test"
                    },
                    Quantity = 1,
                    Size = new SizeViewModel
                    {
                        Id = 1,
                        Name = "M"
                    }
                }
            };

            var mockHttpContext = new Mock<HttpContext>();
            var mockSession = new MockHttpSession();
            mockSession[CommonConstants.CartSession] = JsonConvert.SerializeObject(data);


            mockHttpContext.Setup(s => s.Session).Returns(mockSession);

            var controller = new CartController(_mockProductService.Object, _mockBillService.Object);

            controller.ControllerContext.HttpContext = mockHttpContext.Object;


            var result = controller.Checkout();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CheckoutViewModel>(
                viewResult.ViewData.Model);
            Assert.Single(model.Carts);
        }
    }
}