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
    public class StoreController : Controller
    {
        public readonly IStoreService _storeService;

        //  private Guid _userId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        private string _userName => User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }
         
        [HttpGet("{id:guid}")]                                                                                            //использую
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await _storeService.GetByIdAsync(id));
        }

        [HttpGet] //тип запроса
      //  [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог увидеть все свои магазины
        public async Task<IActionResult> GetByUserAsync()                                                             //использую
        {
              return Ok(await _storeService.GetAsync(_userName));
        }

        [HttpPost("addstorewithemployeewithoutimage")]
        [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог создать магазин
        public async Task<IActionResult> AddWithEmployeeAsync(AddStoreWithEmployeeViewModel storeModel)                    //использую
        {
            var ids = await _storeService.AddWithEmployeeAsync(storeModel, _userName);
            
                return Ok(new {
                    storeId = ids.Item1,
                    employeeId = ids.Item2,
                });          
        }

        [HttpPost("addstorewithoutemployeewithoutimage")]
        [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог создать магазин
        public async Task<IActionResult> AddWithoutEmployeeAsync(AddStoreWithoutEmployeeViewModel storeModel)                 //использую
        {
            return Ok(await _storeService.AddWithoutEmployeeAsync(storeModel, _userName));
        }

        [HttpPost("addstoreimage/{id:guid}")]
        public async Task<IActionResult> AddStoreImageAsync(Guid id)                                                            //использую
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
                return Ok(false);
            }
            catch
            {
                return Ok(false);
            }
        }

        [HttpGet("deleteimage/{id:guid}")]                                                                                            //использую
        public async Task<IActionResult> DeleteImageAsync(Guid id)
        {
            var result = await _storeService.DeleteImageAsync(id);
            return Ok(result);
        }

        [HttpPost("updatestorewithouteimge")]
        public async Task<IActionResult> UpdateStoreIAsync(UpdateStoreWithoutImgeViewModel storeModel)                         //использую
        {
            return Ok(await _storeService.UpdateStoreAsync(storeModel));
        }

        [HttpGet("deletestore/{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)                                                                  //использую
        {
            if (await _storeService.DeleteAsync(id))
            {
                return Ok(true);
            }
            return Ok(false);
        }
    }
}
