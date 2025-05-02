using Microsoft.Extensions.DependencyInjection;
using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Application.MappingProfiles;
using NorthWindTraders.Application.Services;
using NorthWindTraders.Domain.Interfaces;
using NorthWindTraders.Infra.Reports;

namespace NorthWindTraders.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // AutoMapper Profiles
            services.AddAutoMapper(typeof(OrderProfile));
            services.AddAutoMapper(typeof(OrderDetailProfile));
            services.AddAutoMapper(typeof(CustomerProfile));
            services.AddAutoMapper(typeof(EmployeeProfile));
            services.AddAutoMapper(typeof(ShipperProfile));

            // Servicios
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped(provider => new Lazy<IOrderService>(() => provider.GetRequiredService<IOrderService>()));
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IShipperService, ShipperService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderReportService, OrderReportService>();

            return services;
        }
    }
}
