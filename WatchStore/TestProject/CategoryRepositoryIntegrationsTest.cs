using Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class CategoryRepositoryIntegrationsTest : IClassFixture<DatabaseFixture>
    {
        private readonly ShopDB327742698Context _shopDBcontext;
        private readonly CategoriesData _categoriesData;
        public CategoryRepositoryIntegrationsTest(DatabaseFixture databaseFixture)
        {
            _shopDBcontext = databaseFixture.Context;
            _categoriesData = new CategoriesData(_shopDBcontext);
        }

        [Fact]
        public async Task GetCategories_ReturnsAllCategories()
        {
            // Arrange
            var category1 = new Category { CategoryName = "Electronics" };
            var category2 = new Category { CategoryName = "Books" };
            var category3 = new Category { CategoryName = "Clothing" };

            _shopDBcontext.Categories.AddRange(category1, category2, category3);
            await _shopDBcontext.SaveChangesAsync();

            // Act
            var result = await _categoriesData.GetCategories();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Contains(result, c => c.CategoryName == "Electronics");
            Assert.Contains(result, c => c.CategoryName == "Books");
            Assert.Contains(result, c => c.CategoryName == "Clothing");
        }
    }
}
