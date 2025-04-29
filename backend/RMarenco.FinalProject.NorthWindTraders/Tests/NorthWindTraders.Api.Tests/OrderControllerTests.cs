using Microsoft.AspNetCore.Mvc.Testing;

namespace NorthWindTraders.Api.Tests
{
    public class OrderControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public OrderControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetOrders_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/Order");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
