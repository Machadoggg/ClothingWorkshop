﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using ClothingWorkshop.Application.DTO;
using ClothingWorkshop.Application.Interfaces;
using ClothingWorkshop.Domain.Entities;
using ClothingWorkshop.Infrastructure.Repositories;
using ClothingWorkshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClothingWorkshop.TEST
{
    public class EmployeeServiceTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public EmployeeServiceTests()
        {
            // Configura la base de datos en memoria
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ClothingWorkshopTest")
                .Options;
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ReturnsEmployeeDtos()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                context.Employees.Add(new Employee { EmployeeId = 1, Name = "Jhon", Occupation = "Developer" });
                context.Employees.Add(new Employee { EmployeeId = 2, Name = "Maria", Occupation = "Desingner" });
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var service = new EmployeeRepository(context);

                // Act
                var result = await service.GetAllEmployeesAsync();

                // Assert
                var employeeDtos = Assert.IsAssignableFrom<IEnumerable<EmployeeDto>>(result);
                Assert.Equal(2, employeeDtos.Count());

                var employeeDtoList = employeeDtos.ToList();
                Assert.Equal(1, employeeDtoList[0].EmployeeId);
                Assert.Equal("Jhon", employeeDtoList[0].Name);
                Assert.Equal("Developer", employeeDtoList[0].Occupation);

                Assert.Equal(2, employeeDtoList[1].EmployeeId);
                Assert.Equal("Maria", employeeDtoList[1].Name);
                Assert.Equal("Desingner", employeeDtoList[1].Occupation);
            }
        }
    }
}
