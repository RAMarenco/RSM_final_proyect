using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Domain.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task AddOrderDetails(IEnumerable<OrderDetail> orderDetails);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(int orderId);
        Task DeleteOrderDetail(Order order);
    }
}
