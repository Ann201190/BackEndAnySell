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
    public class ProductRepository : IProductRepository
    {
        private readonly CustomDbContext _dbContext;

        public ProductRepository(CustomDbContext dbContext)
        {
            _dbContext = dbContext;
            ModelBuilderExtensions.Initialize(dbContext);
            var builder = new DbContextOptionsBuilder<CustomDbContext>();
        }

          public async Task<bool> AddAsync(Product product)
        {
            var isUniqueBarcode = !await _dbContext.Products
                .AnyAsync(p => 
                    p.Barcode == product.Barcode 
                    && p.StoreId == product.StoreId);

            if (product != null && isUniqueBarcode)
            {
                 await _dbContext.Products.AddAsync(product);

                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;    
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
          var isReservation =  await _dbContext.Orders.AnyAsync(o => o.ReservationProducts.Any(r => r.ProductId == id));
            if (!isReservation)
            {
                var product = await GetAsync(id);
                _dbContext.Products.Remove(product);

                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }

        public async Task <IEnumerable<Product>> GetAsync()
        {
            return await _dbContext.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByStoreIdAsync(Guid storeId)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .Include(p => p.Store)
                .Include(p=> p.Discount)
                .Where(s=>s.StoreId == storeId)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            if (product !=null)
            {
                 _dbContext.Products.Update(product);
                 return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }
    }
}