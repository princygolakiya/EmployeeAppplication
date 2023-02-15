using EmployeeApplication.Core.Domain.RequestModel;
using EmployeeApplication.Infra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Core.Builder
{
    public class UserRoleBuilder
    {
        public static UserRoleMapping Build(UserRoleRequestModel userRoleRequestModel)
        {
            return new UserRoleMapping(userRoleRequestModel.RoleId,userRoleRequestModel.UserId);
        }
    }
}
