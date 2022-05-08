using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndSellViewModels.ViewModel
{
   public class AddStoreViewModel: BaseEntity
    {
        public string NameStore { get; set; }
        public string NameEmployee { get; set; }
        public string SurNameEmployee { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string Phone { get; set; }
    }
}
