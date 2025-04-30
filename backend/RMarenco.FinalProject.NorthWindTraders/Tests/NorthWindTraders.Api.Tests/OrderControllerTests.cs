using Microsoft.AspNetCore.Mvc.Testing;
using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Application.DTOs;
using System.Net.Http.Json;
using System.Net;
using Xunit.Abstractions;
using Xunit;

namespace NorthWindTraders.Api.Tests
{
    public class OrderControllerTests : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
    {
        private readonly HttpClient _client;
        private int _createdOrderId;

        public OrderControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        public async Task InitializeAsync()
        {
            // This method will be called before each test is executed.
            var createOrderDto = new CreateOrderDto
            {
                CustomerID = "ALFKI",
                EmployeeID = 1,
                OrderDate = DateTime.Now,
                ShipVia = 1,
                ShipAddress = "Test St",
                ShipCity = "Test City",
                ShipRegion = "Test Region",
                ShipPostalCode = "11111",
                ShipCountry = "Testland",
                Products = new List<OrderItemDto> { new() { ProductId = 1, Quantity = 1 } }
            };

            // Create order before each test
            var createResponse = await _client.PostAsJsonAsync("/api/Order", createOrderDto);
            createResponse.EnsureSuccessStatusCode();

            int page = int.MaxValue;

            // Get created order
            var createdOrder = await _client.GetFromJsonAsync<PaginatedDto<IEnumerable<OrderDto>>>($"/api/Order?pageNumber={page}");
            var orderId = createdOrder?.Data?.FirstOrDefault(o =>
                o.ShipAddress == createOrderDto.ShipAddress &&
                o.ShipCity == createOrderDto.ShipCity &&
                o.ShipRegion == createOrderDto.ShipRegion &&
                o.ShipPostalCode == createOrderDto.ShipPostalCode &&
                o.ShipCountry == createOrderDto.ShipCountry &&
                o.Customer.CustomerID == createOrderDto.CustomerID
            )?.OrderID ?? throw new Exception("Order couldn't be created");

            _createdOrderId = orderId;
        }

        public async Task DisposeAsync()
        {
            // This method will be called after each test is executed.
            if (_createdOrderId != 0)
            {
                var deleteResponse = await _client.DeleteAsync($"/api/Order/{_createdOrderId}");
                deleteResponse.EnsureSuccessStatusCode();
                var message = await deleteResponse.Content.ReadAsStringAsync();
                Assert.Contains("deleted successfully", message);
            }
        }

        [Fact]
        public async Task GetOrders_ShouldReturnOkWithOrders()
        {
            // Act
            var response = await _client.GetAsync("/api/Order");

            // Assert
            response.EnsureSuccessStatusCode();
            var orders = await response.Content.ReadFromJsonAsync<PaginatedDto<IEnumerable<OrderDto>>>();
            Assert.NotNull(orders);
            Assert.NotEmpty(orders.Data);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnOkWithOrderDetails()
        {
            // Arrange
            int orderId = _createdOrderId;

            // Act
            var response = await _client.GetAsync($"/api/Order/{orderId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var orderWithDetails = await response.Content.ReadFromJsonAsync<OrderWithDetailsDto>();
            Assert.NotNull(orderWithDetails);
            Assert.Equal(orderId, orderWithDetails.Order.OrderID);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            int invalidOrderId = 999;

            // Act
            var response = await _client.GetAsync($"/api/Order/{invalidOrderId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateOrder_ShouldReturnOkWithSuccessMessage()
        {
            // El pedido ya fue creado por InitializeAsync
            Assert.NotEqual(0, _createdOrderId);

            // Confirmar que se encuentra en la lista
            var response = await _client.GetAsync($"/api/Order/{_createdOrderId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains(_createdOrderId.ToString(), content);
        }

        [Fact]
        public async Task UpdateOrder_ShouldReturnOkWithSuccessMessage()
        {
            var updateOrderDto = new UpdateOrderDto
            {
                OrderDate = DateTime.Now,
                ShipVia = 1,
                ShipAddress = "456 Updated St",
                ShipCity = "Updated City",
                ShipRegion = "Test Region",
                ShipPostalCode = "67890",
                ShipCountry = "Updated Country"
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/Order/{_createdOrderId}", updateOrderDto);

            // Assert
            response.EnsureSuccessStatusCode();
            var message = await response.Content.ReadAsStringAsync();
            Assert.Contains("updated successfully", message);
        }

        [Fact]
        public async Task DeleteOrder_ShouldReturnOkWithSuccessMessage()
        {
            // Act
            var deleteResponse = await _client.DeleteAsync($"/api/Order/{_createdOrderId}");
            deleteResponse.EnsureSuccessStatusCode();
            var message = await deleteResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains("deleted successfully", message);
            _createdOrderId = 0;
        }
    }
}
