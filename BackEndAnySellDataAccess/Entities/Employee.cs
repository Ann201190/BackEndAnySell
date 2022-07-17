using BackEndAnySellDataAccess.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEndAnySellDataAccess.Entities
{
    public class Employee: BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public Role Role { get; set; }
        public ICollection<Store> Stores { get; set; } = new List<Store>();
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public byte[] Photo { get; set; }
        public string Other { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Provider> Providers { get; set; } = new List<Provider>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public string PrinterName { get; set; }
        public int PrinterDpi { get; set; } = 200;
    }
}
