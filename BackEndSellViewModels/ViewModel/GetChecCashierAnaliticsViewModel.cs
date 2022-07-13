using BackEndAnySellDataAccess.Entities;

namespace BackEndSellViewModels.ViewModel
{
    public class GetChecCashierAnaliticsViewModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public int CountOrders { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
