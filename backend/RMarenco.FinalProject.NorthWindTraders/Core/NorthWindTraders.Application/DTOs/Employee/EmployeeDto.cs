namespace NorthWindTraders.Application.DTOs.Employee
{
    public class EmployeeDto
    {
        public int EmployeeID { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        //public required byte[] Photo { get; set; }
    }
}
