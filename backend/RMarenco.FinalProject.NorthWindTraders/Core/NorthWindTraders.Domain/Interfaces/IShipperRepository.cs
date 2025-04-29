using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Domain.Interfaces
{
    public interface IShipperRepository
    {
        Task<IEnumerable<Shipper>> GetAllShippers();
        Task<Shipper> GetShipperById(int shipperId);
    }
}
