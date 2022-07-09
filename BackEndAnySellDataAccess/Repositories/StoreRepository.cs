using BackEndAnySellAccessDataAccess.Context;
using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAnySellDataAccess.Repositories
{
    public class StoreRepository: IStoreRepository
    {
        private readonly CustomDbContext _dbContext;
        public StoreRepository(CustomDbContext dbContext)
        {
            _dbContext = dbContext;
            ModelBuilderExtensions.Initialize(dbContext);
            var builder = new DbContextOptionsBuilder<CustomDbContext>();
        }

        public async Task<bool> AddAsync(Store store)
        {          
            if (store != null )
            {
                await _dbContext.Stores.AddAsync(store);
                 
                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }

        public async Task<bool> AddImageAsync(byte[] fileArrayBytes, Guid id)
        {
            if (fileArrayBytes != null)
            {
                var store = await GetByIdAsync(id);

                store.LogoImage = fileArrayBytes;
                 _dbContext.Stores.Update(store);

                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Store store)
        {
            if (store != null)
            {
                _dbContext.Stores.Update(store);
                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }

        public async Task<bool> DeleteImageAsync(Guid id)
        {
            var store = await GetByIdAsync(id);
            if (store != null)
            {
                store.LogoImage = null;
                _dbContext.Stores.Update(store);
                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }

        public async Task<IEnumerable<Store>> GetAsync(string userName)
        {
            return await _dbContext.Stores
                   .AsNoTracking()
             //   .Include(s => s.Discounts)
             //   .Include(s => s.Products)
            //    .Include(s => s.Employees)
                    .Where(s => s.Employees.Any(e => e.Email == userName && !e.IsDeleted) && !s.IsDeleted)  //по почте, по конкретному юзеру только для Менеджера и не удаленные
                    .OrderBy(p => p.Name)
                   .ToListAsync();
        }

        public async Task<Store> GetByIdAsync(Guid id)
        {
            return await _dbContext.Stores
             //   .Include(s => s.Discounts)
             //   .Include(s => s.Products)
            //    .Include(s => s.Employees)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> UpdateAsync(Store store)
        {
            if (store != null)
            {
                _dbContext.Stores.Update(store);
                return  await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }
    }
}
