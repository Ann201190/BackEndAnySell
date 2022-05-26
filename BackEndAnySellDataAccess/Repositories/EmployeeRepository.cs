using BackEndAnySellAccessDataAccess.Context;
using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return false;
        }

        public async Task<bool> AddPhotoAsync(byte[] fileArrayBytes, Guid id)
        {
            if (fileArrayBytes != null)
            {
                var employees = await GetByIdAsync(id);

                employees.Photo = fileArrayBytes;
                _dbContext.Employees.Update(employees);

                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }

        public async Task<Employee> GetAsync(string userName)
        {
            return await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Email == userName);
        }

        public async Task<IEnumerable<Employee>> GetByStoreAsync(Guid storeId)
        {
            return await _dbContext.Employees
                 .AsNoTracking()               
                   .Where(e => e.Stores.Any(s=>s.Id== storeId)&& !e.IsDeleted)
                 .ToListAsync();
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


        public async Task<Employee> GetByIdAsync(Guid id)
        {
            return await _dbContext.Employees
                .Include(e => e.Stores)
                     .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> DeleteAsync(Employee employee)
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

