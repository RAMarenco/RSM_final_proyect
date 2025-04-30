using Microsoft.AspNetCore.Mvc.Testing;
using NorthWindTraders.Application.DTOs.Customer;
using System.Net.Http.Json;
using System.Net;

namespace NorthWindTraders.Api.Tests
{
    public class CustomerControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CustomerControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCustomers_ShouldReturnOkWithCustomers()
        {
            // Act
            var response = await _client.GetAsync("/api/Customer");

            // Assert
            response.EnsureSuccessStatusCode();
            var customers = await response.Content.ReadFromJsonAsync<IEnumerable<CustomerDto>>();
            Assert.NotNull(customers);
            Assert.NotEmpty(customers);
        }

        [Fact]
        public async Task GetCustomerById_ShouldReturnOkWithCustomer()
        {
            // Arrange
            string customerId = "ALFKI";

            // Act
            var response = await _client.GetAsync($"/api/Customer/{customerId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var customerWithOrders = await response.Content.ReadFromJsonAsync<CustomerWithOrdersDto>();
            Assert.NotNull(customerWithOrders);
            Assert.Equal(customerId, customerWithOrders.Customer.CustomerID);
        }

        [Fact]
        public async Task GetCustomerById_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            string invalidCustomerId = "InvalidId";

            // Act
            var response = await _client.GetAsync($"/api/Customer/{invalidCustomerId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
