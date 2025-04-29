using NorthWindTraders.Application.CustomExceptions;
using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Domain.Interfaces;

namespace NorthWindTraders.Application.Services
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await productRepository.GetAllProducts();
        }

        public async Task<Product> GetProductById(int productId)
        {
            Product? product = await productRepository.GetProductById(productId);
            if (product is null)
            {
                throw new NotFoundException($"Product with ID {productId} not found.");
            }

            return product;
        }
    }
}
