namespace NorthWindTraders.Domain.Entities
{
    public class Customer
    {
        public required string CustomerID { get; set; }
        public required string CompanyName { get; set; }
        public List<Order> Order { get; set; }
    }
}
