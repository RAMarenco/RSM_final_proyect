﻿using AutoMapper;
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

        public async Task<Entity.Order> UpdateOrder(Entity.Order order)
        {
            var orderModel = mapper.Map<Model.Order>(order);
            context.Orders.Update(orderModel);
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

            if (pageSize == 0)
            {
                var allOrders = await orderQuery.ToListAsync();
                var mappedOrders = mapper.Map<IEnumerable<Entity.Order>>(allOrders);
                return (mappedOrders, 1, 1, mappedOrders.Count());
            }

            var (pagedOrders, totalPages, currentPage, totalItems) = await paginationHelper.PaginateAsync(pageNumber, pageSize, orderQuery);
            var mappedPaged = mapper.Map<IEnumerable<Entity.Order>>(pagedOrders);

            return (mappedPaged, totalPages, currentPage, totalItems);
        }

        public async Task<Entity.Order> GetOrderById(int orderId)
        {
            var orderModel = await context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            return mapper.Map<Entity.Order>(orderModel);
        }

        public async Task<IEnumerable<Entity.Order>> GetOrderByCustomerId(string customerId)
        {
            var orderModel = await context.Orders
                .Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation)
                .AsNoTracking()
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();

            return mapper.Map<IEnumerable<Entity.Order>>(orderModel);
        }

        public async Task<IEnumerable<Entity.Order>> GetOrderByShipperId(int shipperId)
        {
            var orderModel = await context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .AsNoTracking()
                .Where(o => o.ShipVia == shipperId)
                .ToListAsync();

            return mapper.Map<IEnumerable<Entity.Order>>(orderModel);
        }

        public async Task<IEnumerable<Entity.Order>> GetOrderByEmployeeId(int employeeId)
        {
            var orderModel = await context.Orders
                .Include(o => o.Customer)
                .Include(o => o.ShipViaNavigation)
                .AsNoTracking()
                .Where(o => o.EmployeeId == employeeId)
                .ToListAsync();

            return mapper.Map<IEnumerable<Entity.Order>>(orderModel);
        }
    }
}
