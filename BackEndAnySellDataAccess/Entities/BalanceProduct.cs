using System;
using System.ComponentModel.DataAnnotations;

namespace BackEndAnySellDataAccess.Entities
{
    public class BalanceProduct : BaseEntity
    {
        [Range(0, double.MaxValue)]
        public decimal ComingPrice { get; set; } = 0;
        public Guid ComingId { get; set; }
        public Coming Coming { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public double Count { get; set; } = 0;  
        public double BalanceCount { get; set; } = 0;
    }
}
