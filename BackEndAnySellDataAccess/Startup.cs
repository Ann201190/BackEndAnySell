﻿using BackEndAnySellAccessDataAccess.Context;
using BackEndAnySellAccessDataAccess.Repositories;
using BackEndAnySellAccessDataAccess.Repositories.Interfaces;
using BackEndAnySellDataAccess.Repositories;
using BackEndAnySellDataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppartmentAppDataAccess
{
    public static class Startup
    {
        public static void Start(IServiceCollection services, string connectStr)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IDiscountRepository, DiscountRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IReservationProductRepository, ReservationProductRepository>();
            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddTransient< IProviderRepository, ProviderRepository >();
            services.AddTransient<IComingRepository, ComingRepository>();
            services.AddTransient< IBalanceProductRepository, BalanceProductRepository >();
            services.AddTransient< ITicketRepository, TicketRepository >();


            //  services.AddTransient<ICustomDbContext, CustomDbContext>();
            services.AddDbContext<CustomDbContext>(options => options.UseSqlServer(connectStr));
        }
    }

}
