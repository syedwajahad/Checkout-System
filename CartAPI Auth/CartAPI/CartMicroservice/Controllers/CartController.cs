using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Cart.Business.Interface;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using DataObject.Cart.Entities;

namespace shoppingcartwebservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBusiness _cartBusiness;
        private readonly ILogger<CartController> _logger;
        private readonly IConfiguration _configuration;

        public CartController(IConfiguration configuration, ICartBusiness cartBusiness, ILogger<CartController> logger)
        {
            _cartBusiness = cartBusiness;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Retrieves the shopping cart for a specified User
        /// </summary>
        /// <param name="userId">The identifier of the User for whom the shopping cart is retrieved</param>
        [HttpGet("cart")]
        [Authorize]
        public async Task<IActionResult> Getcart(int userId)
        {
            try
            {
                var response = await _cartBusiness.GetCartAsync(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception occurred: {ex.Message}");
                return StatusCode(500, "Internal Server Error: Unable to retrieve cart.");
            }
        }

        /// <summary>
        /// Adds Products to the shopping cart for a specified User
        /// </summary>
        /// <param name="cart">The shopping cart containing Products to be added</param>
        /// <param name="userId">The identifier of the User for whom the Products are added to the cart</param>
        [HttpPost("AddtoCart")]
        [Authorize]
        public async Task<IActionResult> AddToCart(CartBasket cart, int userId)
        {
            try
            {
                var response = await _cartBusiness.AddToCartAsync(cart, userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred: {ExceptionMessage}", ex.Message);
                return StatusCode(500, "Internal Server Error: Unable to add items to cart.");
            }
        }
        // <summary>
        /// Deletes items from the shopping cart for a specified cart identifier.
        /// </summary>
        /// <param name=cartId</param>
        [HttpDelete("{CartId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCart(int cartId)
        {
            try
            {
                await _cartBusiness.DeleteCartAsync(cartId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred: {ExceptionMessage}", ex.Message);
                return StatusCode(500, "Internal Server Error: Unable to delete items in cart.");
            }
        }

    }
}
