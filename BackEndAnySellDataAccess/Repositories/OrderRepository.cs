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
               // .Include(o => o.Store)
                .Include(o => o.Employee)
                .Include(o => o.ReservationProducts)
                   .ThenInclude(r => r.Product)
                     .Where(o=>o.StoreId == storeId)
                .OrderByDescending(c => c.OrderDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetTopThreeProductAsync(Guid storeId)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                // .Include(o => o.Store)
                .Include(o => o.Employee)
                .Include(o => o.ReservationProducts)
                   .ThenInclude(r => r.Product)
                     .Where(o => o.StoreId == storeId)
                .OrderByDescending(c => c.ReservationProducts.Select(p=>p.Id))
                .ToListAsync();
        }


        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _dbContext.Orders
              .AsNoTracking()
            //    .Include(o => o.ReservationProducts)
              .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> AddAsync(Order order)
        {
            if (order != null)
            {
                await _dbContext.Orders.AddAsync(order);
                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }

        public async Task<Order> GetCheckAsync(string orderNumber)
        {  
            return await _dbContext.Orders
              .AsNoTracking()
              .Include(o => o.ReservationProducts)
                  .ThenInclude(r => r.Product)
                 .Include(o => o.Store)
                 .Include(e=>e.Employee)
             .FirstOrDefaultAsync(s => s.OrderNumber == orderNumber);
        }

        public async Task<Order> GetStoreCheckAsync(Guid storeId, string orderNumber)
        {
            return await _dbContext.Orders
              .AsNoTracking()
              .Include(o => o.ReservationProducts)
                  .ThenInclude(r => r.Product)
              .Include(o => o.Store)
              .Include(e => e.Employee)
              .FirstOrDefaultAsync(s => s.OrderNumber == orderNumber && s.StoreId == storeId);
        }

        public async Task<bool> CancelCheck(Guid orderId)
        {
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return false;
            }
            order.OrderStatus = BackEndAnySellDataAccess.Enums.OrderStatus.Сanceled;

            return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
