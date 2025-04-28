using NorthWindTraders.Domain.Interfaces;
using NorthWindTraders.Infra.Persistence;

namespace NorthWindTraders.Infra.Repositories
{
    public class CustomerRepository(AppDbContext context) : ICustomerRepository
    {
    }
}
