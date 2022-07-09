using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;

namespace BackEndAnySellBusiness.Services
{
    public class BalanceProductService : IBalanceProductService
    {
        private readonly IBalanceProductRepository _balanceProductRepository;

        public BalanceProductService(IBalanceProductRepository balanceProductRepository)
        {
            _balanceProductRepository = balanceProductRepository;       
        }       
    }
}


