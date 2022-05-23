using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Enums;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Employee>> GetByStoreAsync(Guid storeId)
        {
            return await _employeeRepository.GetByStoreAsync(storeId);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee.Role != Role.Manager)
            {
                employee.IsDeleted = true;
                return await _employeeRepository.DeleteAsync(employee);
            }
            return false;
        }
    }
}
