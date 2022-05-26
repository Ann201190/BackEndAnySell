using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Enums;
using BackEndAnySellDataAccess.Repositories.Interfaces;
using BackEndSellViewModels.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IStoreRepository _storeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository, IStoreRepository storeRepository)
        {
            _employeeRepository = employeeRepository;
            _storeRepository = storeRepository;
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            return await _employeeRepository.GetByIdAsync(id);
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

        public async Task<Guid> AddAsync(AddEmployeeWithoutPhotoViewModel employeeModel, Guid storeId)
        {
            var employee = new Employee()
            {
                Id = employeeModel.Id,
                Name = employeeModel.Name,
                SurName = employeeModel.SurName,
                Email = employeeModel.Email,
                Phone = employeeModel.Phone,
                Address = employeeModel.Address,
                Role =Role.Cashier
            };
        
            var store = await _storeRepository.GetByIdAsync(storeId);
            employee.Stores.Add(store);
     
            var isAddedEmployee = await _employeeRepository.AddAsync(employee);               

            if (isAddedEmployee)
            {
                return employee.Id;
            }
            return Guid.Empty;
        }

         public async Task<Guid> UpdateAsync(UpdateEmployeeWithoutPhotoViewModel employeeModel)
         {
             var employee = await _employeeRepository.GetByIdAsync(employeeModel.Id);

             employee.Name = employeeModel.Name;
             employee.SurName = employeeModel.SurName;
             employee.Phone = employeeModel.Phone;
             employee.Address = employeeModel.Address;

             var isUpdatedEmployee = await _employeeRepository.UpdateAsync(employee);

             if (isUpdatedEmployee)
             {
                 return employee.Id;
             }
             return Guid.Empty;
         }

        public async Task<bool> AddPhotoAsync(IFormFile file, Guid id)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return await _employeeRepository.AddPhotoAsync(ms.ToArray(), id);
            }
        }
    }
}
