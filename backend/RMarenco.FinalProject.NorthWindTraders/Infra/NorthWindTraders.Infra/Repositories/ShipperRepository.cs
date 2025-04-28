using NorthWindTraders.Domain.Interfaces;
using NorthWindTraders.Infra.Persistence;

namespace NorthWindTraders.Infra.Repositories
{
    class ShipperRepository(AppDbContext context) : IShipperRepository
    {
    }
}
