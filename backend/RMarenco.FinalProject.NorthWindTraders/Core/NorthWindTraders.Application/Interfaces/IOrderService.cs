using NorthWindTraders.Application.DTOs;
using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderWithDetailsDto> GetOrderWithDetails(int orderId);
        Task<PaginatedDto<IEnumerable<OrderDto>>> GetAllOrders(int pageNumber);
        Task<Order> GetOrderById(int orderId);
        Task<IEnumerable<Order>> GetOrderByCustomerId(string customerId);
        Task<IEnumerable<Order>> GetOrderByShipperId(int shipperId);
        Task<IEnumerable<Order>> GetOrderByEmployeeId(int employeeId);
        Task<int> AddOrder(CreateOrderDto createOrderDto);
        Task<int> UpdateOrder(int orderId, UpdateOrderDto updateOrderDto);
        Task<byte[]> GenerateAllOrderReport();
        Task<byte[]> GenerateOrderReport(int orderId);
        Task DeleteOrder(int orderId);
    }
}
