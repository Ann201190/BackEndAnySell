using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndSellViewModels.ViewModel
{
    public class BalanceProductViewModel
    {
        public decimal ComingPrice { get; set; } = 0;
        public Guid ProductId { get; set; }  
        public double Count { get; set; } = 0;
    }
}
