using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Domain.Interfaces;
using NorthWindTraders.Infra.Persistence;

namespace NorthWindTraders.Infra.Repositories
{
    public class ShipperRepository(AppDbContext context, IMapper mapper) : IShipperRepository
    {
        public async Task<IEnumerable<Shipper>> GetAllShippers()
        {
            var shipperModel = await context.Shippers.ToListAsync();
            return mapper.Map<IEnumerable<Shipper>>(shipperModel);
        }

        public async Task<Shipper> GetShipperById(int shipperId)
        {
            var shipperModel = await context.Shippers
                .FirstOrDefaultAsync(s => s.ShipperId == shipperId);

            return mapper.Map<Shipper>(shipperModel);
        }
    }
}
