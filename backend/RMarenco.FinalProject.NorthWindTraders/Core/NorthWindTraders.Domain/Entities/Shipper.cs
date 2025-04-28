using System.Text.Json.Serialization;

namespace NorthWindTraders.Domain.Entities
{
    public class Shipper
    {
        public int ShipperID { get; set; }
        public required string CompanyName { get; set; }
        public required string Phone { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Order> Order { get; set; }
    }
}
