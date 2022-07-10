using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Enums;
using BackEndAnySellDataAccess.Repositories.Interfaces;
using BackEndSellViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _odrerRepository;
        private readonly IProductService _productService;
        private readonly IReservationProductRepository _reservationProductRepository;
        private readonly IBalanceProductRepository _balanceProductRepository;
        public OrderService(IOrderRepository odrerRepository,
            IProductService productService,
            IReservationProductRepository reservationProductRepository,
            IBalanceProductRepository balanceProductRepository)
        {
            _odrerRepository = odrerRepository;
            _productService = productService;
            _reservationProductRepository = reservationProductRepository;
            _balanceProductRepository = balanceProductRepository;
        }

        public async Task<string> AddAsync(AddOrderViewModel orderModel)
        {
            if (orderModel != null)
            {
                var orderNumber = GenerationRandomString(15);
                var reservationProducts = new List<ReservationProduct>();

                foreach (var orderProduct in orderModel.Product)
                {
                    var product = await _productService.GetByIdAsync(orderProduct.ProductId);

                    reservationProducts.Add(new ReservationProduct()
                    {
                        OrderId = orderModel.Id,
                        Count = orderProduct.Count,
                        Price = product.Price,
                        Product = product,
                        ProductId = orderProduct.ProductId,
                    });
                }
               
                Order order = new Order()
                {
                    Id = orderModel.Id,
                    OrderDate = DateTime.Now,
                    OrderNumber = orderNumber,
                    StoreId = orderModel.StoreId,
                    OrderStatus = OrderStatus.Paid,
                    ReservationProducts = reservationProducts
                };

                var isAdd = await _odrerRepository.AddAsync(order);

                if (isAdd)
                {
                    return "http://localhost:5000/orderNumber/" + orderNumber;
                }
            }
            return "error";
        }

        private string GenerationRandomString(int lengthString)
        {
            int length = lengthString;
            string alphabet = "1234567890";
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(length - 1);
            int Position = 0;

            for (int i = 0; i < length; i++)
            {
                Position = rnd.Next(0, alphabet.Length - 1);
                sb.Append(alphabet[Position]);
            }
            return sb.ToString();
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _odrerRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetByStoreIdAsync(Guid storeId)
        {
            return await _odrerRepository.GetByStoreIdAsync(storeId);
        }

        public async Task<Order> GetCheckAsync(string orderNumber)
        {
            return await _odrerRepository.GetCheckAsync(orderNumber);
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
