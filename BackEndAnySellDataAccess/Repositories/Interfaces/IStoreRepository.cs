﻿using BackEndAnySellDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndAnySellDataAccess.Repositories.Interfaces
{
    public interface IStoreRepository
    {
        Task<Store> GetByIdAsync(Guid id);
        Task<IEnumerable<Store>> GetAsync(string userName);
    }
}
