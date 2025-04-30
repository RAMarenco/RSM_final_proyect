using Moq;
using Xunit;
using NorthWindTraders.Application.Services;
using NorthWindTraders.Domain.Interfaces;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Application.CustomExceptions;
using NorthWindTraders.Application.Interfaces;

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
        public async Task GetAllProducts_ShouldReturnProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductID = 1, ProductName = "Product1", UnitPrice = 10.0m },
                new Product { ProductID = 2, ProductName = "Product2", UnitPrice = 20.0m }
            };
            _productRepositoryMock.Setup(repo => repo.GetAllProducts()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Product1", result.First().ProductName);
            _productRepositoryMock.Verify(repo => repo.GetAllProducts(), Times.Once);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnProduct()
        {
            // Arrange
            int productId = 1;
            var product = new Product { ProductID = productId, ProductName = "Product1", UnitPrice = 10.0m };
            _productRepositoryMock.Setup(repo => repo.GetProductById(productId)).ReturnsAsync(product);

            // Act
            var result = await _productService.GetProductById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.ProductID);
            Assert.Equal("Product1", result.ProductName);
            _productRepositoryMock.Verify(repo => repo.GetProductById(productId), Times.Once);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnNull_WhenProductNotFound()
        {
            // Arrange
            int productId = 999;
            _productRepositoryMock.Setup(repo => repo.GetProductById(productId)).ReturnsAsync((Product)null);

            // Acc & Assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _productService.GetProductById(productId));
            Assert.Equal($"Product with ID {productId} not found.", ex.Message);
            _productRepositoryMock.Verify(repo => repo.GetProductById(productId), Times.Once);
        }
    }
}
