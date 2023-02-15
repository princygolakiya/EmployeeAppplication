using EmployeeApplication.Core.Contract;
using EmployeeApplication.Core.Domain.RequestModel;
using EmployeeApplication.Core.Service;
using EmployeeApplication.Infra.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeAppplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRegistrationService   _registrationService;
        private readonly EmployeeApplicationContext _employeeApplicationContext;
        public UserController(IRegistrationService registrationService,EmployeeApplicationContext employeeApplicationContext)
        {
            _registrationService = registrationService;
            _employeeApplicationContext = employeeApplicationContext;
        }


        [HttpPost("register")]
        public async Task<ActionResult> PostUser(UserRequestModel userRequestModel)
        {
            await _registrationService.AddUserAsync(userRequestModel);
            return Created("User", null);
        }
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(LoginModel loginModel)
        {
            var data=await _registrationService.LoginAsync(loginModel);
            return Ok(data);
        }
    }
}
