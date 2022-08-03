using BackEndAnySellBusiness.Services.Interfaces;
using BackEndSellViewModels.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackEndAnySell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : Controller
    {
        public readonly IEmployeeService _employeeService;

        private string _userName => User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet] //тип запроса                                                                                            //использую
        //  [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог увидеть все свои магазины
        public async Task<IActionResult> GetByUserAsync()
        {
            var employee = await _employeeService.GetAsync(_userName);

            if (employee != null)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpGet("getemployee")]
        public async Task<IActionResult> GetUserPrintersInformationAsync()
        {
            return Ok(await _employeeService.GetAsync(_userName));
        }

        [HttpGet("getemployeestore/{storeId:guid}")]                                                                       //использую
        // [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог увидеть все свои магазины
        public async Task<IActionResult> GetByStoreAsync(Guid storeId)
        {
            return Ok(await _employeeService.GetByStoreAsync(storeId));
        }


        [HttpGet("deleteemployee/{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)                                                               //использую
        {
            if (await _employeeService.DeleteAsync(id))
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("addemployeewithoutphoto/{storeId:guid}")]
        [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог создать магазин
        public async Task<IActionResult> AddEmployeeAsync(AddEmployeeWithoutPhotoViewModel employeeModel, Guid storeId)                         //использую
        {
            return Ok(await _employeeService.AddAsync(employeeModel, storeId));
        }

        [HttpPost("addemployeephoto/{id:guid}")]
        public async Task<IActionResult> AddEmployeepPhotoAsync(Guid id)                                                         //использую
        {
            try
            {
                var file = Request.Form.Files[0];

                if (ModelState.IsValid)
                {
                    if (await _employeeService.AddPhotoAsync(file, id))
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

        [HttpGet("{id:guid}")]                                                                                               //использую
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await _employeeService.GetByIdAsync(id));
        }

        [HttpPost("updateemploeewithoutphoto")]                                                                                          //использую
        [Authorize(Roles = "Manager")]  // только менеджер может отредактировать скидку
        public async Task<IActionResult> UpdateAsync(UpdateEmployeeWithoutPhotoViewModel employeeModel)
        {
            return Ok(await _employeeService.UpdateAsync(employeeModel));
        }


        [HttpGet("deleteimage/{id:guid}")]                                                                                            //использую
        public async Task<IActionResult> DeleteImageAsync(Guid id)
        {
            var result = await _employeeService.DeleteImageAsync(id);
            return Ok(result);
        }
    }
}
