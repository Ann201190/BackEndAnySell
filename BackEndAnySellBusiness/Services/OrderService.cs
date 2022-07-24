using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Enums;
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
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly IComingRepository _comingRepository;
        private readonly IBalanceProductRepository _balanceProductRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public OrderService(IOrderRepository orderRepository,
            IProductService productService,
            IComingRepository comingRepository,
            IEmployeeRepository employeeRepository,
            IBalanceProductRepository balanceProductRepository)
        {
            _orderRepository = orderRepository;
            _comingRepository = comingRepository;
            _productService = productService;
            _employeeRepository = employeeRepository;
            _balanceProductRepository = balanceProductRepository;
        }

        public async Task<string> AddAsync(AddOrderViewModel orderModel, string userName)
        {       
            if (orderModel != null)
            {
                foreach (var product in orderModel.Product)
                {
                    var countOnBalance = await _balanceProductRepository.CountAsync(product.ProductId);
                    if (countOnBalance<product.Count)
                    {
                        return "error";
                    }
                }

                var user = await _employeeRepository.GetAsync(userName);
                var orderNumber = GenerationRandomString(15);
                var reservationProducts = new List<ReservationProduct>();

                var products = await _productService.GetByStoreIdAsync(orderModel.StoreId);

                foreach (var orderProduct in orderModel.Product)
                {
                    var product = products.FirstOrDefault(p => p.Id == orderProduct.ProductId);//товар
                    var comings = await _comingRepository.GetByProductIdAsync(orderProduct.ProductId); //все приходы
                    var isSmallCount = false; //переменная сообщающая что не хватает товара

                    do
                    {
                        var count = 0.0;
                        var coming = comings.FirstOrDefault(c => c.BalanceProducts.Any(bp => bp.ProductId == orderProduct.ProductId && bp.BalanceCount > 0));
                        if (coming == null)
                        {
                            return "error";
                        }
                        var balanceProduct = coming.BalanceProducts.FirstOrDefault(bp => bp.ProductId == orderProduct.ProductId && bp.BalanceCount > 0);

                        if (balanceProduct.BalanceCount < orderProduct.Count)
                        {
                            isSmallCount = true;
                            count = balanceProduct.BalanceCount;
                            balanceProduct.BalanceCount = 0;

                            await _balanceProductRepository.UpdateAsync(balanceProduct.Id, balanceProduct.BalanceCount); // если false вернуть error
                        }
                        else
                        {
                            isSmallCount = false;
                            count = orderProduct.Count;
                            balanceProduct.BalanceCount -= count;

                            await _balanceProductRepository.UpdateAsync(balanceProduct.Id, balanceProduct.BalanceCount); // если false вернуть error
                        }

                        reservationProducts.Add(new ReservationProduct()
                        {
                            OrderId = orderModel.Id,
                            Count = count,
                            Price = product.Price,
                            ProductId = orderProduct.ProductId,
                            DiscountValue = _productService.GetDiscount(product),
                            PriceComing = balanceProduct.ComingPrice,
                            BalanceProductId = balanceProduct.Id
                        });
                    } while (isSmallCount);
                }

                var order = new Order()
                {
                    Id = orderModel.Id,
                    OrderDate = DateTime.Now,
                    OrderNumber = orderNumber,
                    StoreId = orderModel.StoreId,
                    OrderStatus = OrderStatus.Paid,
                    ReservationProducts = reservationProducts,
                    EmployeeId = user.Id
                };

                var isAdd = await _orderRepository.AddAsync(order);

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
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetByStoreIdAsync(Guid storeId)
        {
            return await _orderRepository.GetByStoreIdAsync(storeId);
        }

        public async Task<Order> GetCheckAsync(string orderNumber)
        {
            var order = await _orderRepository.GetCheckAsync(orderNumber);

            return FormattingOrder(order);
        }

        private Order FormattingOrder(Order order)
        {
            if (order == null)
            {
                return order;
            }
            var resProdGrouped = order.ReservationProducts
                .GroupBy(rp => rp.ProductId)
                .ToList();

            var reservationProducts = new List<ReservationProduct>();

            foreach (var products in resProdGrouped)
            {
                var count = 0.0;
                foreach (var product in products)
                {
                    count += product.Count;
                }
                var rp = products.First();
                rp.Count = count;
                reservationProducts.Add(rp);
            }

            order.ReservationProducts = reservationProducts;

            return order;
        }

        public async Task<Order> GetCheckAsync(Guid storeId, string orderNumber)
        {
            var order = await _orderRepository.GetStoreCheckAsync(storeId, orderNumber);

            return FormattingOrder(order);
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
                    PriceWithDiscount = _productService.GetPriceWithDiscount(balanceProduct.Product),
                });
            }

            return cashBoxProduct
                .GroupBy(b => b.Barcode)
                .Select(b => b.First());
        }
        public async Task<GraphBarDataViewModel> GetChecCashierAsync(Guid storeId)
        {
            var orders = await _orderRepository.GetByStoreIdAsync(storeId);

            var ordersEmployees = orders
                .Where(o => o.OrderDate.Year == DateTime.Now.Year)
                .GroupBy(o => o.EmployeeId);

            var graph = new GraphBarDataViewModel();
            for (int i = 1; i <= 12; i++)
            {
                graph.Labels.Add($"{i}.{DateTime.Now.Year}");
            }

            foreach (var ordersEmployee in ordersEmployees)
            {
                var months = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                var nameEmployee = $"{ordersEmployee.First().Employee.SurName} {ordersEmployee.First().Employee.Name}";
                foreach (var order in ordersEmployee)
                {
                    months[order.OrderDate.Month - 1]++;
                }
                graph.Datasets.Add(new DataSetBar { Label = nameEmployee, Data = months });
            }

            return graph;
        }

        public async Task<GraphLineDataViewModel> GetProfitAsync(Guid storeId)
        {
            var orders = await _orderRepository.GetByStoreIdAsync(storeId);

            var ordersProfit = orders
                .Where(o => o.OrderDate.Year == DateTime.Now.Year)
                .GroupBy(o => o.OrderDate.Month);

            var graph = new GraphLineDataViewModel();
            for (int i = 1; i <= 12; i++)
            {
                graph.Labels.Add($"{i}.{DateTime.Now.Year}");
            }

            var name = "Прибыль"; 
            var profitMonths = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (var ordersEmployee in ordersProfit)
            {
              
            
                foreach (var order in ordersEmployee)
                {
                    var sum = 0.0;
                    foreach (var product in order.ReservationProducts)
                    {
                        sum = sum + ((product.Count * (double) (product.Price-product.DiscountValue)) - (product.Count * (double)product.PriceComing));
                    }

                     profitMonths[order.OrderDate.Month - 1]= sum;
                }
               
            }
            
            graph.Datasets.Add(new DataSetLine { Label = name, Data = profitMonths, Tension= 0.5 });

            return graph;
        }
    
        public async Task<string> ProductsReturn(string orderNumber, List<Guid> reservationProductIds)
        {
            var order = await _orderRepository.GetCheckAsync(orderNumber);
            var newOrderNumber = GenerationRandomString(15);
            var newOrder = new Order()
            {
                OrderDate = order.OrderDate,
                OrderNumber = newOrderNumber,
                StoreId = order.StoreId,
                OrderStatus = OrderStatus.Paid,
                EmployeeId = order.EmployeeId
            };

            var actualProducts = order.ReservationProducts
                .Where(rp => !reservationProductIds.Contains(rp.Id))
                .Select(rp => {
                    return new ReservationProduct
                    {
                        ProductId = rp.ProductId,
                        Count = rp.Count,
                        OrderId = newOrder.Id,
                        Price = rp.Price,
                        PriceComing = rp.PriceComing,
                        DiscountValue = rp.DiscountValue
                    };
                })
                .ToList();

            newOrder.ReservationProducts = actualProducts;

            var returnedProducts = order.ReservationProducts
                .Where(rp => reservationProductIds.Contains(rp.Id));

            foreach (var product in returnedProducts)
            {
                await _balanceProductRepository.AddCountAsync(product.BalanceProductId, product.Count);
            }

            var result = await _orderRepository.CancelCheck(order.Id);
            
            if (result)
            {
                var addedResult = await _orderRepository.AddAsync(newOrder);
                if (addedResult)
                {
                    return "http://localhost:5000/orderNumber/" + newOrderNumber;
                }
            }

            return "error";
        }
    }
}

