using DataObject.Cart.Entities;
using DomainLayer.Entities;

namespace Cart.Business.Interface
{
    public interface ICartBusiness
    {
        public Task<ApiResponses<CartBasket>> GetCartAsync(int userId);
        public Task<Product> GetProductByIdAsync(int productId);
        public Task<ApiResponses<CartBasket>> AddToCartAsync(CartBasket cartBasket, int userId);
        public Task  DeleteCartAsync(int cartId);
        public Task<CartBasket> InsertCartItemsAsync(CartBasket cartBasket,int cartId);
       
    }
}
