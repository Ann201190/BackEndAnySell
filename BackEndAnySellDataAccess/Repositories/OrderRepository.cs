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
   public class OrderRepository: IOrderRepository
    {
        private readonly CustomDbContext _dbContext;

        public OrderRepository(CustomDbContext dbContext)
        {
            _dbContext = dbContext;
            ModelBuilderExtensions.Initialize(dbContext);
            var builder = new DbContextOptionsBuilder<CustomDbContext>();
        }

        public async Task<IEnumerable<Order>> GetByStoreIdAsync(Guid storeId)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.ReservationProducts)
                     .Where(o=>o.StoreId == storeId)
                .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _dbContext.Orders
              .AsNoTracking()
                .Include(o => o.ReservationProducts)
              .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
