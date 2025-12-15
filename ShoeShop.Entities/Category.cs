using System;

namespace ShoeShop.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;

    public string ImageUrl { get; set; } = default!;

    public ICollection<Product> Products { get; set; } = new List<Product>();

}
