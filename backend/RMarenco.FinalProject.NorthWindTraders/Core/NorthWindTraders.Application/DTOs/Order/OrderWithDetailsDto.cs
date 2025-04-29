namespace NorthWindTraders.Application.DTOs.Order
{
    public class OrderWithDetailsDto
    {
        public OrderDto Order { get; set; } = null!;
        public List<OrderDetailDto> OrderDetails { get; set; } = [];
    }
}
