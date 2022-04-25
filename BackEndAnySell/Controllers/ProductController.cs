using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
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
        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await _productService.GetAsync();
        }


      /*  [HttpGet("{id:guid}")] //тип запроса
        public async Task<Product> GetAsync(Guid id)
        {
            return await _productService.GetAsync(id);
        }*/

        [HttpGet("getstoreproduct/{storeId:guid}")]
        public async Task<IEnumerable<Product>> GetByStoreIdAsync(Guid storeId)
        {
            return await _productService.GetByStoreIdAsync(storeId);
        }

        [HttpPost("addproduct")]
        public async Task<IActionResult> AddAsync(Product product)
        {             
            var rezult = await _productService.AddAsync(product);
            if (rezult)
            {
                return Ok();
            }
            return BadRequest();     
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(Product product)
        {
            var rezult = await _productService.UpdateAsync(product);
            if (rezult)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var rezult = await _productService.DeleteAsync(id);
            if (rezult)
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}
