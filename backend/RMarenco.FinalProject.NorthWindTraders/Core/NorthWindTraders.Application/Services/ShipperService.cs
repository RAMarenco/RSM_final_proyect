using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Domain.Interfaces;

namespace NorthWindTraders.Application.Services
{
    public class ShipperService(IShipperRepository shipperRepository) : IShipperService
    {
        public async Task<IEnumerable<Shipper>> GetAllShippers()
        {
            return await shipperRepository.GetAllShippers();
        }
    }
}
