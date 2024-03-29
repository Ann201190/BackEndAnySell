﻿using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellAccessDataAccess.Repositories.Interfaces
{
    public interface IDiscountRepository
    {
        Task<Discount> GetByIdAsync(Guid id);
        Task<Discount> GetByNameAsync(string name);
        Task<bool> AddAsync(Discount discount);
        Task<IEnumerable<Discount>> GetByStoreIdAsync(Guid storeId);
        Task<bool> UpdateAsync(Discount discount);
        Task<bool> DeleteAsync(Guid id);
        Task<Discount> IsUniqueName(Guid id, string name);
        Task<bool> DeleteProducDiscountAsync(List<Guid> productIds, Guid id);
        Task<bool> AddProducDiscountAsync(List<Guid> productIds, Guid id);
    }
}
