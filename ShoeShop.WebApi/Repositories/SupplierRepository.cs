using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoeShop.DataContext;
using ShoeShop.Entities;

namespace ShoeShop.WebApi.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ShoeshopDbContext _context;
        public SupplierRepository(ShoeshopDbContext context)
        {
            _context = context;
        }
        public async Task<Supplier?> CreateAsync(Supplier supplier)
        {
            if (supplier == null) return null;

            supplier.Id = supplier.Id == Guid.Empty ? Guid.NewGuid() : supplier.Id;
            supplier.CreatedAt = DateTime.UtcNow;
            supplier.UpdatedAt = DateTime.UtcNow;

            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<bool?> DeleteAsync(Guid id)
        {
            var existing = await _context.Suppliers
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (existing == null)
                return false;

            // Prevent delete if supplier still has products (FK is Restrict)
            if (existing.Products != null && existing.Products.Any())
                return false;

            _context.Suppliers.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Supplier>> RetrieveAllAsync()
        {
            return await _context.Suppliers
                .ToListAsync();
        }

        public async Task<Supplier?> RetrieveAsync(Guid id)
        {
            return await _context.Suppliers
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Supplier?> UpdateAsync(Guid id, Supplier supplier)
        {
            var existing = await _context.Suppliers.FindAsync(id);
            if (existing == null)
                return null;

            existing.Name = supplier.Name;
            existing.ContactName = supplier.ContactName;
            existing.Email = supplier.Email;
            existing.Phone = supplier.Phone;
            existing.Address = supplier.Address;
            existing.UpdatedAt = DateTime.UtcNow;

            _context.Suppliers.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}