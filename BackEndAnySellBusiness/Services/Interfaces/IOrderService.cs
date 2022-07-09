using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetByIdAsync(Guid id);     
        Task<Order> GetCheckAsync(string orderNumber);
        Task<IEnumerable<Order>> GetByStoreIdAsync(Guid storeId);
        Task<string> AddAsync(AddOrderViewModel orderModel);
        Task<IEnumerable<GetOrderProductViewModel>> GetProductByStoreIdAsync(Guid storeId);
    }
}
