using System;
using ShoeShop.Dto.Product;
using ShoeShop.Entities;

namespace ShoeShop.WebApi.Repositories;

public interface IProductRepository
{
    Task<ProductWithCategoriesDto?> CreateAsync(CreateProductDto dto);
    Task<IEnumerable<ProductWithCategoriesDto>> RetrieveAllAsync(string? category = null);
    Task<ProductWithCategoriesDto?> RetrieveAsync(Guid id);
    Task<ProductWithCategoriesDto?> UpdateAsync(Guid id, UpdateProductDto p);
    Task<bool?> DeleteAsync(Guid id);

}
