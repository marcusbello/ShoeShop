using System;
using ShoeShop.Entities;
using ShoeShop.DataContext;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Dto.Category;
using AutoMapper;

namespace ShoeShop.WebApi.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ShoeshopDbContext _context;
    private readonly IMapper _mapper;

    public CategoryRepository(ShoeshopDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoryDto?> CreateAsync(CreateCategoryDto c)
    {
        // Map DTO to Entity
        var categoryEntity = _mapper.Map<Category>(c);

        await _context.Categories.AddAsync(categoryEntity);
        await _context.SaveChangesAsync();
        return categoryEntity != null ? _mapper.Map<CategoryDto>(categoryEntity) : null;
    }

    public async Task<bool?> DeleteAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<CategoryDto>> RetrieveAllAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryWithProductsDto?> RetrieveAsync(Guid id)
    {
        var category = await _context.Categories
             .Include(c => c.Products)
             .FirstOrDefaultAsync(c => c.Id == id);

        return category == null
            ? null
            : _mapper.Map<CategoryWithProductsDto>(category);
    }

    public async Task<CategoryDto?> UpdateAsync(Guid id, UpdateCategoryDto c)
    {
        var existingCategory = await _context.Categories.FindAsync(id);
        if (existingCategory == null)
            return null;

        existingCategory.Name = c.Name;
        _context.Categories.Update(existingCategory);
        await _context.SaveChangesAsync();
        return _mapper.Map<CategoryDto>(existingCategory);
    }
}
