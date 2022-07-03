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
    public  class OrderService: IOrderService
    {
        private readonly IOrderRepository _odrerRepository;
        private readonly IProductService _productService;
        private readonly IBalanceProductRepository _balanceProductRepository;
        public OrderService(IOrderRepository odrerRepository, IProductService productService, IBalanceProductRepository balanceProductRepository)
        {
            _odrerRepository = odrerRepository;
            _productService = productService;
            _balanceProductRepository = balanceProductRepository;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _odrerRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetByStoreIdAsync(Guid storeId)
        {
            return await _odrerRepository.GetByStoreIdAsync(storeId);
        }

        public async Task<IEnumerable<GetOrderProductViewModel>> GetProductByStoreIdAsync(Guid storeId)
        {
            var balanceProducts = await _balanceProductRepository.GetByStoreIdAsync(storeId);

            var cashBoxProduct = new List<GetOrderProductViewModel>();

            foreach (var balanceProduct in balanceProducts)
            {
                cashBoxProduct.Add(new GetOrderProductViewModel()
                {
                    Id = balanceProduct.Product.Id,
                    Barcode = balanceProduct.Product.Barcode,
                    Name = balanceProduct.Product.Name,
                    ProductUnit = balanceProduct.Product.ProductUnit,
                    PriceWithDiscount = _productService.GetPriceWithDiscount(balanceProduct.Product)
                });
            }

            return cashBoxProduct
                .GroupBy(b => b.Barcode)
                .Select(b => b.First());
        }
    }
}
