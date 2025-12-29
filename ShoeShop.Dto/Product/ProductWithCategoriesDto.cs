using System;
using ShoeShop.Dto.Category;

namespace ShoeShop.Dto.Product;

public class ProductWithCategoriesDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public bool IsNew { get; set; }
    public bool IsFeatured { get; set; }
    public string? Sku { get; set; }
    public string? Sizes { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImageUrls { get; set; }
    public List<CategoryDto> Categories { get; set; } = new();
}
