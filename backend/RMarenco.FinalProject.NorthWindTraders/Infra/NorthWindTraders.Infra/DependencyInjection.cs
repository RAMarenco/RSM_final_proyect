using NorthWindTraders.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthWindTraders.Domain.Interfaces;
using NorthWindTraders.Infra.Repositories;
using NorthWindTraders.Infra.MappingProfiles;

namespace NorthWindTraders.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("NorthwindDbConnection"));
            });

            // AutoMapper Profiles
            services.AddAutoMapper(typeof(ShipperProfile));
            services.AddAutoMapper(typeof(ProductProfile));

            // Repositories
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IShipperRepository, ShipperRepository>();

            return services;
        }
    }
}
