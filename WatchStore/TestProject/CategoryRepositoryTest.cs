using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace TestProject
{
    public class CategoryRepositoryTest
    {
        [Fact]
        public async Task GetCategories_returnsCategories()
        {
            var category1= new Category { Id = 1, CategoryName = "a" };
            var category2 = new Category { Id = 2, CategoryName = "b" };
            var category3 = new Category { Id = 3, CategoryName = "c" };
            var categories = new List<Category>() {category1, category2, category3 };

        }
    }
}
