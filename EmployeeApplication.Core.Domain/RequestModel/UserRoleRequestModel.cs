using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Core.Domain.RequestModel
{
    public class UserRoleRequestModel
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
