namespace DataObject.Cart.Models
{
    public class Constants
    {
        public static readonly string GET_CART = $@"SELECT o.*, oi.ProductId AS ProductId, oi.Quantity AS Quantity,  oi.Price, p.Productname , p.Description, off1.*
                                                    FROM cart o
                                                    LEFT JOIN cartItems oi ON o.CartId = oi.CartId
                                                    LEFT JOIN cartOffers oo ON o.CartId = oo.CartId
					                                LEFT JOIN product p ON p.ProductId = oi.ProductId
					                                LEFT JOIN offers off1 ON off1.Offerid = oo.OfferId
                                                    WHERE o.CartId = @CartId";
        public static readonly string GET_PRODUCT_BY_ID = $"SELECT * FROM Product WHERE ProductId=@ProductId";
        public static readonly string INSERT_CARTITEMS = $"INSERT INTO CartItems VALUES(@CartId,@ProductId,@ProductQuantity,@Price)";
        public static readonly string DELETE_CART = $"DELETE FROM CartItems WHERE CartId=@CartId";
        public static readonly string CHECK_QUANTITY = $"SELECT Quantity FROM Product WHERE Productid = @Productid";
        public static readonly string INSERT_CARTOFFERS = $"INSERT INTO CartOffers VALUES(@CartId, @OfferId)";
        public static readonly string GET_CARTID_BY_USERID = $"SELECT CartId FROM Cart WHERE UserId = @UserId";
    }
}
