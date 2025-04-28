namespace NorthWindTraders.Domain.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public required string ProductName { get; set; }
        public required string UnitPrice { get; set; }
    }
}
