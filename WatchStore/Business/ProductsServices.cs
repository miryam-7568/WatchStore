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
    public class ProductsServices : IProductsServices
    {
        IProductsData _productsData;
        private readonly IMapper _mapper;
        public ProductsServices(IProductsData productsData, IMapper mapper)
        {
            _productsData = productsData;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            List<Product> products = await _productsData.GetProducts(desc,  minPrice, maxPrice, categoryIds);
            return _mapper.Map<List<ProductDto>>(products);
            //List<ProductDto> productDtos = new List<ProductDto>();
            //foreach (var item in products)
            //{
            //    productDtos.Add(_mapper.Map<ProductDto>(item));
            //}
            //return productDtos;
        }
    }
}
