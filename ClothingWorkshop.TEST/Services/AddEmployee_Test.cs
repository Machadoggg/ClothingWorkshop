using ClothingWorkshop.Application.DTO;
using ClothingWorkshop.Infrastructure.Data;
using ClothingWorkshop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClothingWorkshop.TEST.Services
{
    public class AddEmployee_Test
    {
        private DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public AddEmployee_Test()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ClothingWorkshopTest_AddEmployee")
                .Options;
        }


        [Fact]
        public async Task AddEmployeeAsync_AddsEmployeeToDatabase()
        {
            // Arrange
            var employeeDto = new EmployeeDto
            {
                Name = "Carlos",
                Occupation = "Administrador"
            };

            // Act
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var service = new EmployeeRepository(context);
                await service.AddEmployeeAsync(employeeDto);
            }

            // Assert
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var employees = await context.Employees.ToListAsync();
                Assert.Single(employees); // Verify single employee in DB

                var employee = employees.First();
                Assert.Equal("Carlos", employee.Name);
                Assert.Equal("Administrador", employee.Occupation);
            }
        }
    }
}
