using NorthWindTraders.Application.DTOs.Customer;
using NorthWindTraders.Application.DTOs.Employee;
using NorthWindTraders.Application.DTOs.Shipper;

namespace NorthWindTraders.Application.DTOs.Order
{
    public class OrderDto
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountry { get; set; }
        public CustomerDto Customer { get; set; }
        public EmployeeDto Employee { get; set; }
        public ShipperDto Shipper { get; set; }
    }
}
