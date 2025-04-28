using Microsoft.Extensions.DependencyInjection;
using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Application.Services;

namespace NorthWindTraders.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IShipperService, ShipperService>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
