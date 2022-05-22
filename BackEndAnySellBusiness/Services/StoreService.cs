using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndAnySellDataAccess.Enums;
using BackEndAnySellDataAccess.Repositories.Interfaces;
using BackEndSellViewModels.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public StoreService(IStoreRepository storeRepository, IEmployeeRepository employeeRepository)
        {
            _storeRepository = storeRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<Guid> AddWithEmployeeAsync(AddStoreWithEmployeeViewModel storeModel, string userName)
        {
            var store = new Store()
            {
                Id = storeModel.Id,
                Name = storeModel.NameStore
            };

            var employee = new Employee()
            {
                Name = storeModel.NameEmployee,
                SurName = storeModel.SurNameEmployee,
                Email = userName,
                Phone = storeModel.Phone,
                Role = Role.Manager             
            };

            employee.Stores.Add(store);

            var isAddedStore = await AddStoreAsync(store);
            var isAddedEmployee = await AddEmployeeAsync(employee);

            if (isAddedStore && isAddedEmployee)
            {
                return store.Id;
            }
            return Guid.Empty;
        }

        private async Task<bool> AddStoreAsync(Store store)
        {
            if (store != null)
            {
                return await _storeRepository.AddAsync(store);
            }
            return false;
        }

        private async Task<bool> AddEmployeeAsync(Employee employee)
        {
            if (employee != null)
            {
                return  await _employeeRepository.AddAsync(employee);
            }
            return false;
        }

        private async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            if (employee != null)
            {
                return await _employeeRepository.UpdateAsync(employee);
            }
            return false;
        }

        public async Task<bool> AddImageAsync(IFormFile file, Guid id)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);              
                return await _storeRepository.AddImageAsync(ms.ToArray(), id);
            }
          //  return false; //если фолсе удалить магазин вместе с сотрудником
        }

        public async Task<IEnumerable<Store>> GetAsync(string userName)
        {
            return await _storeRepository.GetAsync(userName);
        }

        public async Task<Store> GetByIdAsync(Guid id)
        {
            return await _storeRepository.GetByIdAsync(id);
        }

        public async Task<Guid> AddWithoutEmployeeAsync(AddStoreWithoutEmployeeViewModel storeModel, string userName)
        {
            var store = new Store()
            {
                Id = storeModel.Id,
                Name = storeModel.NameStore
            };

            var employee = await _employeeRepository.GetAsync(userName);

            employee.Stores.Add(store);

            var isAddedStore = await AddStoreAsync(store);
            var isAddedEmployee = await UpdateEmployeeAsync(employee);

            if (isAddedStore && isAddedEmployee)
            {
                return store.Id;
            }
            return Guid.Empty;
        }

        public async Task<Guid> UpdateStoreAsync(UpdateStoreWithoutImgeViewModel storeModel)
        {
           var store = await _storeRepository.GetByIdAsync(storeModel.Id);

            store.Name = storeModel.NameStore;

         var isUpdatedStore = await _storeRepository.UpdateAsync(store);

            if (isUpdatedStore )
            {
                return store.Id;
            }
            return Guid.Empty;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var store = await _storeRepository.GetByIdAsync(id);

            store.IsDeleted = true;
            return await _storeRepository.DeleteAsync(store);
        }
    }
}
