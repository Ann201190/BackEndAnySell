using System;

namespace BackEndAnySellDataAccess.Entities
{
    public class ReservationProduct : BaseEntity  
    {
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal PriceComing { get; set; }
        public double Count { get; set; }
        public Guid BalanceProductId { get; set; }
        public BalanceProduct BalanceProduct { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
       
    }
}
