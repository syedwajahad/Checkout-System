using CartDataAccessLayer.Implementation;
using DomainLayer.Entities;

namespace cartApplication
{
    public class CartLogic 
    {
        private readonly ICartDataAccess _cartDataAccess;
        public CartLogic(ICartDataAccess cartDataAccess)
        {
            _cartDataAccess = cartDataAccess;
        }

        /// <summary>
        /// Applies the "Buy M Get N" offer to a product and adds the free Products to the shopping cart
        /// </summary>
        /// <param name="product">The product to which the offer is applied</param>
        /// <param name="newcart">The shopping cart to which the free Products are added</param>
        /// <returns>List of Products</returns>
        public async Task<List<Product>> ApplyBuyMGetNProductsAsync(Product product, CartBasket newcart)
        {
            try
            {
                var products = new List<Product>(); 
                if (product.Offer!=null&& product.Offer.IsUnlimited == true)
                {
                    var freeProduct = await _cartDataAccess.GetProductByIdAsync(product.Offer.GetProductId);
                    freeProduct.Quantity = product.Quantity * product.Offer.GetUnits;
                    freeProduct.IsFreeProduct = true;
                    products.Add(freeProduct);
                }
                else
                {
                    if (product.Offer != null)
                    {
                        var freeProduct = await _cartDataAccess.GetProductByIdAsync(product.Offer.GetProductId);
                        freeProduct.IsFreeProduct = true;
                        freeProduct.Quantity = product.Offer.GetUnits;
                        products.Add(freeProduct);
                    }
                }
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adds Products to a shopping cart and applies applicable offers based on product quantities and offer types
        /// </summary>
        /// <param name="cart">The original shopping cart containing Products</param>
        /// <returns>CartBasket</returns>
        public async Task<CartBasket> AddToCartAsync(CartBasket cart)
        {
            try
            {
                var newCart = new CartBasket();
                var applicableOffers = new List<Offer>();
                Dictionary<int, bool> productsDictionary = new Dictionary<int, bool>();
                newCart.Products = new List<Product>();
                newCart.ApplicableOffers = applicableOffers;
                double totalCount = 0;
                if (cart.Products != null)
                {
                    for (int i = 0; i < cart.Products.Count; i++)
                    {
                        var product = cart.Products[i];

                        if (product.Offer != null && product.Offer.IsExpired != true)
                        {
                            if (product.Quantity >= product.Offer.BuyUnits)
                            {
                                if (product.Offer.OfferType == "BUY_M_GET_N")
                                {
                                    var res = await ApplyBuyMGetNProductsAsync(product, newCart);
                                    res.Insert(1, product);
                                    newCart.Products = res;
                                    totalCount += product.Quantity * Math.Abs(product.Price - product.ProductDiscount);
                                    applicableOffers.Add(product.Offer);
                                }
                                else
                                {
                                    await ApplyDiscountedPriceAsync(product, newCart, applicableOffers, totalCount);
                                }
                            }
                            else
                            {
                                newCart.Products.Add(product);
                                totalCount += product.Quantity * Math.Abs(product.Price - product.ProductDiscount);
                            }
                        }
                        else
                        {
                            newCart.Products.Add(product);
                            totalCount += product.Quantity * Math.Abs(product.Price - product.ProductDiscount);
                        }
                    }
                }

                newCart.ApplicableOffers = applicableOffers;
                newCart.Totalprice = totalCount;
                return newCart;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Apply discounted price to a product in a shopping cart based on applicable offers.
        /// </summary>
        /// <param name="product">The product to which the discount is applied.</param>
        /// <param name="newCart">The shopping cart containing the Products</param>
        /// <param name="applicableOffers">List of applicable offers</param>
        /// <param name="totalCount">The total count used for discount calculation.</param>
        public async Task ApplyDiscountedPriceAsync(Product product, CartBasket newCart, List<Offer> applicableOffers, double totalCount)
        {
            try
            {
                if (product.Offer != null)
                {
                    var offerProduct = await _cartDataAccess.GetProductByIdAsync(product.Offer.GetProductId);
                    if (offerProduct != null)
                    {
                        var index = IsProductPresentInCart(offerProduct, newCart);
                        if (index != -1 && newCart.ApplicableOffers != null && newCart.Products!=null)
                        {
                            offerProduct.Quantity = newCart.Products[index].Quantity;
                            newCart.Products[index].ProductDiscount = newCart.Products[index].ProductDiscount + Math.Abs(newCart.Products[index].ProductDiscount - product.Offer.Discount);
                            product.Offer.Discount = Math.Min(product.Quantity, offerProduct.Quantity) * Math.Abs(newCart.Products[index].Price - newCart.Products[index].ProductDiscount);
                            newCart.ApplicableOffers.Add(product.Offer);
                            newCart.Products.Add(product);
                            totalCount += product.Quantity * Math.Abs(product.Price - product.ProductDiscount);
                        }
                        else
                        {
                            if (product.ProductId == offerProduct.ProductId && newCart.Products!=null)
                            {
                                product.ProductDiscount = offerProduct.ProductDiscount + Math.Abs(offerProduct.ProductDiscount - product.Offer.Discount);
                                product.Offer.Discount = Math.Min(product.Quantity, offerProduct.Quantity) * Math.Abs(product.ProductDiscount);
                                newCart.Products.Add(product);
                                totalCount += product.Quantity * Math.Abs(product.Price - product.ProductDiscount);
                            }
                            else
                            {
                                if (newCart.Products != null)
                                {
                                    newCart.Products.Add(product);
                                    totalCount += product.Quantity * Math.Abs(product.Price - product.ProductDiscount);
                                }
                            }
                            if (newCart.ApplicableOffers != null)
                            {
                                newCart.ApplicableOffers.Add(product.Offer);
                            }
                        }
                    }
                }
            }
            catch(Exception ex) { 
                throw new Exception(ex.Message);
            };
        }

        /// <summary>
        /// Checks if a specified product is present in the given shopping cart.
        /// </summary>
        /// <param name="product">The product to be checked for presence in the cart.</param>
        /// <param name="cart">The shopping cart to search for the product.</param>
        /// <returns> The index of the product</returns>
        public int IsProductPresentInCart(Product product, CartBasket cart)
        {
            if (cart.Products != null)
            {
                return cart.Products.FindIndex((p) => p.ProductId == product.ProductId);
            }
            else
            {
                return 0;
            }
        }
    }
}