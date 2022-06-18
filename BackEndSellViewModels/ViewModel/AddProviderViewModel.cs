using BackEndAnySellDataAccess.Entities;

namespace BackEndSellViewModels.ViewModel
{
    public class AddProviderViewModel: BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Other { get; set; }
    }
}
