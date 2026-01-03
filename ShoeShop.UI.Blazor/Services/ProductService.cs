using System;
using System.Net.Http.Json;
using ShoeShop.Dto.Product;

namespace ShoeShop.UI.Blazor.Services;

public class ProductService
{
    private readonly HttpClient _http;

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ProductDto>> GetProductsAsync()
        => await _http.GetFromJsonAsync<List<ProductDto>>("api/product") ?? [];

    public async Task<ProductDto?> GetProductAsync(Guid id)
        => await _http.GetFromJsonAsync<ProductDto>($"api/product/{id}");

    public async Task CreateProductAsync(CreateProductDto dto)
        => await _http.PostAsJsonAsync("api/product", dto);
}
