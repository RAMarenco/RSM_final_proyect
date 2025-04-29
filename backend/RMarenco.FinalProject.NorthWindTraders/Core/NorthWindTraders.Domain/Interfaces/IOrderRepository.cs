using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<(IEnumerable<Order> Orders, int TotalPages, int CurrentPage, int TotalItems)> GetAllOrders(int pageNumber, int pageSize);
        Task<Order> GetOrderById(int orderId);
        Task<Order> AddOrder(Order order);
        Task DeleteOrder(Order order);
    }
}

