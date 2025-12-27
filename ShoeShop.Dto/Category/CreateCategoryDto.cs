using System;

namespace ShoeShop.Dto.Category;

public class CreateCategoryDto
{
    public required string Name { get; set; }
    public string? ImageUrl { get; set; }

}
