﻿using BackEndAnySellDataAccess.Enums;
using System;

namespace BackEndSellViewModels.ViewModel
{
    public class GetCashBoxProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal PriceWithDiscount { get; set; }
        public string Barcode { get; set; }
        public ProductUnit ProductUnit { get; set; } = ProductUnit.Piece;
    }
}
