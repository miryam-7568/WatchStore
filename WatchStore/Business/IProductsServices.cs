using DTOs;
using Entities;

namespace Business
{
    public interface IProductsServices
    {
        Task<List<ProductDto>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}