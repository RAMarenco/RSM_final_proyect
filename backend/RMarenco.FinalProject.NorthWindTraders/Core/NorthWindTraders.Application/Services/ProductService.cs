using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Domain.Interfaces;

namespace NorthWindTraders.Application.Services
{
    public class ProductService(IProductRepository orderRepository) : IProductService
    {
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await orderRepository.GetAllProducts();
        }
    }
}
