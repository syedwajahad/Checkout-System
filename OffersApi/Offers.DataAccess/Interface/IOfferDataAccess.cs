using offers.DataObjects.DataModels;

namespace offers.DataAccess.Interface
{
    public interface IOfferDataAccess
    {
        public Task<List<Offer>> GetOffer(int productId);
        public Task<int> DeleteOffer(int OfferId);

    }
}
