﻿using Entities;

namespace Business
{
    public interface IProductsServices
    {
        Task<List<Product>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}