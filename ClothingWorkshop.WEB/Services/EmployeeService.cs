using ClothingWorkshop.WEB.Models;
using System.Net.Http.Json;

namespace ClothingWorkshop.WEB.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>("employees");
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeDto>($"employees/{id}");
        }

        public async Task AddEmployeeAsync(EmployeeDto employee)
        {
            await _httpClient.PostAsJsonAsync("employees", employee);
        }

        public async Task UpdateEmployeeAsync(EmployeeDto employee)
        {
            await _httpClient.PutAsJsonAsync($"employees/{employee.EmployeeId}", employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _httpClient.DeleteAsync($"employees/{id}");
        }
    }
}
