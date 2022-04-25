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
       Task <IEnumerable<Product>> GetAsync();
        Task<Product> GetAsync(Guid id);
        Task<IEnumerable<Product>> GetByStoreIdAsync(Guid storeId);
        Task<bool> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Guid id);
    }
}
