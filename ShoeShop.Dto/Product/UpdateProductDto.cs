using System;

namespace ShoeShop.Dto.Product;

public class UpdateProductDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public bool? IsNew { get; set; }
    public bool? IsFeatured { get; set; }
    public string? Sku { get; set; }
    public string? Sizes { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImageUrls { get; set; }
    public decimal? SupplierPrice { get; set; }
    public Guid? SupplierId { get; set; }
    public List<Guid> CategoryIds { get; set; } = new();
}
