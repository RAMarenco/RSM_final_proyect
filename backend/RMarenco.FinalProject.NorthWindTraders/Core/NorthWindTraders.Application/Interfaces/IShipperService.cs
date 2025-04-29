using NorthWindTraders.Application.DTOs.Shipper;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Application.Interfaces
{
    public interface IShipperService
    {
        Task<IEnumerable<ShipperDto>> GetAllShippers();
        Task<Shipper> GetShipperById(int shipperId);
        Task<ShipperWithOrdersDto> GetShipperWithOrders(int shipperId);
    }
}
