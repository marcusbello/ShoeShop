using System;
using ShoeShop.Dto.Product;

namespace ShoeShop.Dto.Category;

public class CategoryWithProductsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public List<ProductDto> Products { get; set; } = new();
}
