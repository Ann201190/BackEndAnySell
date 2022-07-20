using BackEndAnySellBusiness.Services.Interfaces;
using BackEndSellViewModels.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackEndAnySell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintController : ControllerBase
    {
        private readonly IPrintService _printService;
        private string _userName => User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;

        public PrintController(IPrintService printService)
        {
            _printService = printService;
        }

        [HttpPost("print/{orderNumber}")]
        public async Task<IActionResult> Print(string orderNumber)
        {  
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var isPrinted = await _printService.PrintAsync(_userName, orderNumber, remoteIpAddress);
            
            return Ok(isPrinted);
        }

        [HttpPost("printpriceholder/{productId:Guid}")]
        public async Task<IActionResult> PrintPriceHolder(Guid productId)
        {  
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var isPrinted = await _printService.PrintPriceHolderAsync(_userName, productId, remoteIpAddress);
            
            return Ok(isPrinted);
        }

        [HttpPost("printallpriceholders/{storeId:Guid}")]
        public async Task<IActionResult> PrintAllPriceHolders(Guid storeId)
        {
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var isPrinted = await _printService.PrintAllPriceHoldersAsync(_userName, storeId, remoteIpAddress);

            return Ok(isPrinted);
        }

        [HttpGet("getprinters")]
        public async Task<List<string>> GetPrinters()
        {
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            return await _printService.GetPrintersAsync(remoteIpAddress);
        }

        [HttpPost("setprintersettings")]
        public async Task<IActionResult> SetPrinterSettings(PrinterSettings printerSettings)
        {
            var isSavedChanges = await _printService.SetPrinterSettings(_userName, printerSettings);

            return Ok(isSavedChanges);
        }
    }
}
