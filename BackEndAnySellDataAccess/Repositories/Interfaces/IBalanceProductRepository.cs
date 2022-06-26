using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellAccessDataAccess.Repositories.Interfaces
{
    public interface IBalanceProductRepository
    {      
        Task<IEnumerable<BalanceProduct>> GetByStoreIdDownloadPriceListAsync(Guid storeId);
        Task<IEnumerable<BalanceProduct>> GetByStoreIdAsync(Guid storeId);
    }
}
