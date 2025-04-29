using System.ComponentModel.DataAnnotations;

namespace NorthWindTraders.Application.DTOs.Order
{
    public class UpdateOrderDto
    {
        [Required(ErrorMessage = "Order Date is required.")]
        public DateTime OrderDate { get; set; }
        [Required(ErrorMessage = "Ship Via is required.")]
        public int ShipVia { get; set; }
        [Required(ErrorMessage = "Shipping Address is required.")]
        [StringLength(60, ErrorMessage = "Ship Address can't be longer than 60 characters.")]
        public string ShipAddress { get; set; }
        [Required(ErrorMessage = "City is required.")]
        [StringLength(15, ErrorMessage = "City can't be longer than 15 characters.")]
        public string ShipCity { get; set; }
        [StringLength(15, ErrorMessage = "Region can't be longer than 15 characters.")]
        public string ShipRegion { get; set; }
        [Required(ErrorMessage = "Postal Code is required.")]
        [StringLength(10, ErrorMessage = "Postal Code can't be longer than 10 characters.")]
        public string ShipPostalCode { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        [StringLength(15, ErrorMessage = "Country can't be longer than 15 characters.")]
        public string ShipCountry { get; set; }
        public List<OrderItemDto> Products { get; set; } = [];
    }
}
