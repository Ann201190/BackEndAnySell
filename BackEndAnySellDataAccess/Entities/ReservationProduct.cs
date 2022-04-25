using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAnySellDataAccess.Entities
{
    public class ReservationProduct : BaseEntity  
    {
    //    [Required]
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
        public double Price { get; set; }
        public double Count { get; set; }
 //       [Required]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
       
    }
}
