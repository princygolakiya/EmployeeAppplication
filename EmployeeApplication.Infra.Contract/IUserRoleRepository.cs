using EmployeeApplication.Infra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Infra.Contract
{
    public interface IUserRoleRepository
    {
        Task<int> AddUserRole(UserRoleMapping userRoleMapping);
    }
}
