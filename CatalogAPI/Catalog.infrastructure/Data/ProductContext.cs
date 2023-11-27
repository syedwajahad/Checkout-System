using CatalogAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI
{
    public class ProductContext:DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        public DbSet<Products> Products { get; set; }
    }
}
