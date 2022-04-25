using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEndAnySellDataAccess.Entities
{
    public class Store : BaseEntity   
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Discount> Discounts { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Order> Orders { get; set; }
        public byte[] LogoImage { get; set; }
    }
}
