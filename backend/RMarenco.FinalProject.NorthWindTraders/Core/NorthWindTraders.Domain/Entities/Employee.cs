using System.Text.Json.Serialization;

namespace NorthWindTraders.Domain.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public byte[] Photo { get; set; }
    }
}
