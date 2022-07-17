using System;

namespace BackEndAnySellDataAccess.Entities
{
    public class ReservationProduct : BaseEntity  
    {
    //    [Required]
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal PriceComing { get; set; }
        public double Count { get; set; }
 //       [Required]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
       
    }
}
