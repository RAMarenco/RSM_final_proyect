using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace NorthWindTraders.Api.Tests
{
    public class ProductControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetProducts_ShouldReturnOk()
        {
            // Act
            var response = await _client.GetAsync("/api/Product");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
