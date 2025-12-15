using System;

namespace ShoeShop.Dto.Category;

public class UpdateCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
}
