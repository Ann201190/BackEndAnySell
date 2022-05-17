using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet] //тип запроса
        [Authorize(Roles = "Manager")] // запрос только для директора, чтобы он мог увидеть все свои магазины
        public async Task<IActionResult> GetByUserAsync()                                                            //использую
        {       
              var employee = await _employeeService.GetAsync(_userName);

            if (employee!=null)
            {
                return Ok(true);
            }
            return Ok(false);
        }
    }
}
