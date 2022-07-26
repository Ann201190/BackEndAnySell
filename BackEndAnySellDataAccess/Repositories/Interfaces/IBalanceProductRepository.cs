using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellAccessDataAccess.Repositories.Interfaces
{
    public interface IBalanceProductRepository
    {      
        Task<IEnumerable<BalanceProduct>> GetByStoreIdAsync(Guid storeId);
        Task<bool> UpdateAsync(Guid balanceProductId, double count);
        Task<double> CountAsync(Guid productId);
        Task<bool> AddCountAsync(Guid balanceProductId, double addingCount);
    }
}
