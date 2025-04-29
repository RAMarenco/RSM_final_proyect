namespace NorthWindTraders.Application.DTOs.Order
{
    public class CreateOrderDto
    {
        public string CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public int ShipVia { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public List<OrderItemDto> Products { get; set; } = [];
    }

    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public short Quantity { get; set; }
    }
}
