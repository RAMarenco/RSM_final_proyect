using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Domain.Interfaces;
using NorthWindTraders.Infra.Persistence;
using Entity = NorthWindTraders.Domain.Entities;
using Model = NorthWindTraders.Infra.Persistence.Models;

namespace NorthWindTraders.Infra.Repositories
{
    public class OrderDetailRepository(AppDbContext context, IMapper mapper) : IOrderDetailRepository
    {
        public async Task AddOrderDetails(IEnumerable<Entity.OrderDetail> orderDetails)
        {
            var orderDetailModel = mapper.Map<IEnumerable<Model.OrderDetail>>(orderDetails);
            await context.OrderDetails.AddRangeAsync(orderDetailModel);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(int orderId)
        {
            var orderDetailModel = await context.OrderDetails
                .Include(od => od.Product)
                .AsNoTracking()
                .Where(od => od.OrderId == orderId)
                .ToListAsync();

            return mapper.Map<IEnumerable<OrderDetail>>(orderDetailModel);
        }

        public async Task DeleteOrderDetail(Entity.Order order)
        {
            var orderDetailModel = await context.OrderDetails
                .AsNoTracking()
                .Where(od => od.OrderId == order.OrderID)
                .ToListAsync();

            if (orderDetailModel.Any())
            {
                context.OrderDetails.RemoveRange(orderDetailModel);
                await context.SaveChangesAsync();
            }
        }
    }
}
