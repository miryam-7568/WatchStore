using DTOs;
using Entities;

namespace Business
{
    public interface ICategoriesServices
    {
        Task<List<CategoryDto>> GetCategories();
    }
}