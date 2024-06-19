using ClothingWorkshop.Application.DTO;
using ClothingWorkshop.Application.Interfaces;
using ClothingWorkshop.Domain.Entities;
using ClothingWorkshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClothingWorkshop.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    Occupation = e.Occupation
                }).ToListAsync();
        }

        public async Task AddEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                Name = employeeDto.Name,
                Occupation = employeeDto.Occupation
            };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }



        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var empleado = await _context.Employees.FindAsync(id);
            if (empleado == null) return null;

            return new EmployeeDto
            {
                EmployeeId = empleado.EmployeeId,
                Name = empleado.Name,
                Occupation = empleado.Occupation
            };
        }

        public async Task UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = await _context.Employees.FindAsync(employeeDto.EmployeeId);
            if (employee == null) return;

            employee.Name = employeeDto.Name;
            employee.Occupation = employeeDto.Occupation;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
