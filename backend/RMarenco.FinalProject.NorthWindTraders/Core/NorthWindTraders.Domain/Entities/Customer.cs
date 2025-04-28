using System.Text.Json.Serialization;

namespace NorthWindTraders.Domain.Entities
{
    public class Customer
    {
        public required string CustomerID { get; set; }
        public required string CompanyName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Order> Order { get; set; }
    }
}
