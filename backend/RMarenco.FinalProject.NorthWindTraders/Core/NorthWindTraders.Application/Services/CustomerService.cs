using AutoMapper;
using NorthWindTraders.Application.CustomExceptions;
using NorthWindTraders.Application.DTOs.Customer;
using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Domain.Interfaces;

namespace NorthWindTraders.Application.Services
{
    public class CustomerService(ICustomerRepository customerRepository, Lazy<IOrderService> orderService, IMapper mapper) : ICustomerService
    {
        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            IEnumerable<Customer> customers = await customerRepository.GetAllCustomers();
            return mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<Customer> GetCustomerById(string customerId)
        {
            Customer? customer = await customerRepository.GetCustomerById(customerId);
            if (customer is null)
            {
                throw new NotFoundException($"Customer with ID {customerId} not found.");
            }

            return customer;
        }

        public async Task<CustomerWithOrdersDto> GetCustomerWithOrders(string customerId)
        {
            Customer customer = await GetCustomerById(customerId);

            IEnumerable<Order> orders = await orderService.Value.GetOrderByCustomerId(customerId);

            return new CustomerWithOrdersDto
            {
                Customer = mapper.Map<CustomerDto>(customer),
                Orders = [.. mapper.Map<IEnumerable<OrderDto>>(orders)],
            };
        }
    }
}
