using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Enums;
using System;

namespace BackEndSellViewModels.ViewModel
{
    public class AddDiscountViewModel: BaseEntity
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public Guid StoreId { get; set; }
        public DiscountType DiscountType { get; set; } = DiscountType.Percent;
    }
}
