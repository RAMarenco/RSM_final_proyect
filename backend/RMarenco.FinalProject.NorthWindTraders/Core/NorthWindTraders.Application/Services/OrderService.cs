using AutoMapper;
using NorthWindTraders.Application.CustomExceptions;
using NorthWindTraders.Application.DTOs;
using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Domain.Interfaces;

namespace NorthWindTraders.Application.Services
{
    class OrderService(
        IOrderRepository orderRepository,
        ICustomerService customerService,
        IEmployeeService employeeService,
        IShipperService shipperService,
        IProductService productService,
        IOrderDetailService orderDetailService,
        IMapper mapper
    ) : IOrderService
    {
        public async Task<int> AddOrder(CreateOrderDto createOrderDto)
        {
            // Validate CreateOrder Key Data
            Customer customer = await customerService.GetCustomerById(createOrderDto.CustomerID);
            Employee employee = await employeeService.GetEmployeeById(createOrderDto.EmployeeID);
            Shipper shipper = await shipperService.GetShipperById(createOrderDto.ShipVia);

            // Validate Products
            await ValidateProducts(createOrderDto.Products);

            Order order = mapper.Map<Order>(createOrderDto);

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

        public async Task<PaginatedDto<IEnumerable<OrderDto>>> GetAllOrders(int pageNumber = 1)
        {
            var (orders, totalPages, currentPage, totalItems) = await orderRepository.GetAllOrders(pageNumber, 10);
            return new PaginatedDto<IEnumerable<OrderDto>> {
                Data = mapper.Map<IEnumerable<OrderDto>>(orders),
                TotalPages = totalPages,
                CurrentPage = currentPage,
                TotalItems = totalItems
            };
        }

        public async Task<IEnumerable<Order>> GetOrderByCustomerId(string customerId)
        {
            return await orderRepository.GetOrderByCustomerId(customerId);
        }

        public async Task<IEnumerable<Order>> GetOrderByShipperId(int shipperId)
        {
            return await orderRepository.GetOrderByShipperId(shipperId);
        }

        public async Task<IEnumerable<Order>> GetOrderByEmployeeId(int employeeId)
        {
            return await orderRepository.GetOrderByEmployeeId(employeeId);
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

        public async Task<int> UpdateOrder(int orderId, UpdateOrderDto updateOrderDto)
        {
            Order? order = await orderRepository.GetOrderById(orderId);
            if (order is null)
            {
                throw new NotFoundException($"Order with ID {orderId} not found.");
            }

            // Validate updateOrderDto Key Data
            Shipper shipper = await shipperService.GetShipperById(updateOrderDto.ShipVia);

            // Validate Products
            await ValidateProducts(updateOrderDto.Products);

            // Update order details (non-product-related properties)
            order.OrderDate = updateOrderDto.OrderDate;
            order.ShipVia = updateOrderDto.ShipVia;
            order.ShipAddress = updateOrderDto.ShipAddress;
            order.ShipCity = updateOrderDto.ShipCity;
            order.ShipRegion =  updateOrderDto.ShipRegion;
            order.ShipPostalCode =  updateOrderDto.ShipPostalCode;
            order.ShipCountry = updateOrderDto.ShipCountry;
            order.Shipper = shipper;

            await orderRepository.UpdateOrder(order);

            await orderDetailService.DeleteOrderDetail(order);
            await orderDetailService.AddOrderDetails(updateOrderDto.Products, order);

            return order.OrderID;
        }

        private async Task ValidateProducts(List<OrderItemDto> products) {
            // Validate products
            foreach (OrderItemDto product in products)
            {
                await productService.GetProductById(product.ProductId);
            }
        }
    }
}
