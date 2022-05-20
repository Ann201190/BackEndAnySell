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


        /*  [HttpGet("{id:guid}")] //тип запроса
          public async Task<Product> GetAsync(Guid id)
          {
              return await _productService.GetAsync(id);
          }*/


        //http://localhost:80/api/product/getstoreproduct/BFBC7481-FB3C-4192-A093-519F40F1B812          для ииса
        [HttpGet("getstoreproduct/{storeId:guid}")]                                                                              //использую
        public async Task<IEnumerable<Product>> GetByStoreIdAsync(Guid storeId)
        {
            return await _productService.GetByStoreIdAsync(storeId);
        }


          [HttpPost("addproductimage/{id:guid}")]
          public async Task<IActionResult> AddProductImageAsync(Guid id)                                                         //использую
        {
            try
            {
                var file = Request.Form.Files[0];

                if (ModelState.IsValid)
                {
                    if (await _productService.AddImageAsync(file, id))
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

        [HttpPost("addproductwithoutimage")]
        public async Task<IActionResult> AddProductWithoutImageAsync(AddProductWithoutImgeViewModel productModel)                  //использую
        {
            var id = await _productService.AddWithoutImgeAsync(productModel);
            if (id != Guid.Empty)
            {
                return Ok(id);
            }
            return BadRequest(Guid.Empty);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateAsync(Product product)
        {
            if (await _productService.UpdateAsync(product))
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (await _productService.DeleteAsync(id))
            {
                return Ok(true);
            }
            return Ok(false);
        }

    }
}
