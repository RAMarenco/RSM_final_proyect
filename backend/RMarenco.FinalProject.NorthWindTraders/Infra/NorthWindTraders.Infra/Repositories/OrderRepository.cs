using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Entity = NorthWindTraders.Domain.Entities;
using Model = NorthWindTraders.Infra.Persistence.Models;
using NorthWindTraders.Domain.Interfaces;
using NorthWindTraders.Infra.Persistence;

namespace NorthWindTraders.Infra.Repositories
{
    public class OrderRepository(AppDbContext context, IMapper mapper, IPaginationHelper paginationHelper) : IOrderRepository
    {
        public async Task<Entity.Order> AddOrder(Entity.Order order)
        {
            var orderModel = mapper.Map<Model.Order>(order);
            context.Orders.Add(orderModel);
            await context.SaveChangesAsync();
            
            return mapper.Map<Entity.Order>(orderModel);
        }

        public async Task DeleteOrder(Entity.Order order)
        {
            var orderModel = await context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == order.OrderID);

            if (orderModel != null)
            {
                context.Orders.Remove(orderModel);
                await context.SaveChangesAsync();
            }
        }

        public async Task<(IEnumerable<Entity.Order> Orders, int TotalPages, int CurrentPage, int TotalItems)> GetAllOrders(int pageNumber, int pageSize)
        {
            var orderQuery = context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation)
                .AsQueryable();

            var (pagedOrders, totalPages, currentPage, totalItems) = await paginationHelper.PaginateAsync(pageNumber, pageSize, orderQuery);
            var mappedOrders = mapper.Map<IEnumerable<Entity.Order>>(pagedOrders);

            return (mappedOrders, totalPages, currentPage, totalItems);
        }

        public async Task<Entity.Order> GetOrderById(int orderId)
        {
            var orderModel = await context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            return mapper.Map<Entity.Order>(orderModel);
        }
    }
}
