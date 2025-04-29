using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(string customerId);
    }
}
