using Microsoft.Extensions.DependencyInjection;
using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Application.MappingProfiles;
using NorthWindTraders.Application.Services;

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
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IShipperService, ShipperService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
