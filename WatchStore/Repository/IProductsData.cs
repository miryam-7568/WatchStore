using Entities;

namespace Repository
{
    public interface IProductsData
    {
        Task<List<Product>> GetProducts();
    }
}