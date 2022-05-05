using BackEndAnySellDataAccess.Entities;
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
        Task<bool> AddAsync(Store store);
    }
}
