using BackEndSellViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services.Interfaces
{
    public interface IPrintService
    {
        Task<bool> PrintAsync(string employeeEmail, string orderNumber, string remoteIpAddress);
        Task<List<string>> GetPrintersAsync(string remoteIpAddress);
        Task<bool> PrintPriceHolderAsync(string employeeEmail, Guid productId, string remoteIpAddress);
        Task<bool> PrintAllPriceHoldersAsync(string userName, Guid storeId, string remoteIpAddress);
        Task<bool> SetPrinterSettings(string userName, PrinterSettings printerSettings);
    }
}
