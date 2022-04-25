using System;
using System.ComponentModel.DataAnnotations;

namespace BackEndAnySellDataAccess.Entities
{
    public abstract  class BaseEntity
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
