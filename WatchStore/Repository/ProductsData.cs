using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductsData : IProductsData
    {
        ShopDB327742698Context _ShopDB327742698Context;

        public ProductsData(ShopDB327742698Context shopDB327742698Context)
        {
            _ShopDB327742698Context = shopDB327742698Context;
        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                return await _ShopDB327742698Context.Products.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new CustomApiException(500, "Error reading products from DB " + ex.Message);
            }
        }
    }
}
