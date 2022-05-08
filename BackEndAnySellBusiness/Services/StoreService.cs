using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Repositories.Interfaces;
using BackEndSellViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services
{
    public class StoreService: IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public StoreService(IStoreRepository storeRepository, IEmployeeRepository employeeRepository)
        {
            _storeRepository = storeRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> AddAsync(AddStoreViewModel storeModel)
        {
            Store store = new Store()
            {
               Id = storeModel.Id,
               Name = storeModel.NameStore
            };

            Employee employee = new Employee()
            {
               StoreId = storeModel.Id,
               Name = storeModel.NameEmployee,
               SurName= storeModel.SurNameEmployee,
               Email= storeModel.Email,
               Phone=storeModel.Phone,
               Role = storeModel.Role       
            };

            if (await _storeRepository.AddAsync(store)) 
            {
                return await _employeeRepository.AddAsync(employee);
            }
            return false;
        }

        public async Task<IEnumerable<Store>> GetAsync(string userName)
        {
            return await _storeRepository.GetAsync(userName);
        }

        public async Task<Store> GetByIdAsync(Guid id)
        {
            return await _storeRepository.GetByIdAsync(id);
        }
    }
}
