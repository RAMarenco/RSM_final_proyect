using NorthWindTraders.Application.DTOs.Order;

namespace NorthWindTraders.Application.DTOs.Shipper
{
    public class ShipperWithOrdersDto
    {
        public ShipperDto Shipper { get; set; }
        public List<OrderDto> Orders { get; set; }
    }
}
