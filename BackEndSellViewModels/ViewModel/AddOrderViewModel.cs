using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;

namespace BackEndSellViewModels.ViewModel
{
    public class AddOrderViewModel: BaseEntity
    {
        public Guid StoreId { get; set; }
        public ICollection<AddCashboxProductViewModel> Product { get; set; } = new List<AddCashboxProductViewModel>();
    }
}
