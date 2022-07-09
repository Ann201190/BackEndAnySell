using BackEndAnySellBusiness.Services.Interfaces;
using BackEndSellViewModels.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BackEndAnySell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        public readonly IOrderService _orderService;
       
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


        [HttpPost]                                                                                                   //использую                                                                                                               
        public async Task<IActionResult> AddAsync(AddOrderViewModel orderModel)
        {
            return Ok(await _orderService.AddAsync(orderModel));        
        }
    }
}
