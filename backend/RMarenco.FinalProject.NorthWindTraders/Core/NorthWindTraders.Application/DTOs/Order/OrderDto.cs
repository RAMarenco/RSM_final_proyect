using NorthWindTraders.Application.DTOs.Customer;
using NorthWindTraders.Application.DTOs.Employee;
using NorthWindTraders.Application.DTOs.Shipper;
using System.Text.Json.Serialization;

namespace NorthWindTraders.Application.DTOs.Order
{
    public class OrderDto
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountry { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CustomerDto Customer { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EmployeeDto Employee { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ShipperDto Shipper { get; set; }
    }
}
