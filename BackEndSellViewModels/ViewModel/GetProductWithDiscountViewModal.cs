using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndSellViewModels.ViewModel
{
   public class GetProductWithDiscountViewModal:Product
    {
        public decimal PriceWithDiscount { get; set; }
    }
}
