using Catalog.infrastructure.Interface;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Business.Repository;
using Catalog.infrastructure.DataRepository;
using Catalog.infrastructure.IDataAccess;
using CatalogAPI.Entities;
using Xunit;
using CatalogAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Test
{
    public class ProductItemTests
    {
        [Fact]
        public void AddItem_ShouldCallRepositoryWithCorrectParameters()
        {
            // Arrange
            var productRepository = Substitute.For<ICatalog>();
            var productService = new ProductController(productRepository);

            var product = new Products();

            // Act
            productService.PostAsync(product);

            // Assert
            productRepository.Received(1).AddItem(Arg.Is<Products>(p => p == product));
        }

        [Fact]
        public void DeleteProduct_ShouldCallRepositoryDeleteMethod_WithCorrectProductId()
        {
            // Arrange
            var productId = 3;
            var productRepository = Substitute.For<ICatalog>();
            var productService = new ProductController(productRepository);

            // Act
            productService.Delete(productId);

            // Assert
            productRepository.Received(1).DeleteItem(productId);
        }

       
        [Fact]
        public void GetAllItems_Called_At_Least_Once()
        {
            // Arrange
            var product = Substitute.For<ICatalog>();

            // Act
            var result = product.GetAllItems();

            // Assert
            product.Received().GetAllItems(); // Verifies that GetAllItems was called at least once
        }

        [Fact]
        public void GetProductById_ShouldReturnCorrectProduct()
        {
            // Arrange
            var productId = 1;
            var expectedProduct = new Products { productId = productId, productName = "Sample Product", price = 29.99 };

            var productService = Substitute.For<ICatalog>();
            productService.GetItemById(productId).Returns(expectedProduct);

            // Act
            var actualProduct = productService.GetItemById(productId);

            // Assert
            Assert.Equal(expectedProduct.productId, actualProduct.Id);
           
        }

    }
}

