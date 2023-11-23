using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;
using shoppingcartwebservice.Controllers;
using Cart.Business.Interface;
using DomainLayer.Entities;
using System.Threading.Tasks;
using System;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using DataObject.Cart.Entities;

namespace YourNamespace.Tests
{
    public class CartControllerTests
    {
        private readonly ICartBusiness _cartBusiness;
        private readonly ILogger<CartController> _logger;
        private readonly IConfiguration _configuration;

        public CartControllerTests()
        {
            _cartBusiness = Substitute.For<ICartBusiness>();
            _logger = Substitute.For<ILogger<CartController>>();
            _configuration = Substitute.For<IConfiguration>();
        }

        [Fact]
        public async Task GetCart_ReturnsOk()
        {
            // Arrange
            int userId = 1;
            var expectedCart = new CartBasket();
            var controller = new CartController(_configuration, _cartBusiness, _logger);
            _cartBusiness.GetCartAsync(Arg.Any<int>()).Returns(Task.FromResult<ApiResponses<CartBasket>>(new ApiResponses<CartBasket>
            {
                Status = HttpStatusCode.OK,
                Message = "Cart is retrieved",
                Result = expectedCart
            }));


            // Act
            var result = await controller.Getcart(userId) as OkObjectResult;

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<OkObjectResult>();
            result?.StatusCode.Should().Be(200);
            result?.Value.Should().BeEquivalentTo(expectedCart, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task AddToCart_ReturnsOk()
        {
            // Arrange
            int userId = 1;
            var cart = new CartBasket();
            var expectedCart = new CartBasket();
            var controller = new CartController(_configuration, _cartBusiness, _logger);
            _cartBusiness.AddToCartAsync(Arg.Any<CartBasket>(), Arg.Any<int>()).Returns(Task.FromResult<ApiResponses<CartBasket>>(new ApiResponses<CartBasket>
            {
                Status = HttpStatusCode.OK,
                Message = " items were added to cart",
                Result = expectedCart
            }));


            // Act
            var result = await controller.AddToCart(cart, userId) as OkObjectResult;

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<OkObjectResult>();
            result?.StatusCode.Should().Be(200);
            result?.Value.Should().BeEquivalentTo(expectedCart, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task DeleteCart_ReturnsOk()
        {
            // Arrange
            int cartId = 1;
            var controller = new CartController(_configuration, _cartBusiness, _logger);

            // Act
            var result = await controller.DeleteCart(cartId) as OkResult;

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<OkResult>();
            result?.StatusCode.Should().Be(200);
        }
    }
}

