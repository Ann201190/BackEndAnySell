using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackEndAnySell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProviderController : Controller
    {
        public readonly IProviderService _providerService;
        private string _userName => User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet("{id:guid}")]                                                                                            
        public async Task<IActionResult> GetByIdAsync(Guid id)                            //использую
        {
            return Ok(await _providerService.GetByIdAsync(id));
        }

        [HttpGet]                                                                               
        public async Task<IActionResult> GetAsync()                                                        //использую
        {
            return Ok(await _providerService.GetAsync(_userName));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddProviderViewModel providerModel)                       //использую
        {
            if (await _providerService.AddAsync(providerModel, _userName))
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("updateprovider")]                                                                            
        public async Task<IActionResult> UpdateAsync(UpdateProviderViewModel providerModel)              //использую
        {
            if (await _providerService.UpdateAsync(providerModel))
            {
                return Ok(true);
            }
            return Ok(false);
        }


        [HttpGet("deleteprovider/{id:guid}")]                                                            //использую                                 
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
          if (await _providerService.DeleteAsync(id))
            {
                return Ok(true);
            }
            return Ok(false);
        }
    }
}
