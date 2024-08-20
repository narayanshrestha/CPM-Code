using BlazorWasm.Client.Models;
using CPM.Models;

namespace BlazorWasm.Client.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeeAsync();
        Task<bool> AddEmployeeAsync(EmployeeModel employee);
    }
}
