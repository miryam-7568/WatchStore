using Entities;

namespace Business
{
    public interface IProductsServices
    {
        Task<List<Product>> GetProducts();
    }
}