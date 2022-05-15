using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
        private string _userName => User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("{id:guid}")]
        public async Task<Store> GetByIdAsync(Guid id)
        {
            return await _storeService.GetByIdAsync(id);
        }

        [HttpGet] //тип запроса
        [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог увидеть все свои магазины
        public async Task<IEnumerable<Store>> GetByUserAsync()                    //использую
        {
            return await _storeService.GetAsync(_userName);
        }

        [HttpPost("addstorewithemployeewithoutimage")]
        [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог создать магазин
        public async Task<IActionResult> AddWithEmployeeAsync(AddStoreWithEmployeeViewModel storeModel)                    //использую
        {
            var id = await _storeService.AddWithEmployeeAsync(storeModel, _userName);
            if (id != Guid.Empty)
            {
                return Ok(id);
            }
            return BadRequest(Guid.Empty);
        }

        [HttpPost("addstorewithoutemployeewithoutimage")]
        [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог создать магазин
        public async Task<IActionResult> AddWithoutEmployeeAsync(AddStoreWithoutEmployeeViewModel storeModel)                    //использую
        {
            var id = await _storeService.AddWithoutEmployeeAsync(storeModel, _userName);
            if (id != Guid.Empty)
            {
                return Ok(id);
            }
            return BadRequest(Guid.Empty);
        }

        [HttpPost("addstoreimage/{id:guid}")]
        public async Task<IActionResult> AddStoreImageAsync(Guid id)                                         //использую
        {
            try
            {
                var file = Request.Form.Files[0];

                if (ModelState.IsValid) // может нужно убрать
                {
                    if (await _storeService.AddImageAsync(file, id))
                    {
                        return Ok(true);
                    }
                }
                return BadRequest(false);
            }
            catch
            {
                return BadRequest(false);
            }
        }

    }
}
