using EmployeeApplication.Core.contract;
using EmployeeApplication.Core.Domain.RequestModel;
using EmployeeApplication.Infra.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAppplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpPost("/api/employee")]
        public async Task<ActionResult> PostEmployee([FromForm]EmployeeRequestModel employee)
        {
            await _employeeService.AddEmployeeAsync(employee);
            return Created("Employee",null);
        }
        [HttpGet("/api/employees")]
        public async Task<ActionResult> GetEmployee(int page = 1, int pageSize = 25)
        {
            var employees=await _employeeService.GetEmployeesAsync(page,pageSize);
            return Ok(employees);
        }
        [HttpGet("/api/search-employee")]
        public async Task<ActionResult> GetEmployeeByName(string name=null,int page = 1, int pageSize = 25)
        {
            var employees = await _employeeService.GetEmployeeAsync(name,page, pageSize);
            return Ok(employees);
        }
        [HttpDelete("/api/employee/{id}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }

        [HttpPut("/api/employee/{id}")]
        public async Task<IActionResult> UpdateEmployee([FromForm] EmployeeRequestModel employee,long id)
        {
            await _employeeService.UpdateEmployeeAsync(employee,id);
            return NoContent();
        }

    }
}
