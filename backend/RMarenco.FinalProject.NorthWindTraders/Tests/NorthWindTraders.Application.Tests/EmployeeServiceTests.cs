using Moq;
using Xunit;
using NorthWindTraders.Application.Services;
using NorthWindTraders.Domain.Interfaces;
using AutoMapper;
using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Application.DTOs.Employee;
using NorthWindTraders.Application.CustomExceptions;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Application.DTOs.Order;

namespace NorthWindTraders.Application.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IOrderService> _orderServiceInstanceMock;
        private readonly Lazy<IOrderService> _orderServiceLazy;
        private readonly Mock<IMapper> _mapperMock;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _orderServiceInstanceMock = new Mock<IOrderService>();
            _orderServiceLazy = new Lazy<IOrderService>(() => _orderServiceInstanceMock.Object);
            _mapperMock = new Mock<IMapper>();
            _employeeService = new EmployeeService(_employeeRepositoryMock.Object, _orderServiceLazy, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllEmployees_ShouldReturnEmployees()
        {
            // Arrange
            var employees = new List<Employee> { new Employee { EmployeeID = 1, FirstName = "John", LastName = "Doe" } };
            _employeeRepositoryMock.Setup(repo => repo.GetAllEmployees()).ReturnsAsync(employees);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<EmployeeDto>>(employees))
                       .Returns(new List<EmployeeDto> { new EmployeeDto { EmployeeID = 1, FirstName = "John", LastName="John" } });

            // Act
            var result = await _employeeService.GetAllEmployees();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result.First().EmployeeID);
            _employeeRepositoryMock.Verify(repo => repo.GetAllEmployees(), Times.Once);
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnEmployee()
        {
            // Arrange
            var employee = new Employee { EmployeeID = 1, FirstName = "John", LastName = "Doe" };
            _employeeRepositoryMock.Setup(repo => repo.GetEmployeeById(1)).ReturnsAsync(employee);

            // Act
            var result = await _employeeService.GetEmployeeById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.EmployeeID);
            _employeeRepositoryMock.Verify(repo => repo.GetEmployeeById(1), Times.Once);
        }

        [Fact]
        public async Task GetEmployeeById_ShouldThrowNotFoundException_WhenEmployeeNotFound()
        {
            // Arrange
            _employeeRepositoryMock.Setup(repo => repo.GetEmployeeById(999)).ReturnsAsync((Employee)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _employeeService.GetEmployeeById(999));
            Assert.Equal("Employee with ID 999 not found.", ex.Message);

            _employeeRepositoryMock.Verify(repo => repo.GetEmployeeById(999), Times.Once);
        }

        [Fact]
        public async Task GetEmployeeWithOrders_ShouldReturnEmployeeWithOrders()
        {
            // Arrange
            var employee = new Employee { EmployeeID = 1, FirstName = "John", LastName = "Doe" };
            var orders = new List<Order> { new Order { OrderID = 1 } };

            _employeeRepositoryMock.Setup(repo => repo.GetEmployeeById(1)).ReturnsAsync(employee);
            _orderServiceInstanceMock.Setup(service => service.GetOrderByEmployeeId(1)).ReturnsAsync(orders);
            _mapperMock.Setup(mapper => mapper.Map<EmployeeDto>(employee)).Returns(new EmployeeDto { EmployeeID = 1, FirstName = "John", LastName = "Doe" });
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<OrderDto>>(orders))
                       .Returns(new List<OrderDto> { new OrderDto { OrderID = 1 } });

            // Act
            var result = await _employeeService.GetEmployeeWithOrders(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Employee.EmployeeID);
            Assert.Single(result.Orders);
            _employeeRepositoryMock.Verify(repo => repo.GetEmployeeById(1), Times.Once);
            _orderServiceInstanceMock.Verify(service => service.GetOrderByEmployeeId(1), Times.Once);
        }
    }
}