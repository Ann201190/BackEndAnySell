using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
using Microsoft.AspNetCore.Http;
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
        Task<Guid> AddAsync(AddEmployeeWithoutPhotoViewModel employeeModel, Guid storeId);
        Task<Employee> GetByIdAsync(Guid id);
        Task<Guid> UpdateAsync(UpdateEmployeeWithoutPhotoViewModel employeeModel);
        Task<bool> AddPhotoAsync(IFormFile file, Guid id);
    }
}
