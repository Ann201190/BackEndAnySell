using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace BackEndAnySellAccessDataAccess.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<bool> AddAsync(Employee employee);
        Task<Employee> GetAsync(string userName);
        Task<bool> UpdateAsync(Employee employee);
        Task<IEnumerable<Employee>> GetByStoreAsync(Guid storeId);
        Task<Employee> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Employee employee);
        Task<bool> AddPhotoAsync(byte[] fileArrayBytes, Guid id);
    }
}
