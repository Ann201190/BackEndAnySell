using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services
{
    public  class OrderService: IOrderService
    {
        private readonly IOrderRepository _odrerRepository;
        public OrderService(IOrderRepository odrerRepository)
        {
            _odrerRepository = odrerRepository;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _odrerRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetByStoreIdAsync(Guid storeId)
        {
            return await _odrerRepository.GetByStoreIdAsync(storeId);
        }
    }
}
