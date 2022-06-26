using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Enums;
using BackEndSellViewModels.ViewModel;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAnySell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class CashBoxProductController : Controller
    {
        public readonly ICashBoxProductService _cashBoxProductService;
        public CashBoxProductController(ICashBoxProductService cashBoxProductService)
        {
            _cashBoxProductService = cashBoxProductService;
        }

        [HttpGet("getcashboxproduct/{storeId:guid}")]                                                              //использую
        public async Task<IActionResult> GetByStoreIdAsync(Guid storeId)
        {
            return Ok(await _cashBoxProductService.GetByStoreIdAsync(storeId));
        }
    }
}
