using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Application.Interfaces
{
    public interface IShipperService
    {
        Task<IEnumerable<Shipper>> GetAllShippers();
    }
}
