using System;

namespace BackEndAnySellDataAccess.Entities
{
    public class BalanceProduct : BaseEntity
    {
        public Guid ComingId { get; set; }
        public Coming Coming { get; set; }
        public Product Product { get; set; }
        public double Count { get; set; } = 0;  
        public double Balance { get; set; } = 0;
    }
}
