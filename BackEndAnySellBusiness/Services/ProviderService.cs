using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellBusiness.Services.Interfaces;
using BackEndAnySellDataAccess.Entities;
using BackEndSellViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndAnySellBusiness.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public ProviderService(IProviderRepository providerRepository, IEmployeeRepository employeeRepository)
        {
            _providerRepository = providerRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> AddAsync(AddProviderViewModel providerModel, string userName)
        {
          var employee = await _employeeRepository.GetAsync(userName);

            if (providerModel != null)
            {
                var provider = new Provider()
                {
                    Name = providerModel.Name,
                    Email = providerModel.Email,
                    Phone = providerModel.Phone,
                    Other = providerModel.Other,
                    EmployeeId = employee.Id
                };

                return await _providerRepository.AddAsync(provider);                   // передаем уже готовый объект для сохранения в базу данных               
            }
            return false;
        }

        public async Task<IEnumerable<Provider>> GetAsync(string userName)
        {
            return await _providerRepository.GetAsync(userName);
        }
        public async Task<Provider> GetByIdAsync(Guid id)
        {
            return await _providerRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(UpdateProviderViewModel providerModel)
        {
            var provider = await _providerRepository.GetByIdAsync(providerModel.Id);

            provider.Name = providerModel.Name;
            provider.Email = providerModel.Email;
            provider.Phone = providerModel.Phone;
            provider.Other = providerModel.Other;

             return await _providerRepository.UpdateAsync(provider);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var provider = await _providerRepository.GetByIdAsync(id);

            if (provider.Comings.Count == 0)
            {
                return await _providerRepository.DeleteAsync(id);
            }
            return false;
        }

        public async Task<IEnumerable<Coming>> GetComingsAsync(Guid id, Guid storeId)
        {
            return await _providerRepository.GetComingsAsync(id, storeId);
        }
    }
}


