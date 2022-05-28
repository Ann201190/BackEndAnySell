using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services.Interfaces
{
    public interface IStoreService
    {
        Task<Store> GetByIdAsync(Guid id);
        Task<IEnumerable<Store>> GetAsync(string userName);
        Task<Tuple<Guid, Guid>> AddWithEmployeeAsync(AddStoreWithEmployeeViewModel storeModel, string userName);
        Task<bool> AddImageAsync(IFormFile file, Guid id);
        Task<Guid> AddWithoutEmployeeAsync(AddStoreWithoutEmployeeViewModel storeModel, string userName);
        Task<Guid> UpdateStoreAsync(UpdateStoreWithoutImgeViewModel storeModel);
        Task<bool> DeleteAsync(Guid id);
    }
}
