using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderWithDetailsDto> GetOrderWithDetails(int orderId);
        Task<IEnumerable<Order>> GetAllOrders(int pageNumber);
        Task<Order> GetOrderById(int orderId);
        Task<int> AddOrder(CreateOrderDto createOrderDto);
        Task DeleteOrder(int orderId);
    }
}
