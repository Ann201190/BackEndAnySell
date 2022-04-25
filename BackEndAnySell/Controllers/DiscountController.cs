using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySell.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DiscountController : Controller
    {
        public readonly IDiscountService _discountrService;

        //   private Guid _userId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

       // private string _userName => User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        public DiscountController(IDiscountService discountrService)
        {
            _discountrService = discountrService;
        }


       /*   [HttpGet] //тип запроса
        public async Task<IEnumerable<Discount>> GetAsync()
        {
            return await _discountrService.GetAsync();
        }*/


        [HttpGet("{id:guid}")]
        public async Task<Discount> GetByIdAsync(Guid id)
        {
            return await _discountrService.GetByIdAsync(id);
        }

        [HttpGet("getstorediscount/{storeId:guid}")]
        public async Task<IEnumerable<Discount>> GetByStoreIdAsync(Guid storeId)
        {
            return await _discountrService.GetByStoreIdAsync(storeId);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]  // только менеджер может создать скидку
        public async Task<IActionResult> AddAsync(Discount discount)
        {
            var rezult = await _discountrService.AddAsync(discount);
            if (rezult)
            {
                return Ok("ok");
            }
            return BadRequest();
        }

        [HttpPost("updatediscont")]
        [Authorize(Roles = "Manager")]  // только менеджер может отредактировать скидку
        public async Task<IActionResult> UpdateAsync(Discount discount)
        {
            var rezult = await _discountrService.UpdateAsync(discount);
            if (rezult)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
