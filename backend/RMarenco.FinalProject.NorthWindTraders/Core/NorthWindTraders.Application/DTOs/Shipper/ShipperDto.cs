namespace NorthWindTraders.Application.DTOs.Shipper
{
    public class ShipperDto
    {
        public int ShipperID { get; set; }
        public required string CompanyName { get; set; }
        public required string Phone { get; set; }
    }
}
