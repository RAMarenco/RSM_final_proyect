using AutoMapper;
using NorthWindTraders.Application.CustomExceptions;
using NorthWindTraders.Application.DTOs.Employee;
using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Domain.Interfaces;

namespace NorthWindTraders.Application.Services
{
    class EmployeeService(IEmployeeRepository employeeRepository, Lazy<IOrderService> orderService, IMapper mapper) : IEmployeeService
    {
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            IEnumerable<Employee> employees = await employeeRepository.GetAllEmployees();
            return mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            Employee? employee = await employeeRepository.GetEmployeeById(employeeId);
            if (employee is null)
            {
                throw new NotFoundException($"Employee with ID {employeeId} not found.");
            }

            return employee;
        }

        public async Task<EmployeeWithOrdersDto> GetEmployeeWithOrders(int employeeId)
        {
            Employee employee = await GetEmployeeById(employeeId);

            IEnumerable<Order> orders = await orderService.Value.GetOrderByEmployeeId(employeeId);

            return new EmployeeWithOrdersDto
            {
                Employee = mapper.Map<EmployeeDto>(employee),
                Orders = [.. mapper.Map<IEnumerable<OrderDto>>(orders)],
            };
        }
    }
}
