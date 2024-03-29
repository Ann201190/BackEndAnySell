﻿using BackEndAnySellAccessDataAccess.Context;
using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAnySellAccessDataAccess.Repositories
{
    public class ComingRepository : IComingRepository
    {

        private readonly CustomDbContext _dbContext;

        public ComingRepository(CustomDbContext dbContext)
        {
            _dbContext = dbContext;
            ModelBuilderExtensions.Initialize(dbContext);
            var builder = new DbContextOptionsBuilder<CustomDbContext>();
        }

        public async Task<bool> AddAsync(Coming coming)
        {
            if (coming != null)
            {
                await _dbContext.Comings.AddAsync(coming);

                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }

        public async Task<IEnumerable<Coming>> GetByStoreIdAsync(Guid storeId)
        {
            return await _dbContext.Comings
                .AsNoTracking()
                   .Include(c => c.Provider)            
                   .Include(c => c.BalanceProducts)
                      .ThenInclude(b=>b.Product)
                   .Where(s => s.Store.Id == storeId)
                   .OrderByDescending(c => c.Date)
                .ToListAsync();
        }

        public async Task<Coming> GetByIdAsync(Guid id)
        {
            return await _dbContext.Comings
                  .Include(c => c.BalanceProducts)
                //  .Include(d => d.Products)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var coming = await GetByIdAsync(id);
            _dbContext.Comings.Remove(coming);

            return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
        }

        public async Task<IEnumerable<Coming>> GetByProductIdAsync(Guid productId)
        {
            return await _dbContext.Comings
                .Include(c => c.BalanceProducts)
                    .ThenInclude(bp => bp.Product)
                .Where(c => c.BalanceProducts.Any(bp => bp.Product.Id == productId && bp.BalanceCount > 0))
                .OrderBy(c => c.Date)
                .ToListAsync();
        }
    }
}
