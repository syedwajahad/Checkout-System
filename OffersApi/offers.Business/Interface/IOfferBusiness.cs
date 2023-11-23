using offers.DataObjects.DataModels;
using Offers.DataObjects.DataModels;

namespace offers.Business.Interface
{
    public  interface IOfferBusiness
    {
        public Task<ApiResponse<List<Offer>>> GetOffer(int productId);
        public Task<ApiResponse<int>> DeleteOffer(int OfferId);
    }
}
