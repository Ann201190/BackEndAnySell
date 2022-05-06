using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels;
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
    public class StoreController : Controller
    {
        public readonly IStoreService _storeService;

        //  private Guid _userId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        // private string _userName => User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        /*  [HttpGet ("{email}")] //тип запроса
          [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог увидеть все свои магазины
          public async Task<IEnumerable<Store>> GetByUserAsync(string email)                    //использую
          {
              return await _storeService.GetAsync(email);
          }*/


        [HttpPost] //тип запроса
        [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог увидеть все свои магазины
        public async Task<IEnumerable<Store>> GetByUserAsync(GetStoreByEmail email)                    //использую
        {
            return await _storeService.GetAsync(email.Email);
        }

        [HttpGet("{id:guid}")]
        public async Task<Store> GetByIdAsync(Guid id)
        {
            return await _storeService.GetByIdAsync(id);         
        }

        [HttpPost("addstorewithoutimage")]
        [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог создать магазин
        public async Task<IActionResult> AddAsync(AddStoreViewModel store)                    //использую
        {
            var rezult = await _storeService.AddAsync(store);
            if (rezult)
            {
                return Ok(true);
            }
            return BadRequest(false);
        }
    }
}
