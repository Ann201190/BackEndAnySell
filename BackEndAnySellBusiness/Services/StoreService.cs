using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services
{
     public class StoreService: IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<bool> AddAsync(Store store)
        {
            return await _storeRepository.AddAsync(store);
        }

        public async Task<IEnumerable<Store>> GetAsync(string userName)
        {
            return await _storeRepository.GetAsync(userName);
        }

        public async Task<Store> GetByIdAsync(Guid id)
        {
            return await _storeRepository.GetByIdAsync(id);
        }
    }
}
