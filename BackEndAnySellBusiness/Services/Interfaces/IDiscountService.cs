using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<Discount> GetByIdAsync(Guid id);
        Task <bool> AddAsync(AddDiscountViewModel discountModel);
        Task<IEnumerable<Discount>> GetByStoreIdAsync(Guid storeId);
        Task<bool> UpdateAsync(UpdateDiscountViewModel discountModel);
        Task<bool> DeleteAsync(Guid id);
    }
}
