using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<bool> AddAsync(Discount discount)
        {
            return await _discountRepository.AddAsync(discount);
        }


        public async Task<IEnumerable<Discount>> GetByStoreIdAsync(Guid storeId)
        {
            return await _discountRepository.GetByStoreIdAsync(storeId);
        }


        /* public async Task<IEnumerable<Discount>> GetAsync()
         {
             return await _discountRepository.GetAsync();
         }*/

        public async Task<Discount> GetByIdAsync(Guid id)
        {
            return await _discountRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Discount discount)
        {
            return await _discountRepository.UpdateAsync(discount);
        }
    }
}


