using Entities;

namespace Repository
{
    public interface ICategoriesData
    {
        Task<List<Category>> GetCategories();
    }
}