using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Entity = NorthWindTraders.Domain.Entities;
using Model = NorthWindTraders.Infra.Persistence.Models;
using NorthWindTraders.Domain.Interfaces;
using NorthWindTraders.Infra.Persistence;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Infra.Persistence.Models;

namespace NorthWindTraders.Infra.Repositories
{
    public class OrderRepository(AppDbContext context, IMapper mapper) : IOrderRepository
    {
        public async Task AddOrder(Entity.Order order)
        {
            var orderModel = mapper.Map<Model.Order>(order);
            context.Orders.Add(orderModel);
            await context.SaveChangesAsync();
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

            pageNumber = Math.Abs(pageNumber);

            var totalItems = await orderQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            if (pageNumber > totalPages)
            {
                pageNumber = totalPages == 0 ? 1 : totalPages;
            }

            var pagedOrders = await orderQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mappedOrders = mapper.Map<IEnumerable<Entity.Order>>(pagedOrders);

            return (mappedOrders, totalPages, pageNumber, totalItems);
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
