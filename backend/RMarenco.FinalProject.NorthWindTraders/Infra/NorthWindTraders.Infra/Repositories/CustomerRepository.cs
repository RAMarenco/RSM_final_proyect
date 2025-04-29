using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthWindTraders.Domain.Interfaces;
using NorthWindTraders.Infra.Persistence;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Infra.Repositories
{
    public class CustomerRepository(AppDbContext context, IMapper mapper) : ICustomerRepository
    {
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var customerModel = await context.Products.ToListAsync();
            return mapper.Map<IEnumerable<Customer>>(customerModel);
        }

        public async Task<Customer> GetCustomerById(string customerId)
        {
            var customerModel = await context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            return mapper.Map<Customer>(customerModel);
        }
    }
}
