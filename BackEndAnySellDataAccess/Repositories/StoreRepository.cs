﻿using BackEndAnySellAccessDataAccess.Context;
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
    public class StoreRepository: IStoreRepository
    {
        private readonly CustomDbContext _dbContext;

        public StoreRepository(CustomDbContext dbContext)
        {
            _dbContext = dbContext;
            ModelBuilderExtensions.Initialize(dbContext);
            var builder = new DbContextOptionsBuilder<CustomDbContext>();
        }

        public async Task<IEnumerable<Store>> GetAsync(string userName)
        {
            return await _dbContext.Stores
                .AsNoTracking()
                .Include(s => s.Discounts)
                .Include(s => s.Products)
                .Include(s => s.Employees)
                       .Where(s => s.Employees.Any(e => e.Email == userName))    //по почте, по конкретному юзеру только для Менеджера
                .ToListAsync();
        }
        public async Task<Store> GetByIdAsync(Guid id)
        {
            return await _dbContext.Stores
                .AsNoTracking()
                .Include(s => s.Discounts)
                .Include(s => s.Products)
                .Include(s => s.Employees)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
