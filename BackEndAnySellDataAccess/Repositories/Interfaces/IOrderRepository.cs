using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAnySellAccessDataAccess.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetByStoreIdAsync(Guid storeId);
        Task<Order> GetByIdAsync(Guid id);
    }
}
