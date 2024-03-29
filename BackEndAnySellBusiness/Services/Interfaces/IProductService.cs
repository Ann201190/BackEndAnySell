﻿using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductWithDiscountViewModal>> GetByStoreIdAsync(Guid storeId);
        Task<Guid> AddWithoutImgeAsync(AddProductWithoutImgeViewModel productModel);
        Task<Guid> UpdateAsync(UpdateProductWithoutImgeViewModel productModel);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AddImageAsync(IFormFile file, Guid id);
        Task<Product> GetByIdAsync(Guid id);
        Task<bool> DeleteImageAsync(Guid id);
        Task<IEnumerable<GetAwailableProductViewModel>> GetByStoreIdAwailableListAsync(Guid storeId);
        Task<IEnumerable<GetProductWithDiscountViewModal>> DiscountProductsAsync(Guid discountId);
        Task<IEnumerable<GetProductWithDiscountViewModal>> ProductsWithoutDiscountAsync(Guid discountId);
        decimal GetPriceWithDiscount(Product product);
        decimal GetDiscount(Product product);
        Task<IEnumerable<Product>> GetByStoreIdDownloadNeedListAsync(Guid storeId);
        Task<IEnumerable<Product>> GetByStoreIdDownloadAllListAsync(Guid storeId);
        Task<IEnumerable<Product>> GetByStoreIdDownloadPriceListAsync(Guid storeId);
    }
}
