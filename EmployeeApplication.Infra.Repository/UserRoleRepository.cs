using EmployeeApplication.Infra.Contract;
using EmployeeApplication.Infra.Domain;
using EmployeeApplication.Infra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Infra.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly EmployeeApplicationContext _employeeApplicationContext;
        public UserRoleRepository(EmployeeApplicationContext employeeApplicationContext)
        {
            _employeeApplicationContext = employeeApplicationContext;
        }
        public async Task<int> AddUserRole(UserRoleMapping userRoleMapping)
        {
            await _employeeApplicationContext.userRoleMappings.AddAsync(userRoleMapping);
            return await _employeeApplicationContext.SaveChangesAsync();
        }
    }
}
