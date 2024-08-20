
using CPM.Infrastructure.DataAccess.Context;
using CPM.Models;
using Microsoft.EntityFrameworkCore;

namespace CPM.Infrastructure.DataAccess.Repositories
{
    public class EmployeeRepositoryService : IRepositoryService
    {
        private readonly EmployeeContext _employeeContext;
        public EmployeeRepositoryService(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeContext.Employees.ToListAsync();
        }

        public async Task CreateAsync(Employee emp)
        {
            await _employeeContext.Employees.AddAsync(emp);
            await _employeeContext.SaveChangesAsync();
        }

    }
}
