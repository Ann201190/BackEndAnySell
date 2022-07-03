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
    public class BalanceProductController : Controller
    {
        public readonly IBalanceProductService _balanceProductService;

        public BalanceProductController(IBalanceProductService balanceProductService)
        {
            _balanceProductService = balanceProductService;    
        }
   
    }
}
