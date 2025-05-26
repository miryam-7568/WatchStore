using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class ProductRepositoryIntegrationsTest : IClassFixture<DatabaseFixture>
    {
        private readonly ShopDB327742698Context _context;
        private readonly ProductsData _productsData;

        public ProductRepositoryIntegrationsTest(DatabaseFixture fixture)
        {
            _context = fixture.Context;
            _productsData = new ProductsData(_context);
        }

        [Fact]
        public async Task GetProducts_ReturnsFilteredProducts()
        {
            // Arrange
            var category = new Category { CategoryName = "Electronics" };
            var product1 = new Product { ProductName = "Phone", Description = "Smartphone", Price = 500, CategoryId = 1, Category = category };
            var product2 = new Product { ProductName = "Laptop", Description = "Gaming Laptop", Price = 1500, CategoryId = 1, Category = category };

            _context.Categories.Add(category);
            _context.Products.AddRange(product1, product2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productsData.GetProducts("Phone", 400, 600, new int?[] { 1 });

            // Assert
            Assert.Single(result);
            Assert.Contains(result, p => p.Id == 1);
        }

        [Fact]
        public async Task GetProducts_ReturnsAllProducts_WhenNoFiltersApplied()
        {
            // Arrange
            var category = new Category { CategoryName = "Home Appliances" };
            var product1 = new Product { ProductName = "Vacuum Cleaner", Description = "Powerful vacuum", Price = 300, CategoryId = 2, Category = category };
            var product2 = new Product { ProductName = "Microwave", Description = "Compact microwave", Price = 200, CategoryId = 2, Category = category };

            _context.Categories.Add(category);
            _context.Products.AddRange(product1, product2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productsData.GetProducts(null, null, null, Array.Empty<int?>());

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetProducts_ReturnsEmptyList_WhenNoProductsMatchFilters()
        {
            // Arrange
            var category = new Category { CategoryName = "Books" };
            var product = new Product { ProductName = "Novel", Description = "Fiction book", Price = 20, CategoryId = 3, Category = category };

            _context.Categories.Add(category);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productsData.GetProducts("Nonexistent", 100, 200, new int?[] { 99 });

            // Assert
            Assert.Empty(result);
        }
    }
}
