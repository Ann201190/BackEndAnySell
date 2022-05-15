using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<Employee> GetAsync(string userName)
        {
            return await _employeeRepository.GetAsync(userName);
        }
    }
}
