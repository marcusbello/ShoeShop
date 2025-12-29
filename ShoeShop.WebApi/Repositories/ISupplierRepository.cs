using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoeShop.Entities;

namespace ShoeShop.WebApi.Repositories
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> RetrieveAllAsync();
        Task<Supplier?> RetrieveAsync(Guid id);
        Task<Supplier?> CreateAsync(Supplier supplier);
        Task<Supplier?> UpdateAsync(Guid id, Supplier supplier);
        Task<bool?> DeleteAsync(Guid id);
    }
}