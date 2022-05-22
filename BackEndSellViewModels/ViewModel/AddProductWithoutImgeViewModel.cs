﻿using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Enums;
using System;

namespace BackEndSellViewModels.ViewModel
{
    public class AddProductWithoutImgeViewModel: BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid StoreId { get; set; }
        public string Barcode { get; set; }
        public double Count { get; set; } = 0;
        public ProductUnit ProductUnit { get; set; } = ProductUnit.Piece;
    }
}