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

          public async Task<bool> AddProductAsync(Product product)
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

        public async Task<bool> AddImageAsync(byte[] fileArrayBytes, Guid id)
        {
            if (fileArrayBytes != null)
            {
                var product = await GetByIdAsync(id);

                product.Image = fileArrayBytes;
                _dbContext.Products.Update(product);

                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
          var isReservation =  await _dbContext.Orders.AnyAsync(o => o.ReservationProducts.Any(r => r.ProductId == id));
            if (!isReservation)
            {
                var product = await GetByIdAsync(id);
                _dbContext.Products.Remove(product);

                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _dbContext.Products
                  .Include(b=>b.BalanceProducts)
                  .Include(r => r.ReservationProducts)
                  .Include(p => p.Discount)
                  .Include(p =>p.Store)
                .FirstOrDefaultAsync(p => p.Id == id);                
        }

        public async Task<IEnumerable<Product>> GetByStoreIdAsync(Guid storeId)
        {
            return await _dbContext.Products
                .AsNoTracking()
                 .Include(p=> p.Discount)
                   .Where(s=>s.StoreId == storeId)
                  .OrderBy(p=>p.Name)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetByStoreIdWithIncludesAsync(Guid storeId)
        {
            return await _dbContext.Products
                .AsNoTracking()
                 .Include(p => p.Discount)
                 .Include(p=> p.Store)
                 .Where(s => s.StoreId == storeId)
                 .OrderBy(p => p.Name)
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

        public async Task<bool> DeleteImageAsync(Guid id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                product.Image = null;
                _dbContext.Products.Update(product);
                return await _dbContext.SaveChangesAsync() >= 0 ? true : false;
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetByDiscountIdAsync(Guid discountId)
        {
            return await _dbContext.Products
                .AsNoTracking()
                  .Include(p => p.Discount)
                    .Where(p => p.DiscountId == discountId)
                    .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> ProductsWithoutDiscountAsync(Guid discountId)
        {
            return await _dbContext.Products
                .AsNoTracking()
                  .Include(p => p.Discount)
                    .Where(p => p.DiscountId != discountId && p.Store.Discounts.Any(d => d.Id == discountId))
                    .OrderBy(p => p.Name)
                .ToListAsync();
        }


        public async Task<IEnumerable<Product>> GetByStoreIdDownloadNeedListAsync(Guid storeId)
        {        
            return  await _dbContext.Products
                .AsNoTracking()
                    .Include(p => p.BalanceProducts)
                   .Where(p => p.StoreId == storeId && !p.BalanceProducts.Any(b => b.BalanceCount > 0 ))
                   .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByStoreIdDownloadAllListAsync(Guid storeId)
        {
            return await _dbContext.Products
                .AsNoTracking()
                    .Include(p => p.Discount)
                    .Include(p => p.BalanceProducts)
                   .Where(p => p.StoreId == storeId)
                   .OrderBy(p => p.Name)
                .ToListAsync();                            
        }

        public async Task<IEnumerable<Product>> GetByStoreIdDownloadPriceListAsync(Guid storeId)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .AsNoTracking()
                    .Include(p => p.Discount)
                    .Include(p => p.BalanceProducts)
                   .Where(p => p.StoreId == storeId && p.BalanceProducts.Any(b => b.BalanceCount > 0))
                   .OrderBy(p => p.Name)
                .ToListAsync();
        }
    }
}
