using BackEndAnySellDataAccess.Enums;
using System;
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
        public string Email { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public Guid StoreId { get; set; }
        public Store Store { get; set; }
    }
}
