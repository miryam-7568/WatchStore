using AutoMapper;
using DTOs;
using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly ICategoriesData _categoriesData;
        private readonly IMapper _mapper;

        public CategoriesServices(ICategoriesData categoriesData, IMapper mapper)
        {
            _categoriesData = categoriesData;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetCategories()
        {
            List<Category> categories = await _categoriesData.GetCategories();
            return _mapper.Map<List<CategoryDto>>(categories);
        }
    }
}
