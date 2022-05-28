using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services.Interfaces
{
    public interface IProviderService
    {
        Task<Provider> GetByIdAsync(Guid id);
        Task<IEnumerable<Provider>> GetAsync(string userName);
        Task <bool> AddAsync(AddProviderViewModel providerModel, string userName);
        Task<bool> UpdateAsync(UpdateProviderViewModel providerModel);
        Task<bool> DeleteAsync(Guid id);
    }
}
