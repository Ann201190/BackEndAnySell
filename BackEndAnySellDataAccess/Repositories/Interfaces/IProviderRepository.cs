using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellAccessDataAccess.Repositories.Interfaces
{
    public interface IProviderRepository
    {
        Task<Provider> GetByIdAsync(Guid id);
        Task<bool> AddAsync(Provider providerModel);
        Task<IEnumerable<Provider>> GetAsync(string userName);
        Task<bool> UpdateAsync(Provider providerModel);
        Task<bool> DeleteAsync(Guid id);
    }
}
