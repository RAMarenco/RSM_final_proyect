using NorthWindTraders.Application.DTOs.Customer;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomers();
        Task<Customer> GetCustomerById(string customerId);
        Task<CustomerWithOrdersDto> GetCustomerWithOrders(string customerId);
    }
}
