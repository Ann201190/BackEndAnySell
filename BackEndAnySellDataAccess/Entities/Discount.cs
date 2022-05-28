using BackEndAnySellDataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEndAnySellDataAccess.Entities
{
    public class Discount : BaseEntity  
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public Guid StoreId { get; set; }
        public Store Store { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        [Required]
        public DiscountType DiscountType { get; set; } = DiscountType.Percent;
    }
}
