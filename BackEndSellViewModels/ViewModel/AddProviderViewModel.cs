using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndSellViewModels.ViewModel
{
   public class AddProviderViewModel: BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid EmployeeId { get; set; }
        public string Other { get; set; }
    }
}
