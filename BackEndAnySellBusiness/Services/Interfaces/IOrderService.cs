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
        Task<Order> GetCheckAsync(Guid storeId, string orderNumber);
        Task<IEnumerable<Order>> GetByStoreIdAsync(Guid storeId);
        Task<GraphBarDataViewModel> GetChecCashierAsync(Guid storeId);
        Task<string> AddAsync(AddOrderViewModel orderModel, string userName);
        Task<GraphLineDataViewModel> GetProfitAsync(Guid storeId);
        Task<IEnumerable<GetOrderProductViewModel>> GetProductByStoreIdAsync(Guid storeId);
        
    }
}
