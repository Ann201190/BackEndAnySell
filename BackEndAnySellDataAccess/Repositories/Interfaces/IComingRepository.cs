using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellAccessDataAccess.Repositories.Interfaces
{
    public interface IComingRepository
    {
        Task<Coming> GetByIdAsync(Guid id);
        Task<bool> AddAsync(Coming discount);
        Task<IEnumerable<Coming>> GetByStoreIdAsync(Guid storeId);
        Task<bool> DeleteAsync(Guid id);  
        /* Task<bool> UpdateAsync(Discount discount);
        Task<Coming> GetByNameAsync(string name);
        Task<Discount> IsUniqueName(Guid id, string name); */
    }
}
