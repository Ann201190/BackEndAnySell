using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<bool> AddImageAsync(IFormFile file, Guid id)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return await _productRepository.AddImageAsync(ms.ToArray(), id);
            }
        }

        public async Task<Guid> AddWithoutImgeAsync(AddProductWithoutImgeViewModel productModel)
        {           
            if (productModel != null && productModel.Barcode!=null)
            {
                var product = new Product()
                {
                    Name = productModel.Name,
                    Barcode = productModel.Barcode,
                    Count = productModel.Count,
                    Price = productModel.Price,
                    ProductUnit = productModel.ProductUnit,
                    StoreId = productModel.StoreId,
                };

                var isAddedProduct = await AddProductAsync(product);                   // передаем уже готовый объект для сохранения в базу данных

                if (isAddedProduct )
                {
                    return product.Id;
                }
            }
            return Guid.Empty;
        }

        private async Task<bool> AddProductAsync(Product product)
        {
            if (product != null)
            {
                return await _productRepository.AddProductAsync(product);
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

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetByStoreIdAsync(Guid storeId)
        {
            return await _productRepository.GetByStoreIdAsync(storeId);
        }

        public async Task<Guid> UpdateAsync(UpdateProductWithoutImgeViewModel productModel)
        {
            var product = await _productRepository.GetByIdAsync(productModel.Id);

            product.Name = productModel.Name;
            product.Price = productModel.Price;
            product.Count = productModel.Count;
            product.Barcode = productModel.Barcode;
            product.StoreId = productModel.StoreId;
            product.ProductUnit = productModel.ProductUnit;

            var isUpdatedProduct = await _productRepository.UpdateAsync(product);

            if (isUpdatedProduct)
            {
                return product.Id;
            }
            return Guid.Empty;
        }       
    }
}
