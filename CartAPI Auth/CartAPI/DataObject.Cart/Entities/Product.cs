namespace DomainLayer.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; } 
        public bool IsOutOfStock {  get; set; } 
        public Offer? Offer { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsFreeProduct {  get; set; }
        public double ProductDiscount { get; set; }
    }
}
