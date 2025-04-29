using Microsoft.AspNetCore.Mvc.Testing;

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
        public async Task GetEmployees_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/Employee");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
