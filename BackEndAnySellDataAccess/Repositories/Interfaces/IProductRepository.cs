using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAnySellAccessDataAccess.Repositories.Interfaces
{
  public  interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetByStoreIdAsync(Guid storeId);
        Task<bool> AddProductAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AddImageAsync(byte[] fileArrayBytes, Guid id);
        Task<bool> DeleteImageAsync(Guid id);
        Task<IEnumerable<Product>> GetByDiscountIdAsync(Guid discountId);
    }
}
