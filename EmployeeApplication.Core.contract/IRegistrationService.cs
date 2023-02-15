using EmployeeApplication.Core.Domain.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Core.Contract
{
    public interface IRegistrationService
    {
        Task AddUserAsync(UserRequestModel userRequestModel);
        Task<string> LoginAsync(LoginModel loginModel);
    }
}
