using CatalogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.infrastructure.IDataAccess
{
    public interface IData
    {
       public Task <List<Products>> GetAllItems();
       public Task<Products> GetItemById(int id);
       Products GetItemByName(string name);
        public Task<Products> AddItem(Products newItem);
        public Task<Products> UpdateItem(Products updatedItem);
        public Task<string> DeleteItem(int id);
    }
}
