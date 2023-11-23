using Cart.Business.Interface;
using DomainLayer.Entities;
using CartDataAccessLayer.Implementation;
using cartApplication;
using DataObject.Cart.Entities;
using System.Net;

namespace Cart.Business.Implementation
{
    public class CartBusiness : ICartBusiness
    {
        private readonly ICartDataAccess _cartDataAccess;
        public CartBusiness(ICartDataAccess cartdataaccess)
        {
            _cartDataAccess = cartdataaccess;
        }
        
        /// <summary>
        /// Adds Products to the User cart and inserts CartItems into the database
        /// </summary>
        /// <param name="cartBasket">The shopping cart containing Products</param>
        /// <param name="userId">The unique identifier of the User</param>
        /// <returns>CartBasket</returns>
        public async Task<ApiResponses<CartBasket>> AddToCartAsync(CartBasket cartBasket, int userId)
        {
            try
            {
                CartLogic obj = new CartLogic(_cartDataAccess);
                var newCart = await obj.AddToCartAsync(cartBasket);
                var res = await _cartDataAccess.InsertCartItemsAsync(newCart, userId);
                var response = new ApiResponses<CartBasket>();
                if(res != null)
                {
                    response.Status = HttpStatusCode.OK;
                    response.Result = res;
                    response.Message = "Products are Added to Cart";
                }
                else
                {
                    response.Status = HttpStatusCode.NoContent;
                    response.Result = res;
                    response.Message = "Products are not added to cart";
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception:{ex.Message}");
            }

        }

        /// <summary>
        /// Deletes CartItems from the database before Inserting new CartItems
        /// </summary>
        /// <param name="cartId">The unique identifier of the shopping cart</param>
        public async Task DeleteCartAsync(int cartId)
        {
            try
            {
                await _cartDataAccess.DeleteCartAsync(cartId);   
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception:{ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a User shopping cart from the database based on the User's unique identifier
        /// </summary>
        /// <param name="userId">The unique identifier of the User</param>
        /// <returns>CartBasket</returns>
        public async Task<ApiResponses<CartBasket>> GetCartAsync(int userId)
        {
            try
            {
                var output = await _cartDataAccess.GetCartAsync(userId);
                var response = new ApiResponses<CartBasket>();
                if (output == null)
                {
                    response.Status = HttpStatusCode.NoContent;
                    response.Result = output;
                    response.Message = "Cart is not retrieved";
                }
                else
                {
                    response.Status = HttpStatusCode.OK;
                    response.Result = output;
                    response.Message = "Cart retrieved";
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception:{ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a product from the database based on its unique identifier
        /// </summary>
        /// <param name="productId">The unique identifier of the product to retrieve</param>
        /// <returns>Product</returns>
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            try
            {
                var output = await _cartDataAccess.GetProductByIdAsync(productId);
                return output;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception:{ex.Message}");
            }
        }

        /// <summary>
        /// Inserts shopping cart items into the database for a specified User
        /// </summary>
        /// <param name="cartBasket">The shopping cart containing items to be inserted</param>
        /// <param name="userId">The unique identifier of the User</param>
        /// <returns>true/false</returns>
        public async Task<CartBasket> InsertCartItemsAsync(CartBasket cartBasket, int userId)
        {
            await _cartDataAccess.DeleteCartAsync(userId);
            try
            {
                var res = await _cartDataAccess.InsertCartItemsAsync(cartBasket, userId);
                return res;
            }
            catch (Exception)
            {
                return new CartBasket();
            }

        }
    }
}
