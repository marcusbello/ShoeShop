using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoeShop.Dto.Product;
using ShoeShop.Entities;
using ShoeShop.WebApi.Repositories;

namespace ShoeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/products
        // GET: api/products?category=slippers
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductWithCategoriesDto>))]
        public async Task<IEnumerable<ProductWithCategoriesDto>> GetProducts(string? category = null)
        {
            return await _productRepository.RetrieveAllAsync(category);
        }

        // GET: api/products/[id]
        [HttpGet("{id}", Name = nameof(GetProduct))]
        [ProducesResponseType(200, Type = typeof(ProductWithCategoriesDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productRepository.RetrieveAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        // BODY: Product (JSON, XML)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ProductWithCategoriesDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _productRepository.CreateAsync(dto);
            return CreatedAtRoute(nameof(GetProduct), new { id = created?.Id }, created);
        }

        // PUT: api/products/[id]
        // BODY: Customer (JSON, XML)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto p)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _productRepository.UpdateAsync(id, p);
            if (updated == null)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/products/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productRepository.DeleteAsync(id);
            if (result == false)
                return NotFound();

            return NoContent();
        }

    }
}
