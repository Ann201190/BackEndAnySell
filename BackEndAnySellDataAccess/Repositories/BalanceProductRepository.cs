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
    public class BalanceProductRepository : IBalanceProductRepository
    {
        private readonly CustomDbContext _dbContext;
        public BalanceProductRepository(CustomDbContext dbContext)
        {
            _dbContext = dbContext;
            ModelBuilderExtensions.Initialize(dbContext);
            var builder = new DbContextOptionsBuilder<CustomDbContext>();
        }
        public async Task<IEnumerable<BalanceProduct>> GetByStoreIdAsync(Guid storeId)
        {
            return await _dbContext.BalanceProducts
                .AsNoTracking()
                    .Include(b => b.Product)
                         .ThenInclude(p => p.Store)
                    .Include(b => b.Product)
                         .ThenInclude(p => p.Discount)
                   .Where(b => b.Product.StoreId == storeId && b.BalanceCount > 0)
                   .ToListAsync();
        }

        public async Task<bool> AddCountAsync(Guid balanceProductId, double addingCount)
        {
            var balanceProducts = _dbContext.BalanceProducts.FirstOrDefault(bp => bp.Id == balanceProductId);
            balanceProducts.BalanceCount += addingCount;

            _dbContext.BalanceProducts.Update(balanceProducts);
            return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
        }


        public async Task<bool> UpdateAsync(Guid balanceProductId, double count)
        {
            var balanceProducts = _dbContext.BalanceProducts.FirstOrDefault(bp => bp.Id == balanceProductId);
            balanceProducts.BalanceCount = count;

                _dbContext.BalanceProducts.Update(balanceProducts);
                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;          
        }

        public async Task<double> CountAsync(Guid productId)
        {
            return await _dbContext.BalanceProducts
                   .AsNoTracking()                      
                      .Where(br => br.Product.Id == productId)
                   .SumAsync(br => (double)br.BalanceCount);
        }
    }
}
