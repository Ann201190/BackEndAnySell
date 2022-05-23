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
    public class EmployeeController : Controller
    {
        public readonly IEmployeeService _employeeService;

        private string _userName => User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet] //тип запроса                                                                                      //использую
        [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог увидеть все свои магазины
        public async Task<IActionResult> GetByUserAsync()                                                            
        {       
              var employee = await _employeeService.GetAsync(_userName);

            if (employee!=null)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpGet("getemployeestore/{storeId:guid}")]                                                                                  //использую
        [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог увидеть все свои магазины
        public async Task<IEnumerable<Employee>> GetByStoreAsync(Guid storeId)                                                                            //использую
        {
            return await _employeeService.GetByStoreAsync(storeId);
        }


        [HttpGet("deleteemployee/{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)                                                                        //использую
        {
            if (await _employeeService.DeleteAsync(id))
            {
                return Ok(true);
            }
            return Ok(false);
        }
    }
}
