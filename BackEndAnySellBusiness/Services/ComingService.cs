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
    public class ComingService : IComingService
    {
        private readonly IComingRepository _comingRepository;
        public ComingService(IComingRepository comingRepository)
        {
            _comingRepository = comingRepository;
        }

        public async Task<bool> AddAsync(AddComingViewModel comingModel)
        {

            if (comingModel != null)
            {
                List<BalanceProduct> balanceProduct = new List<BalanceProduct>();
                foreach (var bp in comingModel.BalanceProducts)
                {
                    balanceProduct.Add(new BalanceProduct()
                    {
                         Count = bp.Count,
                         BalanceCount= bp.Count,
                         ProductId = bp.ProductId,
                         ComingPrice = bp.ComingPrice,
                         ComingId= comingModel.Id
                    });
                }

                var coming = new Coming()
                {
                    Number = comingModel.Number,
                    ProviderId = comingModel.ProviderId,
                    StoreId = comingModel.StoreId,
                    Date = DateTime.Now,
                    BalanceProducts= balanceProduct
                };

                return await _comingRepository.AddAsync(coming);                   // передаем уже готовый объект для сохранения в базу данных               
            }
            return false;
        }

        public async Task<IEnumerable<Coming>> GetByStoreIdAsync(Guid storeId)
        {
            return await _comingRepository.GetByStoreIdAsync(storeId);
        }

        public async Task<Coming> GetByIdAsync(Guid id)
        {
            return await _comingRepository.GetByIdAsync(id);
        }

      /*  public async Task<bool> UpdateAsync(UpdateDiscountViewModel discountModel)
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
        }*/
    }
}


