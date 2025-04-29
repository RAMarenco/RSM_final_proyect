using NorthWindTraders.Application.DTOs.Order;

namespace NorthWindTraders.Application.DTOs.Customer
{
    public class CustomerWithOrdersDto
    {
        public CustomerDto Customer { get; set; }
        public List<OrderDto> Orders { get; set; }
    }
}
