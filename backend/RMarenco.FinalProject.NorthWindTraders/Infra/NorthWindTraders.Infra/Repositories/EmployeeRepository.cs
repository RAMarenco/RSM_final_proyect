using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Domain.Interfaces;
using NorthWindTraders.Infra.Persistence;

namespace NorthWindTraders.Infra.Repositories
{
    public class EmployeeRepository(AppDbContext context, IMapper mapper) : IEmployeeRepository
    {
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var employeeModel = await context.Products
                .AsNoTracking()
                .ToListAsync();

            return mapper.Map<IEnumerable<Employee>>(employeeModel);
        }

        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            var employeeModel = await context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            return mapper.Map<Employee>(employeeModel);
        }
    }
}
