using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
