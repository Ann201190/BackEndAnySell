using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetByStoreIdAsync(Guid storeId);
    }
}
