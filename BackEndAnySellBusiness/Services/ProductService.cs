using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Enums;
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

        public async Task<IEnumerable<GetProductWithDiscountViewModal>> GetByStoreIdAsync(Guid storeId)
        {
            var products = await _productRepository.GetByStoreIdAsync(storeId);
            var listProductsWithDisconts = new List<GetProductWithDiscountViewModal>();
            foreach (var product in products)
            {
                listProductsWithDisconts.Add(
                 new GetProductWithDiscountViewModal
                 {
                     Id = product.Id,
                     BalanceProducts = product.BalanceProducts,
                     Barcode = product.Barcode,
                     Discount = product.Discount,
                     DiscountId = product.DiscountId,
                     Image = product.Image,
                     Name = product.Name,
                     Price = product.Price,
                     PriceWithDiscount = GetPriceWithDiscount(product),
                     ProductUnit = product.ProductUnit,
                     ReservationProducts = product.ReservationProducts,
                     Store = product.Store,
                     StoreId = product.StoreId
                 });
            }
            return listProductsWithDisconts;
        }

        public async Task<Guid> UpdateAsync(UpdateProductWithoutImgeViewModel productModel)
        {
            var product = await _productRepository.GetByIdAsync(productModel.Id);

            product.Name = productModel.Name;
            product.Price = productModel.Price;
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

        public async Task<bool> DeleteImageAsync(Guid id)
        {
            return await _productRepository.DeleteImageAsync(id);
        }

        private decimal GetPriceWithDiscount(Product product)
        {
            if (product.Discount == null)
            {
                return product.Price;
            }

            var priceWithDiscount = 0m;
            if (product.Discount.DiscountType == DiscountType.Fixed)
            {
                priceWithDiscount = product.Price - (decimal)product.Discount.Value;
                if (priceWithDiscount < 0)
                {
                    priceWithDiscount = 0;
                }
            }
            else
            {
                priceWithDiscount = product.Price - (decimal)((double)product.Price * (product.Discount.Value / 100));
            }
            return priceWithDiscount;
        }

        public async Task<IEnumerable<GetProductWithDiscountViewModal>> DiscountProductsAsync(Guid discountId)
        {
            var products = await _productRepository.GetByDiscountIdAsync(discountId);
            var productsWithDiscount = new List<GetProductWithDiscountViewModal>();
 
            foreach (var product in products)
            {             
                productsWithDiscount.Add(new GetProductWithDiscountViewModal()
                {
                     Id = product.Id,
                     Barcode = product.Barcode,
                     Name = product.Name,
                     Image = product.Image,
                     ProductUnit = product.ProductUnit,
                     Price = product.Price,
                     Discount = product.Discount,
                     DiscountId = product.DiscountId,
                     BalanceProducts = product.BalanceProducts,
                     ReservationProducts = product.ReservationProducts,
                     Store = product.Store,
                     StoreId = product.StoreId,                     
                     PriceWithDiscount = GetPriceWithDiscount(product)
                });
            }

            return productsWithDiscount; 
        }

        public async Task<IEnumerable<GetProductWithDiscountViewModal>> ProductsWithoutDiscountAsync(Guid discountId)
        {
            var products = await _productRepository.ProductsWithoutDiscountAsync(discountId);
            var productsWithDiscount = new List<GetProductWithDiscountViewModal>();

            foreach (var product in products)
            {
                productsWithDiscount.Add(new GetProductWithDiscountViewModal()
                {
                    Id = product.Id,
                    Barcode = product.Barcode,
                    Name = product.Name,
                    Image = product.Image,
                    ProductUnit = product.ProductUnit,
                    Price = product.Price,
                    Discount = product.Discount,
                    DiscountId = product.DiscountId,
                    BalanceProducts = product.BalanceProducts,
                    ReservationProducts = product.ReservationProducts,
                    Store = product.Store,
                    StoreId = product.StoreId,
                    PriceWithDiscount = GetPriceWithDiscount(product)
                });
            }

            return productsWithDiscount;             
        }
    }
}
