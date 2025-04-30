using Moq;
using Xunit;
using NorthWindTraders.Application.Services;
using NorthWindTraders.Domain.Interfaces;
using AutoMapper;
using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Application.DTOs.Customer;
using NorthWindTraders.Application.CustomExceptions;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Application.DTOs.Order;

namespace NorthWindTraders.Application.Tests
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IOrderService> _orderServiceInstanceMock;
        private readonly Lazy<IOrderService> _orderServiceLazy;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _orderServiceInstanceMock = new Mock<IOrderService>();
            _orderServiceLazy = new Lazy<IOrderService>(() => _orderServiceInstanceMock.Object);
            _mapperMock = new Mock<IMapper>();
            _customerService = new CustomerService(_customerRepositoryMock.Object, _orderServiceLazy, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllCustomers_ShouldReturnCustomers()
        {
            // Arrange
            var customers = new List<Customer> { new Customer { CustomerID = "C1", CompanyName = "Company1" } };
            _customerRepositoryMock.Setup(repo => repo.GetAllCustomers()).ReturnsAsync(customers);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<CustomerDto>>(customers))
                       .Returns(new List<CustomerDto> { new CustomerDto { CustomerID = "C1", CompanyName = "Company1" } });

            // Act
            var result = await _customerService.GetAllCustomers();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("C1", result.First().CustomerID);
            _customerRepositoryMock.Verify(repo => repo.GetAllCustomers(), Times.Once);
        }

        [Fact]
        public async Task GetCustomerById_ShouldReturnCustomer()
        {
            // Arrange
            var customer = new Customer { CustomerID = "C1", CompanyName = "Company1" };
            _customerRepositoryMock.Setup(repo => repo.GetCustomerById("C1")).ReturnsAsync(customer);

            // Act
            var result = await _customerService.GetCustomerById("C1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("C1", result.CustomerID);
            _customerRepositoryMock.Verify(repo => repo.GetCustomerById("C1"), Times.Once);
        }

        [Fact]
        public async Task GetCustomerById_ShouldThrowNotFoundException_WhenCustomerNotFound()
        {
            // Arrange
            _customerRepositoryMock.Setup(repo => repo.GetCustomerById("InvalidId")).ReturnsAsync((Customer)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _customerService.GetCustomerById("InvalidId"));
            Assert.Equal("Customer with ID InvalidId not found.", ex.Message);
            _customerRepositoryMock.Verify(repo => repo.GetCustomerById("InvalidId"), Times.Once);
        }

        [Fact]
        public async Task GetCustomerWithOrders_ShouldReturnCustomerWithOrders()
        {
            // Arrange
            var customer = new Customer { CustomerID = "C1", CompanyName = "Company1" };
            var orders = new List<Order> { new Order { OrderID = 1 } };

            var customerDto = new CustomerDto { CustomerID = "C1", CompanyName = "Company1" };

            _customerRepositoryMock.Setup(repo => repo.GetCustomerById("C1")).ReturnsAsync(customer);
            _orderServiceInstanceMock.Setup(service => service.GetOrderByCustomerId("C1")).ReturnsAsync(orders);
            _mapperMock.Setup(mapper => mapper.Map<CustomerDto>(customer)).Returns(customerDto);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<OrderDto>>(orders))
                       .Returns(new List<OrderDto> { new OrderDto { OrderID = 1 } });

            // Act
            var result = await _customerService.GetCustomerWithOrders("C1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("C1", result.Customer.CustomerID);
            Assert.Single(result.Orders);
            _customerRepositoryMock.Verify(repo => repo.GetCustomerById("C1"), Times.Once);
            _orderServiceInstanceMock.Verify(service => service.GetOrderByCustomerId("C1"), Times.Once);
        }
    }
}