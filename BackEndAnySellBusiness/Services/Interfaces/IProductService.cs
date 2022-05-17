using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAsync();
        Task<IEnumerable<Product>> GetByStoreIdAsync(Guid storeId);
        Task<Guid> AddWithoutImgeAsync(AddProductWithoutImgeViewModel productModel);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AddImageAsync(IFormFile file, Guid id);
    }
}
