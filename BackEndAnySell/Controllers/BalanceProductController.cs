using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Enums;
using BackEndSellViewModels.ViewModel;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAnySell.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class BalanceProductController : Controller
    {
        public readonly IBalanceProductService _balanceProductService;
        public readonly IStoreService _storetService;
        public readonly IProductService _productService;

        public BalanceProductController(IBalanceProductService balanceProductService, IStoreService storetService, IProductService productService)
        {
            _balanceProductService = balanceProductService;
            _storetService = storetService;
            _productService = productService;
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


        [HttpGet("download/{storeId:guid}/{leng}")]                                                               //использую
        public async Task<IActionResult> DownloadFilePriceList(Guid storeId, string leng)
        {
            var products = await _balanceProductService.GetByStoreIdDownloadPriceListAsync(storeId);
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
                    worksheet.Cell(startRowTable, 2).Value = product.Product.Name;
                    worksheet.Cell(startRowTable, 3).Value = product.Product.Barcode;
                    if (Language.en.ToString() == leng)
                    {
                        worksheet.Cell(startRowTable, 4).Value = product.Product.ProductUnit;
                    }
                    else
                    {
                        worksheet.Cell(startRowTable, 4).Value = ConvertUnit(product.Product.ProductUnit);
                    }

                    worksheet.Cell(startRowTable, 5).Value = _productService.GetPriceWithDiscount(product.Product);

                    worksheet.Cell(startRowTable, 6).Value = product.BalanceCount;                 
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
