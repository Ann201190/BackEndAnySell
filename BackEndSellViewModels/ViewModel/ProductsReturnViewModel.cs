using System;
using System.Collections.Generic;

namespace BackEndSellViewModels.ViewModel
{
    public class ProductsReturnViewModel
    {
        public string OrderNumber { get; set; }
        public List<Guid> ReservationProductIds { get; set; }
    }
}
