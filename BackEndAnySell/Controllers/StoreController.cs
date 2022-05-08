using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("addstorewithoutimage")]
        [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог создать магазин
        public async Task<IActionResult> AddAsync(AddStoreViewModel storeModel)                    //использую
        {
            if (await _storeService.AddAsync(storeModel))
            {
                return Ok(true);
            }
            return BadRequest(false);
        }

        [HttpPost("addstoreimage")]
        public async Task<IActionResult> AddStoreImageAsync()                                         //использую
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Upload";
                //  string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = "G:\\Диплом";
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                string fileName = "";
                if (file.Length > 0)
                {
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                //   return Ok(fileName);
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                //  return BadRequest(ex.Message);
                return BadRequest(false);
            }
        }
    }
}
