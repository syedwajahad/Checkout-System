using Catalog.infrastructure.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.infrastructure.Interface;
using CatalogAPI.Entities;

namespace Catalog.Business.Repository
{
    public class CatalogRepository:ICatalog
    {
        private readonly IData _data;

        public CatalogRepository(IData data)
        {
            _data=data;
        }


        public async Task <List<Products>> GetAllItems()
        {
            try
            {

                var res = await _data.GetAllItems();
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }
        

        public async Task<Products> GetItemById(int id)
        {
            try
            {
                var res = await _data.GetItemById(id);
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

        public async  Task<Products> AddItem(Products newItem)
        {
            try
            {

                var res = await _data.AddItem(newItem);
                return res;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message); 
            }
        }

        public async Task<Products>UpdateItem(Products updatedItem)
        {
            var res = await _data.UpdateItem(updatedItem);
            return res;
        }

        public async Task<string> DeleteItem(int id)
        {
            var res = await _data.DeleteItem(id);
            return res;
        }
    }
}
