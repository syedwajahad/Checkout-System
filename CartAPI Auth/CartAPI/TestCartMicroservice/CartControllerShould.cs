using Business.Cart.Interface;
using DataObject.Cart.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using shoppingcartwebservice.Controllers;
using System.Net;

namespace CartAPI.Tests
{
    public class CartControllerShould
    {
        private readonly ICartBusiness _cartBusiness;
        private readonly ILogger<CartController> _logger;

        public CartControllerShould()
        {
            _cartBusiness = Substitute.For<ICartBusiness>();
            _logger = Substitute.For<ILogger<CartController>>();
        }

        [Fact]
        public async Task ReturnOkWhenCartIsPresent()
        {
            // Arrange
            int userId = 1;
            var expectedCart = new CartBasket();
            var controller = new CartController(_cartBusiness, _logger);
            _cartBusiness.GetCartAsync(Arg.Any<int>()).Returns(Task.FromResult<ApiResponses<CartBasket>>(new ApiResponses<CartBasket>
            {
                Status = HttpStatusCode.OK,
                Message = "Cart is retrieved",
                Result = expectedCart
            }));


            // Act
            var result = await controller.Getcart(userId) as OkObjectResult;

            // Assert
            result.Should().BeAssignableTo<OkObjectResult>();
            result?.StatusCode.Should().Be(200);
            result?.Value.Should().BeEquivalentTo(expectedCart, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task ReturnOkWhenAddToCartSucceeds()
        {
            // Arrange
            int userId = 1;
            var cart = new CartBasket();
            var expectedCart = new CartBasket();
            var controller = new CartController(_cartBusiness, _logger);
            _cartBusiness.AddToCartAsync(Arg.Any<CartBasket>(), Arg.Any<int>()).Returns(Task.FromResult<ApiResponses<CartBasket>>(new ApiResponses<CartBasket>
            {
                Status = HttpStatusCode.OK,
                Message = " items were added to cart",
                Result = expectedCart
            }));


            // Act
            var result = await controller.AddToCart(cart, userId) as OkObjectResult;

            // Assert
            result.Should().BeAssignableTo<OkObjectResult>();
            result?.StatusCode.Should().Be(200);
            result?.Value.Should().BeEquivalentTo(expectedCart, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task ReturnOkWhenCartIsDeleted()
        {
            // Arrange
            int cartId = 1;
            var controller = new CartController(_cartBusiness, _logger);

            // Act
            var result = await controller.DeleteCart(cartId) as OkResult;

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<OkResult>();
            result?.StatusCode.Should().Be(200);
        }
    }
}

