using BackEndAnySellBusiness.Services.Interfaces;
using BackEndSellViewModels.ViewModel;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await _orderService.GetByIdAsync(id));
        }

        [Authorize]
        [HttpGet(("getstoreorder/{storeId:guid}"))]
        public async Task<IActionResult> GetByStoreIdAsync(Guid storeId)
        {
            return Ok(await _orderService.GetByStoreIdAsync(storeId));
        }

        [Authorize]
        [HttpGet("getcheccashier/{storeId:guid}")]
        public async Task<IActionResult> GetChecCashierAsync(Guid storeId)
        {
            return Ok(await _orderService.GetChecCashierAsync(storeId));
        }

        [Authorize]
        [HttpGet("getcashboxproduct/{storeId:guid}")]                                            //использую
        public async Task<IActionResult> GetProductByStoreIdAsync(Guid storeId)
        {
            return Ok(await _orderService.GetProductByStoreIdAsync(storeId));
        }

        [HttpGet("getcheck/{orderNumber}")]                                                                             //использую
        public async Task<IActionResult> GetCheckAsync(string orderNumber)
        {
            return Ok(await _orderService.GetCheckAsync(orderNumber));
        }

        [Authorize]
        [HttpGet("getstorecheck/{storeId:guid}/{orderNumber}")]                                                                             //использую
        public async Task<IActionResult> GetCheckAsync(Guid storeId, string orderNumber)
        {
            return Ok(await _orderService.GetCheckAsync(storeId, orderNumber));
        }

        [Authorize]
        [HttpGet("getprofit/{storeId:guid}")]                                                                             //использую
        public async Task<IActionResult> GetProfitAsync(Guid storeId)
        {
            return Ok(await _orderService.GetProfitAsync(storeId));
        }

        [Authorize]
        [HttpGet("gettopthreeproduct/{storeId:guid}")]                                                                             //использую
        public async Task<IActionResult> GetProductMontheAsync(Guid storeId)
        {
            return Ok(await _orderService.GetProductMontheAsync(storeId));
        }

        [Authorize]
        [HttpPost]                                                                                                   //использую                                                                                                               
        public async Task<IActionResult> AddAsync(AddOrderViewModel orderModel)
        {
          var request =  await _orderService.AddAsync(orderModel, _userName);
      
            return Ok(new
            {
                access_qrcode = request
            }) ;         
        }

        [Authorize]
        [HttpPost("productsreturn")]                                                                                                 //использую                                                                                                               
        public async Task<IActionResult> ProductsReturn(ProductsReturnViewModel request)
        {
            var response = await _orderService.ProductsReturn(request.OrderNumber, request.ReservationProductIds);

            return Ok(new
            {
                access_qrcode = response
            });
        }
    }
}
