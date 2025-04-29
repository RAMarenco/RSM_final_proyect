using NorthWindTraders.Application.DTOs.Order;

namespace NorthWindTraders.Application.DTOs.Employee
{
    public class EmployeeWithOrdersDto
    {
        public EmployeeDto Employee { get; set; }
        public List<OrderDto> Orders { get; set; }
    }
}
