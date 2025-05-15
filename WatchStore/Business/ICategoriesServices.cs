using Entities;

namespace Business
{
    public interface ICategoriesServices
    {
        Task<List<Category>> GetCategories();
    }
}