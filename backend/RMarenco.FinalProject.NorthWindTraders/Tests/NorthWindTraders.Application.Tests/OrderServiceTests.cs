using Moq;
using Xunit;
using NorthWindTraders.Application.Services;
using NorthWindTraders.Domain.Interfaces;
using NorthWindTraders.Application.Interfaces;
using AutoMapper;
using NorthWindTraders.Application.CustomExceptions;
using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Application.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly Mock<IEmployeeService> _employeeServiceMock;
        private readonly Mock<IShipperService> _shipperServiceMock;
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<IOrderDetailService> _orderDetailServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _customerServiceMock = new Mock<ICustomerService>();
            _employeeServiceMock = new Mock<IEmployeeService>();
            _shipperServiceMock = new Mock<IShipperService>();
            _productServiceMock = new Mock<IProductService>();
            _orderDetailServiceMock = new Mock<IOrderDetailService>();
            _mapperMock = new Mock<IMapper>();
            _orderService = new OrderService(
                _orderRepositoryMock.Object, 
                _customerServiceMock.Object, 
                _employeeServiceMock.Object, 
                _shipperServiceMock.Object, 
                _productServiceMock.Object,
                _orderDetailServiceMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task GetOrderWithDetails_ShouldReturnOrderWithDetails()
        {
            // Arrange
            int orderId = 1;
            var order = new Order { OrderID = orderId };
            var orderDetails = new List<OrderDetail> { new OrderDetail { OrderID = orderId } };
            var orderDto = new OrderDto { OrderID = orderId };
            var orderWithDetailsDto = new OrderWithDetailsDto
            {
                Order = orderDto,
                OrderDetails = new List<OrderDetailDto> { new OrderDetailDto { OrderID = orderId } }
            };

            _orderRepositoryMock.Setup(repo => repo.GetOrderById(orderId)).ReturnsAsync(order);
            _orderDetailServiceMock.Setup(service => service.GetOrderDetailsByOrderId(orderId)).ReturnsAsync(orderDetails);
            _mapperMock.Setup(mapper => mapper.Map<OrderDto>(order)).Returns(orderDto);
            _mapperMock.Setup(mapper => mapper.Map<List<OrderDetailDto>>(orderDetails)).Returns(orderWithDetailsDto.OrderDetails);

            // Act
            var result = await _orderService.GetOrderWithDetails(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.Order.OrderID);
            Assert.Single(result.OrderDetails);
        }

        [Fact]
        public async Task GetAllOrders_ShouldReturnPaginatedOrders()
        {
            // Arrange
            int pageNumber = 1;
            var orders = new List<Order> { new Order { OrderID = 1 } };
            var orderDtos = new List<OrderDto> { new OrderDto { OrderID = 1 } };
            var paginatedResult = (orders, 1, 1, 1);

            _orderRepositoryMock.Setup(repo => repo.GetAllOrders(pageNumber, 10)).ReturnsAsync(paginatedResult);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<OrderDto>>(orders)).Returns(orderDtos);

            // Act
            var result = await _orderService.GetAllOrders(pageNumber);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Data);
            Assert.Equal(1, result.TotalPages);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(1, result.TotalItems);
            Assert.Equal(1, result.Data.First().OrderID);
            _orderRepositoryMock.Verify(repo => repo.GetAllOrders(pageNumber, 10), Times.Once);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnOrder()
        {
            // Arrange
            int orderId = 1;
            var order = new Order { OrderID = orderId };

            _orderRepositoryMock.Setup(repo => repo.GetOrderById(orderId)).ReturnsAsync(order);

            // Act
            var result = await _orderService.GetOrderById(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.OrderID);
            _orderRepositoryMock.Verify(repo => repo.GetOrderById(orderId), Times.Once);
        }

        [Fact]
        public async Task GetOrderById_ShouldThrowNotFoundException_WhenOrderNotFound()
        {
            // Arrange
            int orderId = 999;

            _orderRepositoryMock.Setup(repo => repo.GetOrderById(orderId)).ReturnsAsync((Order)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _orderService.GetOrderById(orderId));
            Assert.Equal($"Order with ID {orderId} not found.", ex.Message);
            _orderRepositoryMock.Verify(repo => repo.GetOrderById(orderId), Times.Once);
        }

        [Fact]
        public async Task AddOrder_ShouldReturnOrderId()
        {
            // Arrange
            var createOrderDto = new CreateOrderDto { OrderDate = System.DateTime.Now };
            var order = new Order { OrderID = 1 };

            _mapperMock.Setup(mapper => mapper.Map<Order>(createOrderDto)).Returns(order);
            _orderRepositoryMock.Setup(repo => repo.AddOrder(order)).ReturnsAsync(order);

            // Act
            var result = await _orderService.AddOrder(createOrderDto);

            // Assert
            Assert.Equal(1, result);
            _orderRepositoryMock.Verify(repo => repo.AddOrder(order), Times.Once);
        }

        [Fact]
        public async Task UpdateOrder_ShouldReturnUpdatedOrderId()
        {
            // Arrange
            int orderId = 1;
            var updateOrderDto = new UpdateOrderDto { OrderDate = System.DateTime.Now };
            var order = new Order { OrderID = orderId };

            _orderRepositoryMock.Setup(repo => repo.GetOrderById(orderId)).ReturnsAsync(order);
            _mapperMock.Setup(mapper => mapper.Map(updateOrderDto, order)).Returns(order);
            _orderRepositoryMock.Setup(repo => repo.UpdateOrder(order)).ReturnsAsync(order);

            // Act
            var result = await _orderService.UpdateOrder(orderId, updateOrderDto);

            // Assert
            Assert.Equal(orderId, result);
            _orderRepositoryMock.Verify(repo => repo.GetOrderById(orderId), Times.Once);
            _orderRepositoryMock.Verify(repo => repo.UpdateOrder(order), Times.Once);
        }

        [Fact]
        public async Task DeleteOrder_ShouldDeleteOrder()
        {
            // Arrange
            int orderId = 1;
            var order = new Order { OrderID = orderId };

            _orderRepositoryMock.Setup(repo => repo.GetOrderById(orderId)).ReturnsAsync(order);

            // Act
            await _orderService.DeleteOrder(orderId);

            // Assert
            _orderRepositoryMock.Verify(repo => repo.DeleteOrder(order), Times.Once);
        }
    }
}
