using BackEndAnySellDataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEndAnySellDataAccess.Entities
{
    public class Product : BaseEntity 
    {
        [Required]
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public Guid StoreId { get; set; } 
        public Store Store { get; set; }
        [Required]
        public string Barcode { get; set; }
        public double Count { get; set; } = 0;
        public ProductUnit ProductUnit { get; set; } = ProductUnit.Piece;
        public byte[] Image { get; set; }
        public Guid? DiscountId { get; set; }
        public Discount Discount { get; set; } 
        public ICollection<ReservationProduct> ReservationProducts { get; set; }
        public ProductUnit Unit { get; internal set; }
    }
}
