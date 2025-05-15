using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly ICategoriesData _categoriesData;

        public CategoriesServices(ICategoriesData categoriesData)
        {
            _categoriesData = categoriesData;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _categoriesData.GetCategories();
        }
    }
}
