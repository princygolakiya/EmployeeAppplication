using EmployeeApplication.Core.Contract;
using EmployeeApplication.Core.Domain.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAppplication.Controllers
{
    [Authorize]
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost("user-role")]
        public async Task<ActionResult> PostUserRole(UserRoleRequestModel userRoleRequestModel)
        {
            await _userRoleService.AddUserRoleAsync(userRoleRequestModel);
            return Created("UsertRole", null);

        }
    }
}
