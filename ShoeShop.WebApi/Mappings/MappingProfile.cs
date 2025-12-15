using AutoMapper;
using ShoeShop.Dto.Category;
using ShoeShop.Dto.Product;
using ShoeShop.Entities;

namespace ShoeShop.WebApi.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Category
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryWithProductsDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();

        // Product
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductWithCategoriesDto>();

        CreateMap<CreateProductDto, Product>().ForMember(dest => dest.Categories, opt => opt.Ignore());

        CreateMap<UpdateProductDto, Product>().ForMember(dest => dest.Categories, opt => opt.Ignore());

    }
}
