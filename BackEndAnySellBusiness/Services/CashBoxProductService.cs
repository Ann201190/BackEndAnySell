using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndSellViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services
{
    public class CashBoxProductService : ICashBoxProductService
    {
        private readonly IBalanceProductRepository _balanceProductRepository;
        private readonly IProductService _productService;
        public CashBoxProductService(IBalanceProductRepository balanceProductRepository, IProductService productService)
        {
            _balanceProductRepository = balanceProductRepository;
            _productService = productService;
        }

        public async Task<IEnumerable<GetCashBoxProductViewModel>> GetByStoreIdAsync(Guid storeId)
        {
            var balanceProducts = await _balanceProductRepository.GetByStoreIdAsync(storeId);

            var cashBoxProduct = new List<GetCashBoxProductViewModel>();

            foreach (var balanceProduct in balanceProducts)
            {
                cashBoxProduct.Add(new GetCashBoxProductViewModel()
                {
                    Id = balanceProduct.Product.Id,
                    Barcode = balanceProduct.Product.Barcode,
                    Name = balanceProduct.Product.Name,                 
                    ProductUnit = balanceProduct.Product.ProductUnit,
                    PriceWithDiscount = _productService.GetPriceWithDiscount(balanceProduct.Product)
                });
            }

            return cashBoxProduct;
        }
    }
}


