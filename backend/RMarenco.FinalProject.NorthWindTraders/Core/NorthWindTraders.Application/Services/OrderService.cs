using AutoMapper;
using NorthWindTraders.Application.CustomExceptions;
using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Domain.Interfaces;
using System.Net.Http.Headers;

namespace NorthWindTraders.Application.Services
{
    class OrderService(
        IOrderRepository orderRepository, 
        ICustomerRepository customerRepository, 
        IEmployeeRepository employeeRepository, 
        IShipperRepository shipperRepository,
        IProductRepository productRepository,
        IOrderDetailService orderDetailService,
        IMapper mapper
    ) : IOrderService
    {
        public async Task<int> AddOrder(CreateOrderDto createOrderDto)
        {
            Customer? customer = await customerRepository.GetCustomerById(createOrderDto.CustomerID);
            if (customer is null)
            {
                throw new NotFoundException($"Customer with ID {createOrderDto.CustomerID} not found.");
            }

            Employee? employee = await employeeRepository.GetEmployeeById(createOrderDto.EmployeeID);
            if (employee is null)
            {
                throw new NotFoundException($"Employee with ID {createOrderDto.EmployeeID} not found.");
            }

            Shipper? shipper = await shipperRepository.GetShipperById(createOrderDto.ShipVia);
            if (shipper is null)
            {
                throw new NotFoundException($"Shipper with ID {createOrderDto.ShipVia} not found.");
            }

            // Validate products
            foreach (var product in createOrderDto.Products)
            {
                var productEntity = await productRepository.GetProductById(product.ProductId);
                if (productEntity is null)
                {
                    throw new NotFoundException($"Product with ID {product.ProductId} not found.");
                }
            }

            var order = mapper.Map<Order>(createOrderDto);

            Order newOrder = await orderRepository.AddOrder(order);
            if (newOrder is null)
            {
                throw new ConflictException("Failed to create order.");
            }
            
            await orderDetailService.AddOrderDetails(createOrderDto.Products, newOrder);

            return newOrder.OrderID;
        }

        public async Task DeleteOrder(int orderId)
        {
            Order? order = await GetOrderById(orderId);

            await orderDetailService.DeleteOrderDetail(order);
            await orderRepository.DeleteOrder(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrders(int pageNumber = 1)
        {
            var(orders, totalPages, currentPage, totalItems) = await orderRepository.GetAllOrders(pageNumber, 10);
            return orders;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            Order? order = await orderRepository.GetOrderById(orderId);

            if (order is null)
            {
                throw new NotFoundException($"Order with ID {orderId} not found.");
            }

            return order;
        }

        public async Task<OrderWithDetailsDto> GetOrderWithDetails(int orderId)
        {
            Order order = await GetOrderById(orderId);
            IEnumerable<OrderDetail> orderDetail = await orderDetailService.GetOrderDetailsByOrderId(orderId);

            return new OrderWithDetailsDto
            {
                Order = mapper.Map<OrderDto>(order),
                OrderDetails = mapper.Map<List<OrderDetailDto>>(orderDetail)
            };
        }
    }
}
