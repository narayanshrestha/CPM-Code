using CPM.Infrastructure.DataAccess.Repositories;
using CPM.Models;

namespace CPM.Application
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryService _employeeRepository;
        public EmployeeService(IRepositoryService employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task AddEmployee(Employee employee)
        {
            await _employeeRepository.CreateAsync(employee);
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeRepository.GetEmployees();
        }

    }
}
