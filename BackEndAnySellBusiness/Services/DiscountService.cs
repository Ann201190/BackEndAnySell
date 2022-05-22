using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> AddAsync(AddDiscountViewModel discountModel)
        {
            var isUnique = await _discountRepository.GetByNameAsync(discountModel.Name);

            if (discountModel != null && isUnique == null)
            {
                var discount = new Discount()
                {
                    Name = discountModel.Name,
                    Value = discountModel.Value,
                    StoreId = discountModel.StoreId,
                    DiscountType = discountModel.DiscountType,
                };

                return await _discountRepository.AddAsync(discount);                   // передаем уже готовый объект для сохранения в базу данных               
            }
            return false;
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

        public async Task<bool> UpdateAsync(UpdateDiscountViewModel discountModel)
        {
            var isUnique = await _discountRepository.IsUniqueName(discountModel.Id, discountModel.Name);

            if (isUnique==null)
            {
                var discount = await _discountRepository.GetByIdAsync(discountModel.Id);

                discount.Name = discountModel.Name;
                discount.StoreId = discountModel.StoreId;
                discount.Value = discountModel.Value;
                discount.DiscountType = discountModel.DiscountType;

                return await _discountRepository.UpdateAsync(discount);
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var discount = await _discountRepository.GetByIdAsync(id);

            if (discount.Products.Count == 0)
            {
                return await _discountRepository.DeleteAsync(id);
            }
            return false;
        }
    }
}


