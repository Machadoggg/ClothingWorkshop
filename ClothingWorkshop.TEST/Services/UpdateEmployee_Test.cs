using ClothingWorkshop.Application.DTO;
using ClothingWorkshop.Domain.Entities;
using ClothingWorkshop.Infrastructure.Data;
using ClothingWorkshop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClothingWorkshop.TEST.Services
{
    public class UpdateEmployee_Test
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public UpdateEmployee_Test()
        {
            // Configura la base de datos en memoria
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ClothingWorkshopTest")
                .Options;
        }

        [Fact]
        public async Task UpdateEmployeeAsync_UpdatesEmployeeInDatabase_WhenEmployeeExists()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                context.Employees.Add(new Employee { EmployeeId = 1, Name = "Jhon", Occupation = "Developer" });
                await context.SaveChangesAsync();
            }

            var employeeDto = new EmployeeDto
            {
                EmployeeId = 1,
                Name = "Carl",
                Occupation = "Administrator"
            };

            // Act
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var service = new EmployeeRepository(context);
                await service.UpdateEmployeeAsync(employeeDto);
            }

            // Assert
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var employee = await context.Employees.FindAsync(1);
                Assert.NotNull(employee);
                Assert.Equal("Carl", employee.Name);
                Assert.Equal("Administrator", employee.Occupation);
            }
        }

        [Fact]
        public async Task UpdateEmployeeAsync_DoesNothing_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employeeDto = new EmployeeDto
            {
                EmployeeId = 99, // id not exist
                Name = "Carlos",
                Occupation = "Administrador"
            };

            // Act
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var service = new EmployeeRepository(context);
                await service.UpdateEmployeeAsync(employeeDto);
            }

            // Assert
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var employee = await context.Employees.FindAsync(99);
                Assert.Null(employee);
            }
        }
    }
}
