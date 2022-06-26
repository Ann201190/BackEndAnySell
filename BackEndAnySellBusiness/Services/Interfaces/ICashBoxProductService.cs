using BackEndSellViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services.Interfaces
{
    public interface ICashBoxProductService
    {
        Task<IEnumerable<GetCashBoxProductViewModel>> GetByStoreIdAsync(Guid storeId);
    }
}
