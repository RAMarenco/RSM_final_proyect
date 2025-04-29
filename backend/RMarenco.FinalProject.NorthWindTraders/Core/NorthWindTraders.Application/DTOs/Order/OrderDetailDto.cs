namespace NorthWindTraders.Application.DTOs.Order
{
    public class OrderDetailDto
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
