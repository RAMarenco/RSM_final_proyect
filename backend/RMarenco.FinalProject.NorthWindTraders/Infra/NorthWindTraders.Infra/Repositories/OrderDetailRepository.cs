using NorthWindTraders.Domain.Interfaces;
using NorthWindTraders.Infra.Persistence;

namespace NorthWindTraders.Infra.Repositories
{
    class OrderDetailRepository(AppDbContext context) : IOrderDetailRepository
    {
    }
}
