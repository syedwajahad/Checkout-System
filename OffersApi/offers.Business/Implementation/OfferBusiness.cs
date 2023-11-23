using offers.Business.Interface;
using offers.DataObjects.DataModels;
using offers.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offers.DataObjects.DataModels;
using System.Net;

namespace offers.Business.Implementation
{
    public class OfferBusiness : IOfferBusiness
    {
        private readonly IOfferDataAccess _OfferDataAccess;
        public OfferBusiness(IOfferDataAccess UserDataAccess)
        {
            _OfferDataAccess = UserDataAccess;
        }
        /// <summary>
        /// Deletes an offer with the specified OfferId.
        /// </summary>
        /// <param name="OfferId">The identifier of the offer to be deleted.</param>
        /// <returns>An ApiResponse<int> indicating the status of the delete operation.</returns>
        public async Task<ApiResponse<int>> DeleteOffer(int OfferId)
        {
            try
            {
                var response = new ApiResponse<int>();             
                var output = await _OfferDataAccess.DeleteOffer(OfferId);
                if (output == 0)
                {
                    response.Status = HttpStatusCode.NoContent;
                    response.Message = "Offer not found";
                }
                else
                {
                    response.Status = HttpStatusCode.OK;
                    response.Message = "Deleted offers successfully";
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Retrieves a list of offers for a given product ID.
        /// </summary>
        /// <param name="ProductId">The identifier of the product for which to retrieve offers.</param>
        /// <returns>
        /// The status of the response indicates the success or failure of the operation.
        /// </returns>
        public async  Task<ApiResponse<List<Offer>>> GetOffer(int ProductId)
        {
            try
            {
                var response = new ApiResponse<List<Offer>>();
                var output = await _OfferDataAccess.GetOffer(ProductId);
                if (output == null)
                {
                    response.Status = HttpStatusCode.NoContent;
                    response.Data = null;
                    response.Message = "Offers not found";
                }
                else
                {
                    response.Status = HttpStatusCode.OK;
                    response.Data = output;
                    response.Message = "Retrived offers successfully";
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
