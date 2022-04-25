using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services.Interfaces
{
    public interface IDiscountService
    {
    /*    Task<IEnumerable<Discount>> GetAsync();*/
        Task<Discount> GetByIdAsync(Guid id);
        Task <bool> AddAsync(Discount discount);
        Task<IEnumerable<Discount>> GetByStoreIdAsync(Guid storeId);
        Task<bool> UpdateAsync(Discount discount);
    }
}
