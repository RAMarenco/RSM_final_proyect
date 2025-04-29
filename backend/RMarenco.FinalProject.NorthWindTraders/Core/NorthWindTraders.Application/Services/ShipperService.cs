using AutoMapper;
using NorthWindTraders.Application.CustomExceptions;
using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Application.DTOs.Shipper;
using NorthWindTraders.Application.Interfaces;
using NorthWindTraders.Domain.Entities;
using NorthWindTraders.Domain.Interfaces;

namespace NorthWindTraders.Application.Services
{
    public class ShipperService(IShipperRepository shipperRepository, Lazy<IOrderService> orderService, IMapper mapper) : IShipperService
    {
        public async Task<IEnumerable<ShipperDto>> GetAllShippers()
        {
            IEnumerable<Shipper> shippers = await shipperRepository.GetAllShippers();
            return mapper.Map<IEnumerable<ShipperDto>>(shippers);
        }

        public async Task<Shipper> GetShipperById(int shipperId)
        {
            Shipper? shipper = await shipperRepository.GetShipperById(shipperId);
            if (shipper is null)
            {
                throw new NotFoundException($"Shipper with ID {shipperId} not found.");
            }

            return shipper;
        }

        public async Task<ShipperWithOrdersDto> GetShipperWithOrders(int shipperId)
        {
            Shipper shipper = await GetShipperById(shipperId);

            IEnumerable<Order> orders = await orderService.Value.GetOrderByShipperId(shipperId);

            return new ShipperWithOrdersDto
            {
                Shipper = mapper.Map<ShipperDto>(shipper),
                Orders = [.. mapper.Map<IEnumerable<OrderDto>>(orders)],
            };
        }
    }
}
