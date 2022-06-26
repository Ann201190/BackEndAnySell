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
    public class BalanceProductService : IBalanceProductService
    {
        private readonly IBalanceProductRepository _balanceProductRepository;

        public BalanceProductService(IBalanceProductRepository balanceProductRepository)
        {
            _balanceProductRepository = balanceProductRepository;       
        }

        public async Task<IEnumerable<BalanceProduct>> GetByStoreIdDownloadPriceListAsync(Guid storeId)
        {
            return await _balanceProductRepository.GetByStoreIdDownloadPriceListAsync(storeId);
        }
    }
}


