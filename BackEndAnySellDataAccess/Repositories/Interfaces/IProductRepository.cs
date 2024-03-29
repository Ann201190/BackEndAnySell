﻿using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellAccessDataAccess.Repositories.Interfaces
{
    public  interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetByStoreIdAsync(Guid storeId);
        Task<IEnumerable<Product>> GetByStoreIdWithIncludesAsync(Guid storeId);
        Task<bool> AddProductAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AddImageAsync(byte[] fileArrayBytes, Guid id);
        Task<bool> DeleteImageAsync(Guid id);
        Task<IEnumerable<Product>> GetByDiscountIdAsync(Guid discountId);
        Task<IEnumerable<Product>> ProductsWithoutDiscountAsync(Guid discountId);
        Task<IEnumerable<Product>> GetByStoreIdDownloadNeedListAsync(Guid storeId);
        Task<IEnumerable<Product>> GetByStoreIdDownloadAllListAsync(Guid storeId);
        Task<IEnumerable<Product>> GetByStoreIdDownloadPriceListAsync(Guid storeId);

    }
}
