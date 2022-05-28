using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEndAnySellDataAccess.Entities
{
    public class Provider : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string Other { get; set; }
        public ICollection<Coming> Comings { get; set; } = new List<Coming>();
    }
}
