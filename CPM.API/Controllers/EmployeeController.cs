
using CPM.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CPM.API.Controllers
{
    //TODO: Incomment this for authorization
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            await Task.Delay(2000);
            return Ok(await _employeeService.GetEmployees());
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Models.Employee employee)
        {
            await Task.Delay(2000);
            await _employeeService.AddEmployee(employee);
            return StatusCode(StatusCodes.Status201Created, employee);
        }
    }
}
