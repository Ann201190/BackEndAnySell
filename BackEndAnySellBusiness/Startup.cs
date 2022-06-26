using BackEndAnySellBusiness.Services;
using BackEndAnySellBusiness.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BackEndAnySellBusiness
{
    public static class Startup
    {
        public static void Start(IServiceCollection services, string connectStr)
        {
            //Dependency injection:
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IReservationProductService, ReservationProductService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IProviderService, ProviderService>();
            services.AddTransient<IComingService, ComingService>();     
            services.AddTransient<IBalanceProductService, BalanceProductService>();
     

            //передаем в Startup DataAccess нужные параметры: service и строку подключения:
            AppartmentAppDataAccess.Startup.Start(services, connectStr);
        }
    }

}
