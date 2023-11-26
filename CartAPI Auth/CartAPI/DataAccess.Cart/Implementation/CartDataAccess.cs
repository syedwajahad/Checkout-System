using Dapper;
using DataAccess.Cart.Interface;
using DataObject.Cart.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Cart.Implementation
{
    public class CartDataAccess : ICartDataAccess
    {
        private readonly IConfiguration _config;
        public readonly SqlConnection _connection = new();
        public CartDataAccess(IConfiguration config)
        {
            _config = config;
            _connection = new SqlConnection(_config.GetConnectionString("SqlConnection"));
        }

        /// <summary>
        /// Retrieves a shopping cart for a specified User identifier, including associated Products and offers
        /// </summary>
        /// <param name="userId">The identifier of the User for whom the shopping cart is retrieved</param>
        /// <returns>CartBasket</returns>
        public async Task<CartBasket> GetCartAsync(int userId)
        {
            try
            {
                var cartId = await _connection.ExecuteScalarAsync<int>(Constants.GetCartIdByUserId, new { userId });
                var cartDictionary = new Dictionary<int, CartBasket>();
                var result = await _connection.QueryAsync<CartBasket, Product, Offer, CartBasket>(
                   Constants.GetCart,
                   (CartBasket, Product, Offer) => ProcessCartItems(CartBasket, Product, Offer, cartId),
                   new { cartId },
                   splitOn: "ProductId, OfferId");
                return result.First();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (_connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                }
            }
        }

        /// <summary>
        /// Retrieves product details based on the specified product identifier
        /// </summary>
        /// <param name="productId">The identifier of the product to be retrieved</param>
        /// <returns>Product</returns>
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            try
            {
                var product = await _connection.QueryFirstAsync<Product>(Constants.GetProductbyId, new { productId });
                return product;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (_connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                }
            }
        }

        /// <summary>
        /// Deletes the shopping cart associated with the specified User identifier
        /// </summary>
        /// <param name="userId">The identifier of the User whose shopping cart is to be deleted</param>
        public async Task DeleteCartAsync(int userId)
        {
            try
            {
                var cartId = _connection.ExecuteScalarAsync<int>(Constants.GetCartIdByUserId, new { userId });
                await _connection.ExecuteAsync(Constants.DeleteCart, new { cartId });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (_connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                }
            }
        }

        /// <summary>
        /// Checks the available quantity of a product in the inventory
        /// </summary>
        /// <param name="product">The product for which the quantity is to be checked</param>
        /// <returns>quantity</returns>
        public async Task<int> CheckQuantityAsync(Product product)
        {
            try
            {
                var command = await _connection.ExecuteScalarAsync<int>(Constants.CheckQuantity, new { product.ProductId });
                return command;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (_connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                }
            }
        }

        /// <summary>
        /// Inserts items into a shopping cart, updating the cart and applying offers
        /// </summary>
        /// <param name="cart">The shopping cart containing Products and offers</param>
        /// <param name="userid">The User ID associated with the cart</param>
        /// <returns>CartBasket</returns>
        public async Task<CartBasket> InsertCartItemsAsync(CartBasket cart, int userId)
        {
            try
            {
                if (cart.Products != null)
                {
                    foreach (var product in cart.Products)
                    {
                        var cartId = await _connection.ExecuteScalarAsync<int>(Constants.GetCartIdByUserId, new { userId });
                        cart.CartId = cartId;
                        await _connection.ExecuteAsync(Constants.DeleteCart, new { cartId });
                        var command = await _connection.ExecuteScalarAsync<int>(Constants.CheckQuantity, new { id = product.ProductId });
                        if (command >= product.Quantity)
                        {
                            var productInfo = new
                            {
                                productId = product.ProductId,
                                productQuantity = product.Quantity,
                                price = product.Price,
                                cartId,
                            };
                            var output = await _connection.ExecuteAsync(Constants.InsertCartItems, productInfo);
                            if (output == 0)
                            {
                                throw new Exception("Insertion failed");
                            }
                        }
                        else
                        {
                            throw new Exception("One Or More Products are out of stock");
                        }
                    }
                    if (cart.ApplicableOffers != null)
                    {
                        foreach (var offer in cart.ApplicableOffers)
                        {
                            var offerInfo = new
                            {
                                cartId = cart.CartId,
                                offerId = offer.OfferId
                            };
                            var res = await _connection.ExecuteAsync(Constants.InsertCartOffers, offerInfo);
                            if (res == 0)
                            {
                                return cart;
                            }
                        }
                    }
                    return cart;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return cart;

        }

        /// <summary>
        /// Processes and updates a CartBasket by adding Products and applicable offers
        /// </summary>
        /// <param name="CartBasket">The current CartBasket being processed</param>
        /// <param name="product">The product to be added to the CartBasket</param>
        /// <param name="offer">The offer to be added to the CartBasket's ApplicableOffers</param>
        /// <param name="cartId">The identifier of the cart for dictionary tracking</param>
        /// <returns>CartBasket</returns>
        public CartBasket ProcessCartItems(CartBasket cartBasket, Product product, Offer offer, int cartId)
        {
            try
            {
                var cartDictionary = new Dictionary<int, CartBasket>();

                if (!cartDictionary.TryGetValue(cartId, out var existingCart))
                {
                    existingCart = cartBasket;
                    existingCart.Products = new List<Product>();
                    existingCart.ApplicableOffers = new List<Offer>();
                    cartDictionary.Add(existingCart.CartId, existingCart);
                }
                if (product != null && existingCart.Products != null)
                {
                    existingCart.Products.Add(product);
                }
                if (offer != null && existingCart.ApplicableOffers != null)
                {
                    existingCart.ApplicableOffers.Add(offer);
                }
                return existingCart;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
