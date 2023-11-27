namespace DataObject.Cart.Models
{
    public class Offer
    {
        public int OfferId { get; set; }
        public int OfferName { get; set; }
        public string? OfferType { get; set; }
        public int BuyUnits { get; set; }
        public int GetUnits { get; set; }
        public int ProductId { get; set; }
        public int GetProductId { get; set; }
        public double Discount { get; set; }
        public bool IsExpired { get; set; }
        public bool IsUnlimited { get; set; }
        public bool IsApplied { get; set; }
    }
}
