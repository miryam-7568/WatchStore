using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoriesData : ICategoriesData
    {
        ShopDB327742698Context _ShopDB327742698Context;

        public CategoriesData(ShopDB327742698Context shopDB327742698Context)
        {
            _ShopDB327742698Context = shopDB327742698Context;
        }

        public async Task<List<Category>> GetCategories()
        {
            try
            {
                return await _ShopDB327742698Context.Categories.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new CustomApiException(500, "Error reading categories from DB " + ex.Message);
            }
        }
    }
}
