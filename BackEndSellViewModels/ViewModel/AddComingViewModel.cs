using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndSellViewModels.ViewModel
{
    public class AddComingViewModel : BaseEntity
    {
        public string Number { get; set; }
        public Guid StoreId { get; set; }
        public Guid ProviderId { get; set; }
        public ICollection<BalanceProductViewModel> BalanceProducts { get; set; } = new List<BalanceProductViewModel>();
    }
}
