using BackEndAnySellDataAccess.Entities;

namespace BackEndSellViewModels.ViewModel
{
    public class UpdateEmployeeViewModel : BaseEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
    }
}

