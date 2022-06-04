using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAnySellAccessDataAccess.Repositories.Interfaces
{
    public interface IComingRepository
    {
        Task<Coming> GetByIdAsync(Guid id);
      //  Task<Coming> GetByNameAsync(string name);
        Task<bool> AddAsync(Coming discount);
        Task<IEnumerable<Coming>> GetByStoreIdAsync(Guid storeId);
     /*   Task<bool> UpdateAsync(Discount discount);
        Task<bool> DeleteAsync(Guid id);
        Task<Discount> IsUniqueName(Guid id, string name);*/
    }
}
