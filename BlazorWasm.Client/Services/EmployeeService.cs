using BlazorWasm.Client.Models;
using CPM.Models;
using System.Net.Http.Json;

namespace BlazorWasm.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiEndPoint");
        }
        public async Task<bool> AddEmployeeAsync(EmployeeModel employee)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/employee", employee);
            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>("api/employee");
        }
    }
}
