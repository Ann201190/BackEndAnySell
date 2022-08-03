using BackEndAnySellBusiness.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndAnySell.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BalanceProductController : Controller
    {
        public readonly IBalanceProductService _balanceProductService;

        public BalanceProductController(IBalanceProductService balanceProductService)
        {
            _balanceProductService = balanceProductService;    
        }
   
    }
}
