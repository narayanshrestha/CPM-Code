using CPM.Application;
using CPM.Infrastructure.DataAccess.Repositories;
using Moq;
using System.Reflection.Metadata.Ecma335;

namespace CPM.API.UnitTests.Services
{
    public class EmployeeServiceTests
    {

        private readonly EmployeeService _shipmentService;
        private readonly Mock<IRepositoryService> _repositoryService;

        public EmployeeServiceTests()
        {
            _repositoryService = new Mock<IRepositoryService>();
            _shipmentService = new EmployeeService(_repositoryService.Object);
        }


        [Fact]
        public async Task GetEmployees_ShouldReturn_AllEmployees()
        {
            //Arrange
            _repositoryService.Setup(x => x.GetEmployees()).ReturnsAsync(GetEmployees());

            //Act
            var result = await _shipmentService.GetEmployees();

            //Assert
            Assert.NotNull(result);
            _repositoryService.Verify(x => x.GetEmployees(), Times.Once);
        }

        private List<Models.Employee> GetEmployees()
        {
            return new List<Models.Employee> {
                new Models.Employee { Id = 1, Department = "IT", Name = "John Deo" },
                new Models.Employee { Id = 2, Department = "HR", Name = "Lorem Epsum" }
            };
        }
    }
}