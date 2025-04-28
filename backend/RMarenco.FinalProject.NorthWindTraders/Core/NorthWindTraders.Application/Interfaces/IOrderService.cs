using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders(int pageNumber);
    }
}
