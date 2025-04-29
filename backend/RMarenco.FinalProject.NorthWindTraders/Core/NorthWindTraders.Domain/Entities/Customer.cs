using System.Text.Json.Serialization;

namespace NorthWindTraders.Domain.Entities
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
    }
}
