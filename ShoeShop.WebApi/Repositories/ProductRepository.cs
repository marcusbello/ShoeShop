using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoeShop.DataContext;
using ShoeShop.Dto.Product;
using ShoeShop.Entities;

namespace ShoeShop.WebApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ShoeshopDbContext _context;

    private readonly IMapper _mapper;

    public ProductRepository(ShoeshopDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductWithCategoriesDto?> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name ?? string.Empty,
            Description = dto.Description ?? string.Empty,
            Price = dto.Price,
            Stock = dto.Stock,
            Sku = dto.Sku ?? string.Empty,
            ImageUrl = dto.ImageUrl ?? string.Empty,
            SupplierPrice = dto.SupplierPrice,
            SupplierId = dto.SupplierId,    
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var categories = await _context.Categories
            .Where(c => dto.CategoryIds.Contains(c.Id))
            .ToListAsync();

        product.Categories = categories;

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProductWithCategoriesDto>(product);
    }

    public async Task<IEnumerable<ProductWithCategoriesDto>> RetrieveAllAsync(string? category = null)
    {
        var query = _context.Products
        .Include(p => p.Categories)
        .AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
        {
            var normalized = category.Trim();

            query = query.Where(p =>
                p.Categories.Any(c =>
                    c.Name == normalized));
        }

        var products = await query.ToListAsync();
        return _mapper.Map<IEnumerable<ProductWithCategoriesDto>>(products);
    }

    public async Task<ProductWithCategoriesDto?> RetrieveAsync(Guid id)
    {
        var product = await _context.Products
            .Include(p => p.Categories)
            .FirstOrDefaultAsync(p => p.Id == id);

        return product == null
            ? null
            : _mapper.Map<ProductWithCategoriesDto>(product);
    }

    public async Task<ProductWithCategoriesDto?> UpdateAsync(Guid id, UpdateProductDto p)
    {
        
        var existing = await _context.Products
            .Include(prod => prod.Categories)
            .FirstOrDefaultAsync(prod => prod.Id == id);
        if (existing == null)
            return null;

        existing.Name = p.Name;
        existing.Description = p.Description ?? existing.Description;
        existing.Price = p.Price;
        existing.Stock = p.Stock;
        existing.Sku = p.Sku ?? existing.Sku;
        existing.ImageUrl = p.ImageUrl ?? existing.ImageUrl;
        existing.SupplierPrice = p.SupplierPrice ?? existing.SupplierPrice;
        existing.SupplierId = p.SupplierId ?? existing.SupplierId;
        existing.UpdatedAt = DateTime.UtcNow;

        // Update categories
        var categories = await _context.Categories
            .Where(c => p.CategoryIds.Contains(c.Id))
            .ToListAsync();
        existing.Categories = categories;

        _context.Products.Update(existing);
        await _context.SaveChangesAsync();
        return _mapper.Map<ProductWithCategoriesDto>(existing);
    }

    public async Task<bool?> DeleteAsync(Guid id)
    {
        var existing = await _context.Products.FindAsync(id);
        if (existing == null)
            return false;

        _context.Products.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
