using System;
using ShoeShop.Dto.Category;
using ShoeShop.Entities;

namespace ShoeShop.WebApi.Repositories;

public interface ICategoryRepository
{
    Task<CategoryDto?> CreateAsync(CreateCategoryDto c);
    Task<IEnumerable<CategoryDto>> RetrieveAllAsync();
    Task<CategoryWithProductsDto?> RetrieveAsync(Guid id);
    Task<CategoryDto?> UpdateAsync(Guid id, UpdateCategoryDto c);
    Task<bool?> DeleteAsync(Guid id);

}
