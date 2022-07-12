using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEndAnySellDataAccess.Entities
{
    public class Store : BaseEntity   
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Coming> Comings { get; set; } = new List<Coming>();
        public byte[] LogoImage { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Address { get; set; }
    }
}
