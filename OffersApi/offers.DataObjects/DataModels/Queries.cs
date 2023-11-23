namespace offers.DataObjects.DataModels
{
    public  class Queries
    {
        public static string GetOffers = $@"SELECT * FROM offers o right JOIN                                  
                                         Product p ON o.ProductId = p.ProductId
                                         where P.ProductId= @ProductId;";
        public static string RemoveOffers = $"DELETE FROM offers  where OfferId = @OfferId";
        public static string CheckOffers = $"SELECT * from offers where ProductId = @ProductId ";
    }
}

