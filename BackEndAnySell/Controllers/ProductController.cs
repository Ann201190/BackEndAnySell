using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BackEndAnySell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        public readonly IProductService _productService;

        //  private Guid _userId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet] //тип запроса
        [Authorize (Roles = "Manager")]
        public async Task<IEnumerable<Product>> GetAsync()                                             //использую
        {
            return await _productService.GetAsync();
        }


        /*  [HttpGet("{id:guid}")] //тип запроса
          public async Task<Product> GetAsync(Guid id)
          {
              return await _productService.GetAsync(id);
          }*/


        //http://localhost:80/api/product/getstoreproduct/BFBC7481-FB3C-4192-A093-519F40F1B812                     для ииса
        [HttpGet("getstoreproduct/{storeId:guid}")]
        public async Task<IEnumerable<Product>> GetByStoreIdAsync(Guid storeId)
        {
            return await _productService.GetByStoreIdAsync(storeId);
        }


       // http://localhost:80/api/product/addproduct     для ииса
        [HttpPost("addproduct")]
        public async Task<IActionResult> AddAsync([FromBody]Product product)       
        {
            if (await _productService.AddAsync(product))
            {
                return Ok(true);
            }
            return BadRequest(false);     
        }

          [HttpPost("addproductimage")]
          public async Task<IActionResult> AddProductImageAsync()                                         //использую
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

        [HttpPost("addproductwithoutimage")]
        public async Task<IActionResult> AddProductWithoutImageAsync([FromBody] Product product)                  //использую
        {
            if (await _productService.AddAsync(product))
            {
                return Ok(true);
            }
            return BadRequest(false);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateAsync(Product product)
        {
            if (await _productService.UpdateAsync(product))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (await _productService.DeleteAsync(id))
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}
