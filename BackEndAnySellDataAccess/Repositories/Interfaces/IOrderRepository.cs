using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellAccessDataAccess.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetByStoreIdAsync(Guid storeId);
        Task<Order> GetByIdAsync(Guid id);
        Task<Order> GetCheckAsync(string orderNumber);
        Task<Order> GetStoreCheckAsync(Guid storeId, string orderNumber);
        Task<bool> AddAsync(Order order);
        Task<bool> CancelCheck(Guid orderId);
        Task<IEnumerable<Order>> GetTopThreeProductAsync(Guid storeId);        
    }
}
