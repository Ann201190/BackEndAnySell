using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> GetAsync(string userName);
        Task<IEnumerable<Employee>> GetByStoreAsync(Guid storeId);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AddAsync(AddEmployeeViewModel employeeModel, Guid storeId);
        Task<Employee> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(UpdateEmployeeViewModel employeeModel);
    }
}
