using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.infrastructure.IDataAccess;
using CatalogAPI.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Catalog.infrastructure.DataRepository
{
    public class DataRepository:IData
    {
        private readonly IConfiguration _configuration;
        public readonly SqlConnection _connection=new SqlConnection();

        public DataRepository(IConfiguration configuration)
        {
            _configuration=configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("CatalogDb"));
        }

        public async Task<Products> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Products>> GetAllItems()
        {
            var res = await _connection.QueryAsync<Products>(Queries.getallItems);
            return res.ToList();

        }

        public async Task <Products> GetItemById(int id)
        {
            try
            {
                var res = await _connection.QueryFirstAsync<Products>(Queries.GetItemById, new { productId = id });
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           

        }

        public Products GetItemByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Products> AddItem(Products newItem)
        {
            try
            {

                var res = await _connection.ExecuteScalarAsync<int>(Queries.getLastInsertedId); 
                var insertedProduct = await _connection.ExecuteAsync(Queries.addProduct, new {productId = res+1, productName = newItem.productName, quantity = newItem.quantity, imageUrl = "", description = newItem.Description, isOutOfStock = 0, price = newItem.price});
                newItem.productId = res;
                return newItem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }





        }

        public async Task<Products> UpdateItem(Products updatedItem)
        {
            var res = await _connection.ExecuteAsync(Queries.UpdateItem);
            updatedItem.productId = res;
            return updatedItem;
        }

        public async Task<string> DeleteItem(int id)
        {
            try
            {
                var res = await _connection.ExecuteAsync(Queries.DeleteItemById,new {productId=id});
                return res.ToString() ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }   
        }
    }
}
