namespace ClothingWorkshop.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = default!;
        public string Occupation { get; set; } = default!;
    }
}