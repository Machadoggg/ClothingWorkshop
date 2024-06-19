using ClothingWorkshop.Application.DTO;
using ClothingWorkshop.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingWorkshop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _empleoyeeService;

        public EmployeesController(IEmployeeService empleoyeeService)
        {
            _empleoyeeService = empleoyeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var employee = await _empleoyeeService.GetAllEmployeesAsync();
            return Ok(employee);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _empleoyeeService.GetEmployeeByIdAsync(id);

            if (employee == null) return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeDto employeeDto)
        {
            await _empleoyeeService.AddEmployeeAsync(employeeDto);
            return CreatedAtAction(nameof(Get), new { id = employeeDto.EmployeeId }, employeeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (id != employeeDto.EmployeeId) return BadRequest();
            await _empleoyeeService.UpdateEmployeeAsync(employeeDto);
            return Ok(employeeDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _empleoyeeService.DeleteEmployeeAsync(id);
            return Ok();
        }
    }
}
