using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Catalog.Core.Entities;

namespace CatalogAPI.Entities
{
   
    public class Products
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string Description { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public bool isOutOfStock { get; set; }
        public Offer offer { get; set; }
        public string imageUrl { get; set; }
        public Boolean isFreeProduct { get; set; }

        public double productDiscount { get; set; }

        public Products(Products product)
        {
            this.quantity = product.quantity;
            this.price = product.price;
            this.offer = product.offer;
            this.imageUrl = product.imageUrl;
            this.Description = product.Description;
            this.productId = product.productId;
            this.isOutOfStock = product.isOutOfStock;
            this.productDiscount = product.productDiscount;
            this.isFreeProduct = product.isFreeProduct;
            this.productName = product.productName;
        }
        public Products() { }

    }

    //public Offers? offer { get; set; }
}

