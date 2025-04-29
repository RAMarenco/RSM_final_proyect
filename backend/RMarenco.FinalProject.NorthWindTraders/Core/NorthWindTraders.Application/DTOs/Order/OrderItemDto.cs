using System.ComponentModel.DataAnnotations;

namespace NorthWindTraders.Application.DTOs.Order
{
    public class OrderItemDto
    {
        [Required(ErrorMessage = "Product is required.")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Quantity is required.")]
        public short Quantity { get; set; }
    }
}
