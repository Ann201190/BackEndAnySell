using BackEndAnySellAccessDataAccess.Context;
using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAnySellAccessDataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CustomDbContext _dbContext;

        public EmployeeRepository(CustomDbContext dbContext)
        {
            _dbContext = dbContext;
            ModelBuilderExtensions.Initialize(dbContext);
            var builder = new DbContextOptionsBuilder<CustomDbContext>();
        }

        public async Task<bool> AddAsync(Employee employee)
        {
            if (employee == null)
            {
                return false;
            }

            var existingEmployee = await GetAsync(employee.Email);
       
            if (existingEmployee == null)
            {
                await _dbContext.Employees.AddAsync(employee);
                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return true;
        }

        public async Task<Employee> GetAsync(string userName)
        {
            return await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Email == userName);
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            if (employee != null)
            {
                _dbContext.Employees.Update(employee);
                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }
    }
}

