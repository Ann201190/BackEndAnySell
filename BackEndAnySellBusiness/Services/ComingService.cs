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
        private readonly IBalanceProductRepository _balanceProductRepository;
        public ComingService(IComingRepository comingRepository, IBalanceProductRepository balanceProductRepository)
        {
            _comingRepository = comingRepository;
            _balanceProductRepository = balanceProductRepository;
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
        }*/

        public async Task<bool> DeleteAsync(Guid id)
        {
            var coming = await _comingRepository.GetByIdAsync(id);
            var balanceProduct = await _balanceProductRepository.GetByStoreIdAsync(coming.StoreId);
            var delete = false;

            foreach (var comingBalanceProducts in coming.BalanceProducts)
            {
             //  balanceProduct.Any(bp => bp.Id == comingBalanceProducts.Id && bp.BalanceCount == comingBalanceProducts.BalanceCount);
                if (balanceProduct.Any(bp => bp.Id == comingBalanceProducts.Id && bp.BalanceCount == comingBalanceProducts.BalanceCount))
                {
                    delete = true;
                }
            }

            if (delete)
            {
                return await _comingRepository.DeleteAsync(id);
            }
            return false;
        }
    }
}


