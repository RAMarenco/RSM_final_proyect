using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Domain.Interfaces;
using NorthWindTraders.Infra.Persistence;

namespace NorthWindTraders.Infra.Repositories
{
    public class ProductRepository(AppDbContext context, IMapper mapper) : IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var productModel = await context.Products
                .AsNoTracking()
                .ToListAsync();

            return mapper.Map<IEnumerable<Product>>(productModel);
        }

        public async Task<Product> GetProductById(int productId)
        {
            var productModel = await context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ProductId == productId);

            return mapper.Map<Product>(productModel);
        }
    }
}
