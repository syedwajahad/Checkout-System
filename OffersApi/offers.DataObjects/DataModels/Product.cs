namespace offers.DataObjects.DataModels
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }  
        public int ProductQuanity { get; set; }
        public double ProductPrice { get; set; }
        public Boolean IsFreeProduct { get; set; }
        public string ImageUrl { get; set; }
        public Boolean IsOutOfStock { get; set; }  
        public double ProductDiscount { get; set; }
        public Offer? Offer { get; set; }
    }
}
