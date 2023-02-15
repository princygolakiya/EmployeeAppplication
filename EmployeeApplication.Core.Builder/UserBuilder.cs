using EmployeeApplication.Core.Domain.RequestModel;
using EmployeeApplication.Infra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Core.Builder
{
    public  class UserBuilder
    {
        public static User Build(UserRequestModel userRequestModel,byte[] password, byte[] passwordKey)
        {
            return new User(userRequestModel.UserName, password,passwordKey);
        }
        
    }
}
