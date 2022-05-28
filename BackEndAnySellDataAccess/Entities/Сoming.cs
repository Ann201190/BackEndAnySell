using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEndAnySellDataAccess.Entities
{
    public class Coming : BaseEntity
    {
        public string Number { get; set; } 
        [Required]
        public Guid StoreId { get; set; }
        public Store Store { get; set; }
        [Required]
        public Guid ProviderId { get; set; }
        public Provider Provider { get; set; }
        public ICollection<BalanceProduct> BalanceProducts { get; set; } = new List<BalanceProduct>();
        public DateTime Date { get; set; }
    }
}
