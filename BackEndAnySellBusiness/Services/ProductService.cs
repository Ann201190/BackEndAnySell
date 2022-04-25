using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> AddAsync(Product product)
        {          
            if (product != null && product.Barcode!=null)
            {            
                return await _productRepository.AddAsync(product); // передаем уже готовый объект для сохранения в базу данных
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id != Guid.Empty)
            {
                return await _productRepository.DeleteAsync(id);
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await _productRepository.GetAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _productRepository.GetAsync(id);
        }

        public async Task<IEnumerable<Product>> GetByStoreIdAsync(Guid storeId)
        {
            return await _productRepository.GetByStoreIdAsync(storeId);
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            if (product != null)
            {  
                return await _productRepository.UpdateAsync(product); 
            }
            return false;
        }
    }
}
