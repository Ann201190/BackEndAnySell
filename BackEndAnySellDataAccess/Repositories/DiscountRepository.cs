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
    public class DiscountRepository : IDiscountRepository
    {

        private readonly CustomDbContext _dbContext;

        public DiscountRepository(CustomDbContext dbContext)
        {
            _dbContext = dbContext;
            ModelBuilderExtensions.Initialize(dbContext);
            var builder = new DbContextOptionsBuilder<CustomDbContext>();
        }

        public async Task<bool> AddAsync(Discount discount)
        {
            if (discount != null)
            {
                await _dbContext.Discounts.AddAsync(discount);

                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }

        /*  public async Task<IEnumerable<Discount>> GetAsync()
          {
              return await _dbContext.Discounts
                  .AsNoTracking()
                  .Include(d => d.Store)
                  .Include(d => d.Products)
                  .ToListAsync();
          }*/


        public async Task<IEnumerable<Discount>> GetByStoreIdAsync(Guid storeId)
        {
            return await _dbContext.Discounts
                .AsNoTracking()
                .Include(d => d.Store)
                .Include(d => d.Products)
                   .Where(s => s.Store.Id == storeId)
                .ToListAsync();
        }

        public async Task<Discount> GetByIdAsync(Guid id)
        {
            return await _dbContext.Discounts           
                .Include(d => d.Store)
                .Include(d => d.Products)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> UpdateAsync(Discount discount)
        {
            if (discount!=null)
            {
                _dbContext.Discounts.Update(discount);
                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
         return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
             var discount = await GetByIdAsync(id);
            _dbContext.Discounts.Remove(discount);

             return await _dbContext.SaveChangesAsync() >= 0 ? true : false;    
        }

        public async Task<Discount> GetByNameAsync(string name)
        {
            return await _dbContext.Discounts
                  .AsNoTracking()
                .Include(d => d.Store)
                .Include(d => d.Products)
                .FirstOrDefaultAsync(d => d.Name == name);
        }

        public async Task<Discount> IsUniqueName(Guid id, string name)
        {
            return await _dbContext.Discounts
               .Include(d => d.Store)
               .Include(d => d.Products)
               .FirstOrDefaultAsync(d => d.Name == name && d.Id != id);
        }
        
    }
}
