using BackEndAnySellBusiness.Services.Interfaces;
using BackEndSellViewModels.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackEndAnySell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        public readonly IOrderService _orderService;
        private string _userName => User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await _orderService.GetByIdAsync(id));
        }

        [HttpGet(("getstoreorder/{storeId:guid}"))]                                      
        public async Task<IActionResult> GetByStoreIdAsync(Guid storeId)
        {
            return Ok(await _orderService.GetByStoreIdAsync(storeId));
        }

        [HttpGet("getcheccashier/{storeId:guid}")]
        public async Task<IActionResult> GetChecCashierAsync(Guid storeId)
        {
            return Ok(await _orderService.GetChecCashierAsync(storeId));
        }
       
        [HttpGet("getcashboxproduct/{storeId:guid}")]                                                              //использую
        public async Task<IActionResult> GetProductByStoreIdAsync(Guid storeId)
        {
            return Ok(await _orderService.GetProductByStoreIdAsync(storeId));
        }

        [HttpGet("getcheck/{orderNumber}")]                                                                             //использую
        public async Task<IActionResult> GetCheckAsync(string orderNumber)
        {
            return Ok(await _orderService.GetCheckAsync(orderNumber));
        }

        [HttpGet("getstorecheck/{storeId:guid}/{orderNumber}")]                                                                             //использую
        public async Task<IActionResult> GetCheckAsync(Guid storeId, string orderNumber)
        {
            return Ok(await _orderService.GetCheckAsync(storeId, orderNumber));
        }

        [HttpGet("getprofit/{storeId:guid}")]                                                                             //использую
        public async Task<IActionResult> GetProfitAsync(Guid storeId)
        {
            return Ok(await _orderService.GetProfitAsync(storeId));
        }

        [HttpGet("gettopthreeproduct/{storeId:guid}")]                                                                             //использую
        public async Task<IActionResult> GetProductMontheAsync(Guid storeId)
        {
            return Ok(await _orderService.GetProductMontheAsync(storeId));
        }

        [HttpPost]                                                                                                   //использую                                                                                                               
        public async Task<IActionResult> AddAsync(AddOrderViewModel orderModel)
        {
          var request =  await _orderService.AddAsync(orderModel, _userName);
      
            return Ok(new
            {
                access_qrcode = request
            }) ;         
        }
    }
}
