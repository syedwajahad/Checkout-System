using Dapper;
using Microsoft.Extensions.Configuration;
using offers.DataObjects.DataModels;
using offers.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.DataAccess.Implementation
{
    public class OfferDataAccess : IOfferDataAccess
    {
        private readonly IConfiguration _config;
        private readonly SqlConnection _connection;
        public OfferDataAccess(IConfiguration config)
        {
            _config = config;
            _connection = new SqlConnection(_config.GetConnectionString("SQLConnection"));
        }
        /// <summary>
        /// Retrieves a list of offers associated with a specific product identified by its unique identifier.
        /// </summary>
        /// <param name="ProductId">The unique identifier of the product for which to retrieve offers.</param>
        /// <returns>
        /// The method queries the database to check the existence of the specified product.
        /// If the product is found, a list of offers related to the product, including detailed product information,
        /// is retrieved asynchronously.
        /// </returns>
        public async Task<List<Offer>> GetOffer(int ProductId)
        {
            try
            {
                var isProductExist = await _connection.QueryFirstAsync<Offer>(Queries.CheckOffers, new { ProductId = ProductId });
                if(isProductExist != null)
                {
                    throw new Exception("Product not found");
                }
                else
                {
                    var result = await _connection.QueryAsync<Offer, Product, Offer>(
                    Queries.GetOffers,
                    (offer, product) =>
                    {
                        offer.Product = product;
                        return offer;
                    },
                     new { ProductId },
                    splitOn: "ProductId"
                    );
                    return (List<Offer>)result;
                }              
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception message : {ex.Message}");
            }
           
        }
        /// <summary>
        /// Deletes an offer from the database based on the provided OfferId.
        /// </summary>
        /// <param name="OfferId">The identifier of the offer to be deleted.</param>
        /// <returns>
        /// An integer representing the number of affected rows in the database.
        /// </returns>
        public async Task<int> DeleteOffer(int OfferId)
        {
            try
            {
                return await _connection.ExecuteAsync(Queries.RemoveOffers, new { OfferId });
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception message : {ex.Message}");
            }
        }
    }
}
