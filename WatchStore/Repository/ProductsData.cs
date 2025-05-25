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

        public async Task<List<Product>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            try
            {
                var query = _ShopDB327742698Context.Products.Include(product=>product.Category).Where(product =>
                (desc == null ? (true) : (product.Description.Contains(desc)))
                && ((minPrice == null) ? (true) : (product.Price >= minPrice))
                && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
                && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CategoryId))))
                    .OrderBy(product => product.Price);

                List<Product> products = await query.ToListAsync();
                return products;

            }
            catch (Exception ex)
            {
                throw new CustomApiException(500, "Error reading products from DB " + ex.Message);
            }
        }
    }
}
