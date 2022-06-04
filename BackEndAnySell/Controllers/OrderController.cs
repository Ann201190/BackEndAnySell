using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        public readonly IOrderService _orderService;

       // private Guid _userId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
       
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
    }
}
