using System;
using System.Collections.Generic;

namespace ShoeShop.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    // Price from the supplier for this product
    public decimal SupplierPrice { get; set; }
    public int Stock { get; set; }
    // IsNew flag
    public bool IsNew { get; set; } = false;
    // IsFeatured flag
    public bool IsFeatured { get; set; } = false;
    // Sizes available (e.g., "7,8,9,10")
    public string Sizes { get; set; } = default!;
    // Image location (URL or path)
    public string ImageUrl { get; set; } = default!;
    // Other images (URLs or paths)
    public string ImageUrls { get; set; } = default!;
    // SKU for inventory systems
    public string Sku { get; set; } = default!;
    // Many-to-many → A product can belong to multiple categories
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    // Supplier relationship (one supplier -> many products)
    public Guid SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}
