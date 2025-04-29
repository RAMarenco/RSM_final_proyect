using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Application.Interfaces
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(int orderId);
        Task AddOrderDetails(IEnumerable<OrderItemDto> orderItems, Order OrderId);
        Task DeleteOrderDetail(Order order);
    }
}
