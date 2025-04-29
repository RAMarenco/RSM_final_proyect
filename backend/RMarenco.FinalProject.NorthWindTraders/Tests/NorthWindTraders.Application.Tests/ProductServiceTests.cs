using Moq;
using Xunit;
using NorthWindTraders.Application.Services;
using NorthWindTraders.Domain.Interfaces;

namespace NorthWindTraders.Application.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnProducts()
        {
            // Arrange
            // Mock repository behavior here

            // Act
            var result = await _productService.GetAllProducts();

            // Assert
            // Verify result and repository interactions
        }
    }
}
