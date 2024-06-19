namespace ClothingWorkshop.WEB.Models
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = default!; 
        public string Occupation { get; set; } = default!;
        public DateTime HiringDate { get; set; }
    }
}
