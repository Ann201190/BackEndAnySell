using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndSellViewModels.ViewModel
{
   public class AddEmployeeViewModel: BaseEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
