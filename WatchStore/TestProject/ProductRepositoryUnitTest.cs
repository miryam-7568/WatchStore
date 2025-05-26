using AutoMapper;
using DTOs;
using Entities;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class ProductRepositoryUnitTest
    {
        [Fact]
        public async Task GetProducts_ReturnsFilteredProducts()
        {
            // Arrange
            var product1 = new Product { Id = 1, ProductName = "Product A", Description = "Description A", Price = 100, CategoryId = 1 };
            var product2 = new Product { Id = 2, ProductName = "Product B", Description = "Description B", Price = 200, CategoryId = 2 };
            var product3 = new Product { Id = 3, ProductName = "Product C", Description = "Description C", Price = 300, CategoryId = 1 };
            var products = new List<Product> { product1, product2, product3 };

            var mockContext = new Mock<ShopDB327742698Context>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);

            var productsData = new ProductsData(mockContext.Object);

            // Act
            var result = await productsData.GetProducts("C", 150, 300, new int?[] { 1 });

            // Assert
            Assert.Single(result);
            Assert.Contains(result, p => p.Id == 3);
        }

        [Fact]
        public async Task GetProducts_ReturnsAllProducts_WhenNoFiltersApplied()
        {
            // Arrange
            var product1 = new Product { Id = 1, ProductName = "Product A", Description = "Description A", Price = 100, CategoryId = 1 };
            var product2 = new Product { Id = 2, ProductName = "Product B", Description = "Description B", Price = 200, CategoryId = 2 };
            var product3 = new Product { Id = 3, ProductName = "Product C", Description = "Description C", Price = 300, CategoryId = 1 };
            var products = new List<Product> { product1, product2, product3 };

            var mockContext = new Mock<ShopDB327742698Context>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);

            var productsData = new ProductsData(mockContext.Object);

            // Act
            var result = await productsData.GetProducts(null, null, null, Array.Empty<int?>());

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetProducts_ReturnsEmptyList_WhenNoProductsMatchFilters()
        {
            // Arrange
            var product1 = new Product { Id = 1, ProductName = "Product A", Description = "Description A", Price = 100, CategoryId = 1 };
            var product2 = new Product { Id = 2, ProductName = "Product B", Description = "Description B", Price = 200, CategoryId = 2 };
            var product3 = new Product { Id = 3, ProductName = "Product C", Description = "Description C", Price = 300, CategoryId = 1 };
            var products = new List<Product> { product1, product2, product3 };

            var mockContext = new Mock<ShopDB327742698Context>();
            mockContext.Setup(x => x.Products).ReturnsDbSet(products);

            var productsData = new ProductsData(mockContext.Object);

            // Act
            var result = await productsData.GetProducts("Nonexistent", 500, 1000, new int?[] { 99 });

            // Assert
            Assert.Empty(result);
        }
    }
}
