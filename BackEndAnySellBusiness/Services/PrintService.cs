using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Models;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Repositories.Interfaces;
using BackEndSellViewModels.ViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services
{
    public class PrintService : IPrintService
    {
        private const string LocalIP = "::1";
        private readonly string DEFAULT_PRINTER_NAME = "Microsoft Print to PDF";
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly ITicketRepository _ticketRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly PrintSettings printSettings;

        public PrintService(
            IHttpClientFactory clientFactory,
            IConfiguration config,
            ITicketRepository ticketRepository,
            IEmployeeRepository employeeRepository,
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IProductService productService)
        {
            _config = config;
            _clientFactory = clientFactory;
            _ticketRepository = ticketRepository;
            _employeeRepository = employeeRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _productService = productService;

            printSettings = new PrintSettings
            {
                AllPrintersHost = _config.GetConnectionString("AllPrintersHost"),
                PrintServiceHost = _config.GetConnectionString("PrintServiceHost")
            };
        }

        public async Task<bool> PrintAsync(string employeeEmail, string orderNumber, string remoteIpAddress)
        {
            var ticketHeader = await _ticketRepository.GetTicketAsync();
            var order = await _orderRepository.GetCheckAsync(orderNumber);
            var user = await _employeeRepository.GetAsync(employeeEmail);
            var printerName = GetPrinterNameAsync(user);

            if (ticketHeader == null || order == null || printerName == null)
            {
                return false;
            }

            var bodyJson = ticketHeader.BodyJson;

            var ticketItemsList = new List<TicketModel>();

            var ticketItem = GetTicketModel(bodyJson, order);
            ticketItemsList.Add(ticketItem);

            var printModel = new PrintModel
            {
                PrinterName = printerName,
                Tickets = ticketItemsList
            };

            var multiplicator = MultiplicateValue(user.PrinterDpi);

            printModel.Multiplicate(multiplicator);

            var isPrinted = await SendPrintRequestAsync(printModel, remoteIpAddress);
            return isPrinted;
        }
        
        public async Task<bool> PrintPriceHolderAsync(string employeeEmail, Guid productId, string remoteIpAddress)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            var priceHolder = await _ticketRepository.GetPriceHolderAsync();
            var user = await _employeeRepository.GetAsync(employeeEmail);
            var printerName = GetPrinterNameAsync(user);

            if (product == null || priceHolder == null || printerName == null)
            {
                return false;
            }

            var bodyJson = priceHolder.BodyJson;

            var ticketItemsList = new List<TicketModel>();

            var ticketItem = GetPriceHolderModel(bodyJson, product);
            ticketItemsList.Add(ticketItem);

            var printModel = new PrintModel
            {
                PrinterName = printerName,
                Tickets = ticketItemsList
            };

            var multiplicator = MultiplicateValue(user.PrinterDpi);

            printModel.Multiplicate(multiplicator);

            var isPrinted = await SendPrintRequestAsync(printModel, remoteIpAddress);
            return isPrinted;
        }

        public async Task<bool> PrintAllPriceHoldersAsync(string employeeEmail, Guid storeId, string remoteIpAddress)
        {
            var products = await _productRepository.GetByStoreIdWithIncludesAsync(storeId);
            var priceHolder = await _ticketRepository.GetPriceHolderAsync();
            var user = await _employeeRepository.GetAsync(employeeEmail);
            var printerName = GetPrinterNameAsync(user);

            if (products == null || priceHolder == null || printerName == null)
            {
                return false;
            }

            var bodyJson = priceHolder.BodyJson;

            var ticketItemsList = GetPriceHoldersModel(bodyJson, products.ToList());

            var printModel = new PrintModel
            {
                PrinterName = printerName,
                Tickets = ticketItemsList
            };

            var multiplicator = MultiplicateValue(user.PrinterDpi);

            printModel.Multiplicate(multiplicator);

            var isPrinted = await SendPrintRequestAsync(printModel, remoteIpAddress);
            return isPrinted;
        }

        public async Task<bool> SetPrinterSettings(string employeeEmail, PrinterSettings printerSettings)
        {
            var user = await _employeeRepository.GetAsync(employeeEmail);

            user.PrinterDpi = printerSettings.Dpi;
            user.PrinterName = printerSettings.PrinterName;

            var isUpdated = await _employeeRepository.UpdateWithSettingsAsync(user);

            return isUpdated;
        }

        private string GetPrinterNameAsync(Employee user)
        {
            if (user == null)
            {
                return null;
            }
            return (user.PrinterName == string.Empty || user.PrinterName == null) ? DEFAULT_PRINTER_NAME : user.PrinterName;
        }

        public async Task<List<string>> GetPrintersAsync(string remoteIpAddress)
        {
            var requestAddress = GetRequestAddress(remoteIpAddress, printSettings.AllPrintersHost);

            try
            {
                var httpClient = _clientFactory.CreateClient();
                var response = await httpClient.GetStringAsync(requestAddress);
                return JsonConvert.DeserializeObject<List<string>>(response);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        private TicketModel GetTicketModel(string bodyJsonHeader, Order order)
        {
            var ticketModel = GetTicketItemsHeader(bodyJsonHeader, order);
            ticketModel.ItemsInfo.AddRange(GetTicketItemsProducts(order));

            return ticketModel;
        }

        private TicketModel GetPriceHolderModel(string bodyJson, Product product)
        {
            var ticketModel = GetPriceHolderItems(bodyJson, product);

            return ticketModel;
        }

        private List<TicketModel> GetPriceHoldersModel(string bodyJson, List<Product> products)
        {
            var pageColumns = 3;
            var pageRows = 8;

            var pixelsRight = 550;
            var pixelsDown = 300;
            var ticketModels = new Queue<TicketModel>();

            foreach (var product in products)
            {
                if (product.Name.Length > 30)
                {
                    product.Name = new string(product.Name.Take(30).ToArray());
                }
                var ticketModel = GetPriceHolderItems(bodyJson, product);
                ticketModels.Enqueue(ticketModel);
            }
            var pagesTicketModels = new List<TicketModel>();

            while (ticketModels.Count > 0)
            {
                var pageModel = new TicketModel() { ItemsInfo = new List<TicketItemInfo>()};
                for (int row = 0; row < pageRows; row++)
                {
                    for (int col = 0; col < pageColumns; col++)
                    {
                        if (ticketModels.Count ==0)
                        {
                            break;
                        }
                        var tiketModel = ticketModels.Dequeue();
                        SetRightModel(tiketModel, pixelsRight * col);
                        SetDownModel(tiketModel, pixelsDown * row);
                        pageModel.ItemsInfo.AddRange(tiketModel.ItemsInfo);
                    }
                }
                pagesTicketModels.Add(pageModel); //add page
            }
           
            return pagesTicketModels;
        }

        private void SetRightModel(TicketModel model, int pixels)
        {
            foreach (var item in model.ItemsInfo)
            {
                item.PositionInfo.X += pixels;
            }
        }

        private void SetDownModel(TicketModel model, int pixels)
        {
            foreach (var item in model.ItemsInfo)
            {
                item.PositionInfo.Y += pixels;
            }
        }

        private TicketModel GetPriceHolderItems(string bodyJson, Product product)
        {
            var ticketPrintModel = GetPriceHolderModel(product);
            var tokens = GetTokens(ticketPrintModel);
            var ticketModel = GetTicketModelHeader(bodyJson, tokens);

            return ticketModel;
        }

        private PriceHolderPrintModel GetPriceHolderModel(Product product)
        {
            var priceWithDiscount = _productService.GetPriceWithDiscount(product);
            var ticketModel = new PriceHolderPrintModel
            {
                ProductName = product.Name,
                Barcode = product.Barcode,
                DefaultPrice = string.Format("{0:F2}", product.Price),
                PriceWithDiscount = string.Format("{0:F2}", priceWithDiscount),
                StoreName = product.Store.Name
            };

            return ticketModel;
        }

        private double MultiplicateValue(int dpi)
        {
            double defaultDpi = 200;
            return dpi / defaultDpi;
        }

        private IEnumerable<TicketItemInfo> GetTicketItemsProducts(Order order)
        {
            var items = new List<TicketItemInfo>(order.ReservationProducts.Count * 5);
            int startY = 350;
            int nextLinePixels = 37;
            int fontSize = 25;
            int xStart = 120;
            int xSumm = 350;

            foreach (var rProduct in order.ReservationProducts)
            {
                if (rProduct.Product.Name.Length > 20)
                {
                    rProduct.Product.Name = new string(rProduct.Product.Name.Take(20).ToArray());
                }
                var nameItem = new TicketItemInfo { Name = rProduct.Product.Name, PositionInfo = new PositionInfo { FontSize = fontSize, X = xStart, Y = startY } };
                items.Add(nameItem);
                startY += nextLinePixels;

                var priceCount = $"{rProduct.Price} X {rProduct.Count} =";
                var priceItem = new TicketItemInfo { Name = priceCount, PositionInfo = new PositionInfo { FontSize = fontSize, X = xStart, Y = startY } };
                items.Add(priceItem);

                var totalPriceValue = (double)rProduct.Price * rProduct.Count;
                var stringTotalPrice = string.Format("{0:F2}", totalPriceValue);
                var totalPrice = new TicketItemInfo { Name = stringTotalPrice, PositionInfo = new PositionInfo { FontSize = fontSize, X = xSumm, Y = startY } };
                items.Add(totalPrice);
                startY += nextLinePixels;

                //var disountItem = new TicketItemInfo { Name = "Скидка", PositionInfo = new PositionInfo { FontSize = fontSize, X = xStart, Y = startY } };
                var disountItem = new TicketItemInfo { Name = "Discount", PositionInfo = new PositionInfo { FontSize = fontSize, X = xStart, Y = startY } };

                items.Add(disountItem);

                var totalDiscountValue = (double)rProduct.DiscountValue * rProduct.Count;
                var stringDiscount = string.Format("{0:F2}", totalDiscountValue);
                var disountItemValue = new TicketItemInfo { Name = $"-{stringDiscount}", PositionInfo = new PositionInfo { FontSize = fontSize, X = xSumm, Y = startY } };
                items.Add(disountItemValue);
                startY += (nextLinePixels * 2);

            }

            //var summ = new TicketItemInfo { Name = "Сумма", PositionInfo = new PositionInfo { FontSize = fontSize, X = xStart, Y = startY } };
            var summ = new TicketItemInfo { Name = "Amount", PositionInfo = new PositionInfo { FontSize = fontSize, X = xStart, Y = startY } };

            items.Add(summ);

            var summValue = CalculateTotalSumm(order);
            var stringSummValue = string.Format("{0:F2}", summValue);
            var totalSummItem = new TicketItemInfo { Name = stringSummValue, PositionInfo = new PositionInfo { FontSize = fontSize, X = xSumm, Y = startY } };
            items.Add(totalSummItem);
            startY += (nextLinePixels * 2);

            //var systemInfo = new TicketItemInfo { Name = "-------Служебная информация-------", PositionInfo = new PositionInfo { FontSize = fontSize, X = xStart, Y = startY } };
            var systemInfo = new TicketItemInfo { Name = "-------Service info-------", PositionInfo = new PositionInfo { FontSize = fontSize, X = xStart, Y = startY } };

            items.Add(systemInfo);
            startY += (nextLinePixels * 2);

            //var discount = new TicketItemInfo { Name = "Скидка", PositionInfo = new PositionInfo { FontSize = fontSize, X = xStart, Y = startY } };
            var discount = new TicketItemInfo { Name = "Discount", PositionInfo = new PositionInfo { FontSize = fontSize, X = xStart, Y = startY } };
            items.Add(discount);

            var discountValue = CalculateTotalDiscount(order);
            var stringDiscountValue = string.Format("{0:F2}", discountValue);
            var totalDiscountItem = new TicketItemInfo { Name = $"{stringDiscountValue}", PositionInfo = new PositionInfo { FontSize = fontSize, X = xSumm, Y = startY } };
            items.Add(totalDiscountItem);
            startY += nextLinePixels;

            var date = new TicketItemInfo { Name = order.OrderDate.ToString(), PositionInfo = new PositionInfo { FontSize = fontSize, X = xStart, Y = startY } };
            items.Add(date);

            return items;
        }

        private double CalculateTotalSumm(Order order)
        {
            var summ = 0d;
            foreach (var rProduct in order.ReservationProducts)
            {
                var totalPriceValue = (double)rProduct.Price * rProduct.Count;
                summ += Math.Round(totalPriceValue, 2);

                var totalDiscountValue = (double)rProduct.DiscountValue * rProduct.Count;
                summ -= Math.Round(totalDiscountValue, 2);
            }

            return summ;
        }

        private double CalculateTotalDiscount(Order order)
        {
            var discount = 0d;
            foreach (var rProduct in order.ReservationProducts)
            {
                var totalDiscountValue = (double)rProduct.DiscountValue * rProduct.Count;
                discount -= Math.Round(totalDiscountValue, 2);
            }

            return discount;
        }

        private TicketModel GetTicketItemsHeader(string bodyJson, Order order)
        {
            var ticketPrintModel = GetTicketPrintModel(order);
            var tokens = GetTokens(ticketPrintModel);
            var ticketModel = GetTicketModelHeader(bodyJson, tokens);
            
            return ticketModel;
        }

        private TicketPrintModel GetTicketPrintModel(Order order)
        {
            var ticketModel = new TicketPrintModel
            {
                StoreName = order.Store.Name,
                StoreAddress = order.Store.Address,
                CashierName = $"{order.Employee.SurName} {order.Employee.Name}",
                OrderNumber = order.OrderNumber
            };

            return ticketModel;
        }

        private Dictionary<string, object> GetTokens(TicketPrintModel ticketPrintModel)
        {
            var tokens = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
            var tokenProvider = new TokenProvider();
            tokenProvider.AddTicketTokens(tokens, ticketPrintModel);
            return tokens;
        }

        private Dictionary<string, object> GetTokens(PriceHolderPrintModel priceHolderPrintModel)
        {
            var tokens = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
            var tokenProvider = new TokenProvider();
            tokenProvider.AddPriceHolderTokens(tokens, priceHolderPrintModel);
            return tokens;
        }

        private TicketModel GetTicketModelHeader(string bodyJson, Dictionary<string, object> tokens)
        {
            var ticketModel = new TicketModel();

            ticketModel.ItemsInfo = JsonConvert.DeserializeObject<List<TicketItemInfo>>(bodyJson);
            foreach (var itemInfo in ticketModel.ItemsInfo)
            {
                if (itemInfo.Name.StartsWith('{') && itemInfo.Name.EndsWith('}'))
                {
                    var name = itemInfo.Name.Trim(new char[] { '{', '}' });
                    itemInfo.Name = tokens.ContainsKey(name) ? tokens[name]?.ToString() : "";
                }
            }
            return ticketModel;
        }

        private string GetRequestAddress(string remoteIpAddress, string requestAddress)
        {
            if (remoteIpAddress != LocalIP)
            {
                return requestAddress.Replace("localhost", remoteIpAddress);
            }
            return requestAddress;
        }

        private async Task<bool> SendPrintRequestAsync(PrintModel model, string remoteIpAddress)
        {
            var requestAddress = GetRequestAddress(remoteIpAddress, printSettings.PrintServiceHost);

            using (var request = new HttpRequestMessage(HttpMethod.Post, requestAddress))
            using (request.Content = JsonContent.Create(model))
            {
                var client = _clientFactory.CreateClient();
                try
                {
                    var response = await client.SendAsync(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
                
            }
        }


    }
}
