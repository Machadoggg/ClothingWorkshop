using ClothingWorkshop.Domain.Entities;
using ClothingWorkshop.Infrastructure.Data;
using ClothingWorkshop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClothingWorkshop.TEST.Services
{
    public class DeleteEmployee_Test
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public DeleteEmployee_Test()
        {
            // Configura la base de datos en memoria
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ClothingWorkshopTest_DeleteEmployee")
                .Options;
        }

        [Fact]
        public async Task DeleteEmployeeAsync_RemovesEmployeeFromDatabase_WhenEmployeeExists()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                context.Employees.Add(new Employee { EmployeeId = 1, Name = "Jhon", Occupation = "Developer" });
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var service = new EmployeeRepository(context);
                await service.DeleteEmployeeAsync(1);
            }

            // Assert
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var employee = await context.Employees.FindAsync(1);
                Assert.Null(employee);
            }
        }

        [Fact]
        public async Task DeleteEmployeeAsync_DoesNothing_WhenEmployeeDoesNotExist()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var service = new EmployeeRepository(context);

                // Act
                await service.DeleteEmployeeAsync(99); // ID not exist

                // Assert
                var employees = await context.Employees.ToListAsync();
                Assert.Empty(employees); //Verify employees list is empty
            }
        }
    }
}
