using BackEndAnySellDataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAnySellDataAccess.Entities
{
    public class Order : BaseEntity 
    {
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public OrderStatus OrderStatus { get; set; }
        [Required]
        public string OrderNumber { get; set; }
        public ICollection<ReservationProduct> ReservationProducts { get; set; } = new List<ReservationProduct>();
        [Required]
        public Guid StoreId { get; set; }
        public Store Store { get; set; }
    }
}
