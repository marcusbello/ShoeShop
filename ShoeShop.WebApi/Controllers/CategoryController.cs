using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoeShop.Dto.Category;
using ShoeShop.Entities;
using ShoeShop.WebApi.Repositories;

namespace ShoeShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/categories
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _categoryRepository.RetrieveAllAsync());
        }

        // GET: api/categories/[id]
        [HttpGet("{id}", Name = nameof(GetCategory))] // named route
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCategory(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("Invalid category ID format.");
            var category = await _categoryRepository.RetrieveAsync(guid);
            if (category == null) return NotFound();
            return Ok(category);
        }

        // POST: api/categories
        // BODY: Category (JSON, XML)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto c)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCategory = await _categoryRepository.CreateAsync(c);
            if (createdCategory == null)
                return BadRequest();
            // 201 Created
            return CreatedAtRoute(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }

        // PUT: api/categories/[id]
        // BODY: Category (JSON, XML)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateCategoryDto c)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("Invalid category ID format.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedCategory = await _categoryRepository.UpdateAsync(guid, c);
            if (updatedCategory == null)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/categories/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("Invalid category ID format.");
            var result = await _categoryRepository.DeleteAsync(guid);
            if (result == false)
                return NotFound();

            return NoContent();
        }

    }
}
