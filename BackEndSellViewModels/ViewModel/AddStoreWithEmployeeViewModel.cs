using BackEndAnySellDataAccess.Entities;

namespace BackEndSellViewModels.ViewModel
{
    public class AddStoreWithEmployeeViewModel : BaseEntity
    {
        public string NameStore { get; set; } 
        public string NameEmployee { get; set; }
        public string SurNameEmployee { get; set; }
        public string Phone { get; set; }
    }
}
