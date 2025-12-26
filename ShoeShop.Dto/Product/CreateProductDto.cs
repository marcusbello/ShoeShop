using System;

namespace ShoeShop.Dto.Product;

public class CreateProductDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal SupplierPrice { get; set; }
    public Guid SupplierId { get; set; }
    public int Stock { get; set; }
    public string? Sku { get; set; }
    public string? ImageUrl { get; set; }
    public List<Guid> CategoryIds { get; set; } = new();
}
