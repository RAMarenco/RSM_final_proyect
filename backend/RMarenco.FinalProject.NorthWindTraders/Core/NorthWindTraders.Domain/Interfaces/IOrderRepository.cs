using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<(IEnumerable<Order> Orders, int TotalPages, int CurrentPage, int TotalItems)> GetAllOrders(int pageNumber, int pageSize);
        Task<Order> GetOrderById(int orderId);
        Task<IEnumerable<Order>> GetOrderByCustomerId(string customerId);
        Task<IEnumerable<Order>> GetOrderByShipperId(int shipperId);
        Task<IEnumerable<Order>> GetOrderByEmployeeId(int employeeId);
        Task<Order> AddOrder(Order order);
        Task<Order> UpdateOrder(Order order);
        Task DeleteOrder(Order order);
    }
}

