using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoeShop.Entities;
using ShoeShop.WebApi.Repositories;

namespace ShoeShop.WebApi.Controllers
{
    [Route("[controller]")]
    public class SupplierController : ControllerBase
    {

        private readonly ISupplierRepository _supplierRepository;
        public SupplierController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        // GET: /supplier
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Supplier>))]
        public async Task<IActionResult> GetSuppliers()
        {
            var suppliers = await _supplierRepository.RetrieveAllAsync();
            return Ok(suppliers);
        }

        // GET: /supplier/{id}
        [HttpGet("{id}", Name = nameof(GetSupplier))]
        [ProducesResponseType(200, Type = typeof(Supplier))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSupplier(Guid id)
        {
            var supplier = await _supplierRepository.RetrieveAsync(id);
            if (supplier == null)
                return NotFound();
            return Ok(supplier);
        }

        // POST: /supplier
        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdSupplier = await _supplierRepository.CreateAsync(supplier);
            if (createdSupplier == null)
                return BadRequest();

            return CreatedAtRoute(nameof(GetSupplier), new { id = createdSupplier.Id }, createdSupplier);
        }

        // PUT: /supplier/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(Guid id, [FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedSupplier = await _supplierRepository.UpdateAsync(id, supplier);
            if (updatedSupplier == null)
                return NotFound();

            return Ok(updatedSupplier);
        }

        // DELETE: /supplier/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _supplierRepository.DeleteAsync(id);
            if (result == false)
                return NotFound();

            return NoContent();
        }

    }
}