using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAnySellAccessDataAccess.Repositories.Interfaces
{
    public interface IDiscountRepository
    {
        /*Task<IEnumerable<Discount>> GetAsync();*/
        Task<Discount> GetByIdAsync(Guid id);
        Task<Discount> GetByNameAsync(string name);
        Task<bool> AddAsync(Discount discount);
        Task<IEnumerable<Discount>> GetByStoreIdAsync(Guid storeId);
        Task<bool> UpdateAsync(Discount discount);
        Task<bool> DeleteAsync(Guid id);
        Task<Discount> IsUniqueName(Guid id, string name);
    }
}
