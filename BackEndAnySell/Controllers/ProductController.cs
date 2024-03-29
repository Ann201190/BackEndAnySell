﻿using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Enums;
using BackEndSellViewModels.ViewModel;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace BackEndAnySell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class ProductController : Controller
    {
        public readonly IProductService _productService;
        public readonly IStoreService _storetService;
        //  private Guid _userId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public ProductController(IProductService productService, IStoreService storetService)
        {
            _productService = productService;
            _storetService = storetService;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)                                                                      //использую
        {
            return Ok(await _productService.GetByIdAsync(id));
        }


        //http://localhost:80/api/product/getstoreproduct/BFBC7481-FB3C-4192-A093-519F40F1B812          для ииса
        [Authorize]
        [HttpGet("getstoreproduct/{storeId:guid}")]                                                                           //использую
        public async Task<IActionResult> GetByStoreIdAsync(Guid storeId)
        {
            return Ok(await _productService.GetByStoreIdAsync(storeId));
        }

        [Authorize]
        [HttpGet("deleteimage/{id:guid}")]                                                                                            //использую
        public async Task<IActionResult> DeleteImageAsync(Guid id)
        {
            var result = await _productService.DeleteImageAsync(id);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("addproductimage/{id:guid}")]
        public async Task<IActionResult> AddProductImageAsync(Guid id)                                                         //использую
        {
            try
            {
                var file = Request.Form.Files[0];

                if (ModelState.IsValid)
                {
                    if (await _productService.AddImageAsync(file, id))
                    {
                        return Ok(true);
                    }
                }
                return Ok(false);
            }
            catch
            {
                return Ok(false);
            }
        }

        [Authorize]
        [HttpPost("addproductwithoutimage")]
        public async Task<IActionResult> AddProductWithoutImageAsync(AddProductWithoutImgeViewModel productModel)             //использую
        {
            return Ok(await _productService.AddWithoutImgeAsync(productModel));
        }

        [Authorize]
        [HttpPost("updateproductwithoutimge")]                                                                             //использую
        public async Task<IActionResult> UpdateAsync(UpdateProductWithoutImgeViewModel productModel)
        {
            return Ok(await _productService.UpdateAsync(productModel));
        }

        [Authorize]
        [HttpGet("deleteproduct/{id:guid}")]                                                                                  //использую
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (await _productService.DeleteAsync(id))
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [Authorize]
        [HttpGet("discountproducts/{discountId:guid}")]                                                                      //использую
                                                                                                                             //     [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DiscountProductsAsync(Guid discountId)
        {
            return Ok(await _productService.DiscountProductsAsync(discountId));
        }

        [Authorize]
        [HttpGet("productswithoutdiscount/{discountId:guid}")]                                                                      //использую                                                                                                                     
        public async Task<IActionResult> ProductsWithoutDiscountAsync(Guid discountId)
        {
            return Ok(await _productService.ProductsWithoutDiscountAsync(discountId));
        }

        [Authorize]
        [HttpGet("needlist/{storeId:guid}")]                                                                     //использую 
        public async Task<IActionResult> NeedList(Guid storeId)
        {
           return Ok(await _productService.GetByStoreIdDownloadNeedListAsync(storeId));
        }

        [Authorize]
        [HttpGet("awailablelist/{storeId:guid}")]
        public async Task<IActionResult> AwailableList(Guid storeId)                                               //использую 
        {
            return Ok(await _productService.GetByStoreIdAwailableListAsync(storeId));
        }

        [HttpGet("download/{storeId:guid}/{leng}")]                                                                  //использую 
        public async Task<IActionResult> DownloadFileNeedList(Guid storeId, string leng)
        {
            var products = await _productService.GetByStoreIdDownloadNeedListAsync(storeId);
            var nameStore = await _storetService.GetByIdAsync(storeId);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");

                var finishColumTable = 5;
                var startRowTable = 3;
                string fileName = "";

                //объединение первой строки и 2
                worksheet.Range("A1:E16").Row(1).Merge();
                worksheet.Range("A2:E17").Row(1).Merge();
                //шрифт и размер для первой заголовочной строки
                worksheet.Cell(1, 1).Style.Font.FontSize = 14;
                worksheet.Cell(1, 1).Style.Font.FontName = "Arial";
           
                //выравнивание по центру заголовка
                worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


                for (int i = 1; i <= finishColumTable; i++)
                {
                    //цвет заголовка
                    worksheet.Cell(startRowTable, i).Style.Fill.BackgroundColor = XLColor.FromArgb(5,23,43);
                    worksheet.Cell(startRowTable, i).Style.Font.FontColor = XLColor.White;
                }

                for (int i = startRowTable; i <= products.Count() + startRowTable; i++)
                {
                    for (int j = 1; j <= finishColumTable; j++)
                    {
                        //границы таблицы
                        worksheet.Cell(i, j).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i, j).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i, j).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i, j).Style.NumberFormat.Format = "@";
                    }
                }

                //ширина колонок
                worksheet.Column(1).Width = 5;
                worksheet.Column(2).Width = 70;
                worksheet.Column(3).Width = 25;
                worksheet.Column(4).Width = 15;
                worksheet.Column(5).Width = 10;

     

                worksheet.Cell(startRowTable, 1).Value = "№";
                if (Language.en.ToString() == leng)
                {
                    worksheet.Cell(startRowTable, 2).Value = "Name product";
                    worksheet.Cell(startRowTable, 3).Value = "Barcode";
                    worksheet.Cell(startRowTable, 4).Value = "Unit";
                    worksheet.Cell(startRowTable, 5).Value = "Count";
                    //заголовой в объединенной строке
                    worksheet.Cell(1, 1).Value = $"Zero balances store {nameStore.Name} as of {DateTime.Now.ToShortDateString()}";
                    fileName = $"Zero_balances_{nameStore.Name}_{DateTime.Now.ToShortDateString()}.xlsx";
                }
                else
                {
                    worksheet.Cell(startRowTable, 2).Value = "Наименование продукта";
                    worksheet.Cell(startRowTable, 3).Value = "Штрих-код";
                    worksheet.Cell(startRowTable, 4).Value = "Единица";
                    worksheet.Cell(startRowTable, 5).Value = "Количество";
                    //заголовой в объединенной строке
                    worksheet.Cell(1, 1).Value = $"Нулевые остатки товаров магазина {nameStore.Name} по состоянию на {DateTime.Now.ToShortDateString()}";
                    fileName = $"Нулевые_остатки_товаров_{nameStore.Name}_{DateTime.Now.ToShortDateString()}.xlsx";
                }

                int n = 0; //порядковый номер продукта
                foreach (var product in products)
                {
                    startRowTable++;
                    n++;
                    worksheet.Cell(startRowTable, 1).Value = n;
                    worksheet.Cell(startRowTable, 2).Value = product.Name;
                    worksheet.Cell(startRowTable, 3).Value = product.Barcode;



                    if (Language.en.ToString() == leng)
                    {
                        worksheet.Cell(startRowTable, 4).Value = product.ProductUnit;
                    }
                    else
                    {
                        worksheet.Cell(startRowTable, 4).Value = ConvertUnit(product.ProductUnit);
                    }
                    worksheet.Cell(startRowTable, 5).Value = 0;  //            
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
                }
            }
        }

        private string ConvertUnit(ProductUnit productUnit)                                                       //использую
        {
            switch (productUnit)
            {
                case ProductUnit.Piece:
                    return "Штука";
                case ProductUnit.Kilogram:
                    return "Килограмм";
                case ProductUnit.Liter:
                    return "Литр";
                default:
                    return "";
            }
        }




        [HttpGet("downloadprice/{storeId:guid}/{leng}")]                                                               //использую
        public async Task<IActionResult> DownloadFilePriceList(Guid storeId, string leng)
        {
            var products = await _productService.GetByStoreIdDownloadPriceListAsync(storeId);
            var nameStore = await _storetService.GetByIdAsync(storeId);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");

                var finishColumTable = 6;
                var startRowTable = 3;
                string fileName = "";

                //объединение первой строки и 2
                worksheet.Range("A1:F16").Row(1).Merge();
                worksheet.Range("A2:F17").Row(1).Merge();
                //шрифт и размер для первой заголовочной строки
                worksheet.Cell(1, 1).Style.Font.FontSize = 14;
                worksheet.Cell(1, 1).Style.Font.FontName = "Arial";
                //выравнивание по центру заголовка
                worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


                for (int i = 1; i <= finishColumTable; i++)
                {
                    //цвет заголовка
                    worksheet.Cell(startRowTable, i).Style.Fill.BackgroundColor = XLColor.FromArgb(254, 161, 22);
                }

                for (int i = startRowTable; i <= products.Count() + startRowTable; i++)
                {
                    for (int j = 1; j <= finishColumTable; j++)
                    {
                        //границы таблицы
                        worksheet.Cell(i, j).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i, j).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i, j).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i, j).Style.NumberFormat.Format = "@";
                    }
                }

                //ширина колонок
                worksheet.Column(1).Width = 5;
                worksheet.Column(2).Width = 70;
                worksheet.Column(3).Width = 25;
                worksheet.Column(4).Width = 15;
                worksheet.Column(5).Width = 10;
                worksheet.Column(6).Width = 10;


                worksheet.Cell(startRowTable, 1).Value = "№";
                if (Language.en.ToString() == leng)
                {
                    worksheet.Cell(startRowTable, 2).Value = "Name product";
                    worksheet.Cell(startRowTable, 3).Value = "Barcode";
                    worksheet.Cell(startRowTable, 4).Value = "Unit";
                    worksheet.Cell(startRowTable, 5).Value = "Price";
                    worksheet.Cell(startRowTable, 6).Value = "Count";
                    //заголовой в объединенной строке
                    worksheet.Cell(1, 1).Value = $" Price-list store {nameStore.Name} on {DateTime.Now.ToShortDateString()}";
                    fileName = $"Price-list_{nameStore.Name}_{DateTime.Now.ToShortDateString()}.xlsx";
                }
                else
                {
                    worksheet.Cell(startRowTable, 2).Value = "Наименование продукта";
                    worksheet.Cell(startRowTable, 3).Value = "Штрих-код";
                    worksheet.Cell(startRowTable, 4).Value = "Единица";
                    worksheet.Cell(startRowTable, 5).Value = "Цена";
                    worksheet.Cell(startRowTable, 6).Value = "Количество";
                    //заголовой в объединенной строке
                    worksheet.Cell(1, 1).Value = $"Прайс-лист магазина {nameStore.Name} по состоянию на {DateTime.Now.ToShortDateString()}";
                    fileName = $"Прайс-лист_{nameStore.Name}_{DateTime.Now.ToShortDateString()}.xlsx";
                }

                int n = 0; //порядковый номер продукта

                foreach (var product in products)
                {
                    startRowTable++;
                    n++;
                    worksheet.Cell(startRowTable, 1).Value = n;
                    worksheet.Cell(startRowTable, 2).Value = product.Name;
                    worksheet.Cell(startRowTable, 3).Value = product.Barcode;
                    if (Language.en.ToString() == leng)
                    {
                        worksheet.Cell(startRowTable, 4).Value = product.ProductUnit;
                    }
                    else
                    {
                        worksheet.Cell(startRowTable, 4).Value = ConvertUnit(product.ProductUnit);
                    }

                    worksheet.Cell(startRowTable, 5).Value = _productService.GetPriceWithDiscount(product);

                    worksheet.Cell(startRowTable, 6).Value = product.BalanceProducts.Sum(bp => bp.BalanceCount);
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
                }
            }
        }


        [HttpGet("downloadall/{storeId:guid}/{leng}")]                                                                  //использую 
        public async Task<IActionResult> DownloadFileAllList(Guid storeId, string leng)
        {
            var products = await _productService.GetByStoreIdDownloadAllListAsync(storeId);
            var nameStore = await _storetService.GetByIdAsync(storeId);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");

                var finishColumTable = 5;
                var startRowTable = 3;
                string fileName = "";

                //объединение первой строки и 2
                worksheet.Range("A1:E16").Row(1).Merge();
                worksheet.Range("A2:E17").Row(1).Merge();
                //шрифт и размер для первой заголовочной строки
                worksheet.Cell(1, 1).Style.Font.FontSize = 14;
                worksheet.Cell(1, 1).Style.Font.FontName = "Arial";

                //выравнивание по центру заголовка
                worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


                for (int i = 1; i <= finishColumTable; i++)
                {
                    //цвет заголовка
                    worksheet.Cell(startRowTable, i).Style.Fill.BackgroundColor = XLColor.FromArgb(254, 161, 22);
                }

                for (int i = startRowTable; i <= products.Count() + startRowTable; i++)
                {
                    for (int j = 1; j <= finishColumTable; j++)
                    {
                        //границы таблицы
                        worksheet.Cell(i, j).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i, j).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i, j).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i, j).Style.NumberFormat.Format = "@";
                    }
                }

                //ширина колонок
                worksheet.Column(1).Width = 5;
                worksheet.Column(2).Width = 70;
                worksheet.Column(3).Width = 25;
                worksheet.Column(4).Width = 15;
                worksheet.Column(5).Width = 10;



                worksheet.Cell(startRowTable, 1).Value = "№";
                if (Language.en.ToString() == leng)
                {
                    worksheet.Cell(startRowTable, 2).Value = "Name product";
                    worksheet.Cell(startRowTable, 3).Value = "Barcode";
                    worksheet.Cell(startRowTable, 4).Value = "Unit";
                    worksheet.Cell(startRowTable, 5).Value = "Count";
                    //заголовой в объединенной строке
                    worksheet.Cell(1, 1).Value = $"All balances store {nameStore.Name} as of {DateTime.Now.ToShortDateString()}";
                    fileName = $"All_balances_{nameStore.Name}_{DateTime.Now.ToShortDateString()}.xlsx";
                }
                else
                {
                    worksheet.Cell(startRowTable, 2).Value = "Наименование продукта";
                    worksheet.Cell(startRowTable, 3).Value = "Штрих-код";
                    worksheet.Cell(startRowTable, 4).Value = "Единица";
                    worksheet.Cell(startRowTable, 5).Value = "Количество";
                    //заголовой в объединенной строке
                    worksheet.Cell(1, 1).Value = $"Все остатки товаров магазина {nameStore.Name} по состоянию на {DateTime.Now.ToShortDateString()}";
                    fileName = $"Все_остатки_товаров_{nameStore.Name}_{DateTime.Now.ToShortDateString()}.xlsx";
                }

                int n = 0; //порядковый номер продукта
                foreach (var product in products)
                {
                    startRowTable++;
                    n++;
                    worksheet.Cell(startRowTable, 1).Value = n;
                    worksheet.Cell(startRowTable, 2).Value = product.Name;
                    worksheet.Cell(startRowTable, 3).Value = product.Barcode; 



                    if (Language.en.ToString() == leng)
                    {
                        worksheet.Cell(startRowTable, 4).Value = product.ProductUnit;
                    }
                    else
                    {
                        worksheet.Cell(startRowTable, 4).Value = ConvertUnit(product.ProductUnit);
                    }
                    var count = 0;
                    worksheet.Cell(startRowTable, 5).Value = product.BalanceProducts != null ? product.BalanceProducts.Sum(bp => bp.BalanceCount):0;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
                }
            }
        }
    }
}
