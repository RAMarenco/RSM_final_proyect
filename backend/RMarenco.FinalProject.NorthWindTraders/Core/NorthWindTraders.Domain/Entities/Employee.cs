using System.Text.Json.Serialization;

namespace NorthWindTraders.Domain.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public required byte[] Photo { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Order> Order { get; set; }
    }
}
