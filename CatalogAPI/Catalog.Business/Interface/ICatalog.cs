using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogAPI.Entities;

namespace Catalog.infrastructure.Interface
{
    public interface ICatalog
    {
        public Task<List<Products>> GetAllItems();
       public Task <Products>GetItemById(int id);
        Products GetItemByName(string name);
        public Task<Products> AddItem(Products newItem);
        public Task<Products> UpdateItem(Products updatedItem);
        public Task <string>  DeleteItem(int id);
    }
}
