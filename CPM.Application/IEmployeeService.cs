using CPM.Models;

namespace CPM.Application
{
    public interface IEmployeeService
    {
        Task AddEmployee(Employee employee);
        Task<List<Employee>> GetEmployees();
    }
}
