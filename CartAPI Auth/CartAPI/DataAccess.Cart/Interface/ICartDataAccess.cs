using DataObject.Cart.Models;

namespace DataAccess.Cart.Interface
{
    public interface ICartDataAccess
    {
        public Task<CartBasket> GetCartAsync(int userId);
        public Task<Product> GetProductByIdAsync(int productId);
        public Task DeleteCartAsync(int cartId);
        public Task<int> CheckQuantityAsync(Product product);
        public Task<CartBasket> InsertCartItemsAsync(CartBasket cart, int userId);
    }
}
