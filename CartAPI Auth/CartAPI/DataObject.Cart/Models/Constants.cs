namespace DataObject.Cart.Models
{
    public class Constants
    {
        public static readonly string GetCart = $@"SELECT o.*, oi.productId AS ProductId, oi.quantity AS Quantity,  oi.price, p.productname , p.description, off1.*
                    FROM cart o
                    LEFT JOIN cartItems oi ON o.CartId = oi.CartId
                    LEFT JOIN cartOffers oo ON o.CartId = oo.CartId
					left join product p on p.productId = oi.productId
					left join offers off1 on off1.offerid = oo.offerId
                    WHERE o.CartId = @CartId";
        public static readonly string GetProductbyId = $"select * FROM Product where productId=@productId";
        public static readonly string InsertCartItems = $"Insert into CartItems values(@CartId,@productId,@productQuantity,@price)";
        public static readonly string DeleteCart = $"DELETE FROM CARTITEMS WHERE CartId=@CartId";
        public static readonly string CheckQuantity = $"select quantity from product where productid = @id";
        public static readonly string InsertCartOffers = $"insert into cartOffers values(@CartId, @offerId)";
        public static readonly string GetCartIdByUserId = $"select cartid from cart where userid = @userId";
    }
}
