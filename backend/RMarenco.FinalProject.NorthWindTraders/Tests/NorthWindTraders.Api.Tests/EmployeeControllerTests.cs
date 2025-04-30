using Microsoft.AspNetCore.Mvc.Testing;
using NorthWindTraders.Application.DTOs.Employee;
using System.Net.Http.Json;
using System.Net;

namespace NorthWindTraders.Api.Tests
{
    public class EmployeeControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EmployeeControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetEmployees_ShouldReturnOkWithEmployees()
        {
            // Act
            var response = await _client.GetAsync("/api/Employee");

            // Assert
            response.EnsureSuccessStatusCode();
            var employees = await response.Content.ReadFromJsonAsync<IEnumerable<EmployeeDto>>();
            Assert.NotNull(employees);
            Assert.NotEmpty(employees);
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnOkWithEmployee()
        {
            // Arrange
            int employeeId = 1;

            // Act
            var response = await _client.GetAsync($"/api/Employee/{employeeId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var employeeWithOrders = await response.Content.ReadFromJsonAsync<EmployeeWithOrdersDto>();
            Assert.NotNull(employeeWithOrders);
            Assert.Equal(employeeId, employeeWithOrders.Employee.EmployeeID);
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnNotFound_WhenEmployeeDoesNotExist()
        {
            // Arrange
            int invalidEmployeeId = 999;

            // Act
            var response = await _client.GetAsync($"/api/Employee/{invalidEmployeeId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
