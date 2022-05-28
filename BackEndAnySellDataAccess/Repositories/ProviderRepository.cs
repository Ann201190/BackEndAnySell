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
    public class ProviderRepository : IProviderRepository
    {
        private readonly CustomDbContext _dbContext;
        public ProviderRepository(CustomDbContext dbContext)
        {
            _dbContext = dbContext;
            ModelBuilderExtensions.Initialize(dbContext);
            var builder = new DbContextOptionsBuilder<CustomDbContext>();
        }

        public async Task<bool> AddAsync(Provider discount)
        {
            if (discount != null)
            {
                await _dbContext.Providers.AddAsync(discount);

                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }

        public async Task<IEnumerable<Provider>> GetAsync(string userName)
        {
            return await _dbContext.Providers
                .AsNoTracking()
                 .Where(p => p.Employee.Email == userName)
                .ToListAsync();
        }

        public async Task<Provider> GetByIdAsync(Guid id)
        {
            return await _dbContext.Providers
                .Include(p => p.Employee)
                .Include(p => p.Comings)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> UpdateAsync(Provider provider)
        {
            if (provider != null)
            {
                _dbContext.Providers.Update(provider);
                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
                return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
             var provider = await GetByIdAsync(id);
            _dbContext.Providers.Remove(provider);

             return await _dbContext.SaveChangesAsync() >= 0 ? true : false;    
        }
    }
}
