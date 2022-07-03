using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services.Interfaces
{
    public interface IComingService
    {
        Task<Coming> GetByIdAsync(Guid id);
        Task <bool> AddAsync(AddComingViewModel discountModel);
        Task<IEnumerable<Coming>> GetByStoreIdAsync(Guid storeId);
      //  Task<bool> UpdateAsync(UpdateDiscountViewModel discountModel);
        Task<bool> DeleteAsync(Guid id);
    }
}
