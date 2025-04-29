using NorthWindTraders.Application.DTOs.Employee;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int employeeId);
        Task<EmployeeWithOrdersDto> GetEmployeeWithOrders(int employeeId);
    }
}
