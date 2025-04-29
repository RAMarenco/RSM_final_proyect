using AutoMapper;
using NorthWindTraders.Application.CustomExceptions;
using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Domain.Interfaces;

namespace NorthWindTraders.Application.Services
{
    class OrderDetailService(
        IOrderDetailRepository orderDetailRepository, 
        IProductRepository productRepository
    ) : IOrderDetailService
    {
        public async Task AddOrderDetails(IEnumerable<OrderItemDto> orderItems, Order order)
        {
            var orderDetails = new List<OrderDetail>();

            foreach (var item in orderItems)
            {
                var product = await productRepository.GetProductById(item.ProductId);
                if (product is null)
                {
                    throw new NotFoundException($"Product with ID {item.ProductId} not found.");
                }

                var detail = new OrderDetail
                {
                    OrderID = order.OrderID,
                    ProductID = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.UnitPrice
                };

                orderDetails.Add(detail);
            }

            await orderDetailRepository.AddOrderDetails(orderDetails);
        }

        public async Task DeleteOrderDetail(Order order)
        {
            await orderDetailRepository.DeleteOrderDetail(order);
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(int orderId)
        {
            return await orderDetailRepository.GetOrderDetailsByOrderId(orderId);
        }
    }
}
