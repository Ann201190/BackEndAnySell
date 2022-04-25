using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAsync();
        Task<IEnumerable<Product>> GetByStoreIdAsync(Guid storeId);
        Task<bool> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Guid id);
    }
}
