using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Domain.Interfaces;

namespace NorthWindTraders.Application.Services
{
    class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        public async Task<IEnumerable<Order>> GetAllOrders(int pageNumber = 1)
        {
            var(orders, totalPages, currentPage, totalItems) = await orderRepository.GetAllOrders(pageNumber, 10);
            return orders;
        }
    }
}
