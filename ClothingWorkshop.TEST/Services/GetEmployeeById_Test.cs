using ClothingWorkshop.Domain.Entities;
using ClothingWorkshop.Infrastructure.Data;
using ClothingWorkshop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClothingWorkshop.TEST.Services
{
    public class GetEmployeeById_Test
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public GetEmployeeById_Test()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ClothingWorkshopTest_GetEmployeeById")
                .Options;
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ReturnsEmployeeDto_WhenEmployeeExists()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                context.Employees.Add(new Employee { EmployeeId = 1, Name = "Jhon", Occupation = "Developer" });
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var service = new EmployeeRepository(context);

                // Act
                var result = await service.GetEmployeeByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.EmployeeId);
                Assert.Equal("Jhon", result.Name);
                Assert.Equal("Developer", result.Occupation);
            }
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ReturnsNull_WhenEmployeeDoesNotExist()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var service = new EmployeeRepository(context);

                // Act
                var result = await service.GetEmployeeByIdAsync(99); // ID not exist

                // Assert
                Assert.Null(result);
            }
        }
    }
}
