namespace DataObject.Cart.Models
{
    public class CartBasket
    {
        public int CartId { get; set; }
        public List<Product>? Products { get; set; }
        public List<Offer>? ApplicableOffers { get; set; }
        public double Totalprice { get; set; }
    }
}
