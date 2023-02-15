using EmployeeApplication.Core.Builder;
using EmployeeApplication.Core.Contract;
using EmployeeApplication.Core.Domain.Exception;
using EmployeeApplication.Core.Domain.RequestModel;
using EmployeeApplication.Infra.Contract;
using EmployeeApplication.Infra.Domain.Entities;
using EmployeeApplication.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Core.Service
{
    public class UserRoleService : IUserRoleService
    {

        private readonly IUserRoleRepository _userRoleRepository;
        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }
        public async Task AddUserRoleAsync(UserRoleRequestModel userRoleRequestModel)
        {
            try
			{
                var userRole = UserRoleBuilder.Build(userRoleRequestModel);
                var count = await _userRoleRepository.AddUserRole(userRole);

                if (count == 0)
                {
                    throw new BadRequestException("UserRole is not created succssfully.");
                }

            }
			catch (Exception)
			{

				throw;
			} 
        }
    }
}
