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
    public class ComingController : Controller
    {
        public readonly IComingService _comingService;

        //   private Guid _userId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

       // private string _userName => User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        public ComingController(IComingService comingService)
        {
            _comingService = comingService;
        }


        [HttpGet("{id:guid}")]                                                                                 
        public async Task<IActionResult> GetByIdAsync(Guid id)      
        {
            return Ok(await _comingService.GetByIdAsync(id));
        }

        [HttpGet("getstorecoming/{storeId:guid}")]                                                              //использую
        public async Task<IActionResult> GetByStoreIdAsync(Guid storeId)
        {
            return Ok(await _comingService.GetByStoreIdAsync(storeId));
        }

        [HttpPost]                                                                                                //использую
      //  [Authorize(Roles = "Manager")]  // только менеджер может создать скидку
        public async Task<IActionResult> AddAsync(AddComingViewModel comingModel)
        {
            if (await _comingService.AddAsync(comingModel))
            {
                return Ok(true);
            }
            return Ok(false);
        }

     /*  [HttpPost("updatecoming")]                                                                               //использую
      //  [Authorize(Roles = "Manager")]  // только менеджер может отредактировать скидку
        public async Task<IActionResult> UpdateAsync(UpdateComingViewModel comingModel)
        {
            if (await _comingService.UpdateAsync(comingModel))
            {
                return Ok(true);
            }
            return Ok(false);
        }*/


       /*  [HttpGet("deletediscount/{id:guid}")]                                                                      //использую
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
