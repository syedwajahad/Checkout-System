namespace offers.DataObjects.DataModels
{
    public  class Offer
    {
        public int OfferId { get; set; }
        public string OfferName { get; set; }
        public string OfferType { get; set; }
        public int BuyUnits { get; set; }
        public int GetUnits { get; set; }
        public Product Product { get; set; }
        public int GetProductId { get; set; }
        public double Discount { get; set; }
        public bool IsExpired { get; set; }
        public bool IsUnlimited { get; set; }
        public bool IsApplied { get; set; }       
    }
}

