using BackEndAnySellDataAccess.Entities;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> GetAsync(string userName);
    }
}
