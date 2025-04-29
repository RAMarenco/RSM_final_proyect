using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int employeeId);
    }
}
