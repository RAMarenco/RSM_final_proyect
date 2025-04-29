using Microsoft.AspNetCore.Mvc.Testing;

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
        public async Task GetCustomers_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/Customer");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
