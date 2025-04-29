using Moq;
using NorthWindTraders.Application.Services;
using NorthWindTraders.Domain.Interfaces;
using AutoMapper;
using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Application.DTOs.Shipper;
using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Application.CustomExceptions;

namespace NorthWindTraders.Application.Tests
{
    public class ShipperServiceTests
    {
        private readonly Mock<IShipperRepository> _shipperRepositoryMock;
        private readonly Mock<IOrderService> _orderServiceInstanceMock;
        private readonly Lazy<IOrderService> _orderServiceLazy;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ShipperService _shipperService;

        public ShipperServiceTests()
        {
            _shipperRepositoryMock = new Mock<IShipperRepository>();
            _orderServiceInstanceMock = new Mock<IOrderService>();
            _orderServiceLazy = new Lazy<IOrderService>(() => _orderServiceInstanceMock.Object);
            _mapperMock = new Mock<IMapper>();
            _shipperService = new ShipperService(_shipperRepositoryMock.Object, _orderServiceLazy, _mapperMock.Object);
        }

        [Fact]
        public async Task GetShippers_ShouldReturnShippers()
        {
            // Arrange
            var shippers = new List<Shipper>
                {
                    new Shipper { ShipperID = 1, CompanyName = "Shipper1", Phone = "123456789" },
                    new Shipper { ShipperID = 2, CompanyName = "Shipper2", Phone = "987654321" }
                };
            var shipperDtos = shippers.Select(s => new ShipperDto { ShipperID = s.ShipperID, CompanyName = s.CompanyName, Phone = s.Phone }).ToList();

            _shipperRepositoryMock.Setup(repo => repo.GetAllShippers()).ReturnsAsync(shippers);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<ShipperDto>>(shippers)).Returns(shipperDtos);

            // Act
            var result = await _shipperService.GetAllShippers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(shipperDtos.Count, result.Count());
            _shipperRepositoryMock.Verify(repo => repo.GetAllShippers(), Times.Once);
        }

        [Fact]
        public async Task GetShipperById_ShouldReturnShipper_WhenShipperExists()
        {
            // Arrange
            var shipper = new Shipper { ShipperID = 1, CompanyName = "Shipper1", Phone = "123456789" };
            _shipperRepositoryMock.Setup(repo => repo.GetShipperById(1)).ReturnsAsync(shipper);

            // Act
            var result = await _shipperService.GetShipperById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(shipper.ShipperID, result.ShipperID);
            _shipperRepositoryMock.Verify(repo => repo.GetShipperById(1), Times.Once);
        }

        [Fact]
        public async Task GetShipperById_ShouldThrowNotFoundException_WhenShipperDoesNotExist()
        {
            // Arrange
            _shipperRepositoryMock
                .Setup(repo => repo.GetShipperById(1))
                .ReturnsAsync((Shipper)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _shipperService.GetShipperById(1));
            Assert.Equal("Shipper with ID 1 not found.", ex.Message);

            _shipperRepositoryMock.Verify(repo => repo.GetShipperById(1), Times.Once);
        }

        [Fact]
        public async Task GetShipperWithOrders_ShouldReturnShipperWithOrders_WhenShipperExists()
        {
            // Arrange
            var shipper = new Shipper { ShipperID = 1, CompanyName = "Shipper1", Phone = "123456789" };
            var orders = new List<Order>
                {
                    new Order { OrderID = 1, ShipVia = 1, ShipAddress = "Address1", ShipCity = "City1", ShipCountry = "Country1" },
                    new Order { OrderID = 2, ShipVia = 1, ShipAddress = "Address2", ShipCity = "City2", ShipCountry = "Country2" }
                };

            var shipperWithOrdersDto = new ShipperWithOrdersDto
            {
                Shipper = new ShipperDto { ShipperID = shipper.ShipperID, CompanyName = shipper.CompanyName, Phone = shipper.Phone },
                Orders = orders.Select(o => new OrderDto
                {
                    OrderID = o.OrderID,
                    ShipAddress = o.ShipAddress,
                    ShipCity = o.ShipCity,
                    ShipCountry = o.ShipCountry
                }).ToList()
            };

            _shipperRepositoryMock.Setup(repo => repo.GetShipperById(1)).ReturnsAsync(shipper);
            _orderServiceInstanceMock.Setup(service => service.GetOrderByShipperId(1)).ReturnsAsync(orders);
            _mapperMock.Setup(mapper => mapper.Map<ShipperDto>(shipper)).Returns(shipperWithOrdersDto.Shipper);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<OrderDto>>(orders)).Returns(shipperWithOrdersDto.Orders);

            // Act
            var result = await _shipperService.GetShipperWithOrders(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(shipperWithOrdersDto.Shipper.ShipperID, result.Shipper.ShipperID);
            Assert.Equal(shipperWithOrdersDto.Orders.Count, result.Orders.Count);
            _shipperRepositoryMock.Verify(repo => repo.GetShipperById(1), Times.Once);
            _orderServiceInstanceMock.Verify(service => service.GetOrderByShipperId(1), Times.Once);
        }
    }
}
