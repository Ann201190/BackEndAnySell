using BackEndAnySellAccessDataAccess.Context;
using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAnySellDataAccess.Repositories
{
    public class ReservationProductRepository : IReservationProductRepository
    {
        private readonly CustomDbContext _dbContext;
        public ReservationProductRepository(CustomDbContext dbContext)
        {
            _dbContext = dbContext;
            ModelBuilderExtensions.Initialize(dbContext);
            var builder = new DbContextOptionsBuilder<CustomDbContext>();
        }

        public async Task<bool> AddAsync(List<ReservationProduct> reservationProducts)
        {
            if (reservationProducts != null)
            {
                await _dbContext.ReservationProducts.AddRangeAsync(reservationProducts);
                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }
    }
}
