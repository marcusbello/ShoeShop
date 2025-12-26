using System;

namespace ShoeShop.Entities;

public class Supplier
{
    public Guid Id { get; set; }

    // Display name for the supplier/company
    public string Name { get; set; } = default!;

    // Optional contact person at the supplier
    public string? ContactName { get; set; }

    // Contact details
    public string? Email { get; set; }
    public string? Phone { get; set; }

    // Physical address or notes
    public string? Address { get; set; }

    // Timestamps
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Optional: Products supplied by this supplier (many-to-many or one-to-many can be added later)
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
