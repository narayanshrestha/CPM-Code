using CPM.Models;

namespace CPM.Infrastructure.DataAccess.Repositories
{
    public interface IRepositoryService
    {
        Task<List<Employee>> GetEmployees();
        Task CreateAsync(Employee employee);
    }
}
