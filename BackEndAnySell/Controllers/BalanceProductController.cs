using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
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
    public class BalanceProductController : Controller
    {
        public readonly IBalanceProductService _balanceProductService;

        //   private Guid _userId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

       // private string _userName => User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        public BalanceProductController(IBalanceProductService balanceProductService)
        {
            _balanceProductService = balanceProductService;
        }


       /*  [HttpGet("{id:guid}")]                                                                                   //использую
        public async Task<IActionResult> GetByIdAsync(Guid id)      
        {
            return Ok(await _balanceProductService.GetByIdAsync(id));
        }

        [HttpGet("getstorediscount/{storeId:guid}")]                                                              //использую
        public async Task<IActionResult> GetByStoreIdAsync(Guid storeId)
        {
            return Ok(await _balanceProductService.GetByStoreIdAsync(storeId));
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]  // только менеджер может создать скидку
        public async Task<IActionResult> AddAsync(AddDiscountViewModel discountModel)
        {
            if (await _balanceProductService.AddAsync(discountModel))
            {
                return Ok(true);
            }
            return Ok(false);
        }

       [HttpPost("updatediscount")]                                                                               //использую
        [Authorize(Roles = "Manager")]  // только менеджер может отредактировать скидку
        public async Task<IActionResult> UpdateAsync(UpdateDiscountViewModel discountModel)
        {
            if (await _discountrService.UpdateAsync(discountModel))
            {
                return Ok(true);
            }
            return Ok(false);
        }


        [HttpGet("deletediscount/{id:guid}")]                                                                      //использую
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
          if (await _discountrService.DeleteAsync(id))
            {
                return Ok(true);
            }
            return Ok(false);
        }*/
    }
}
