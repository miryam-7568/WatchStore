using Entities;

namespace Repository
{
    public interface IProductsData
    {
        Task<List<Product>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}