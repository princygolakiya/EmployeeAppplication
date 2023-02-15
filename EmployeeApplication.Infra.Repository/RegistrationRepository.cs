using EmployeeApplication.Infra.Contract;
using EmployeeApplication.Infra.Domain;
using EmployeeApplication.Infra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Infra.Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly EmployeeApplicationContext _employeeApplicationContext;
        public RegistrationRepository(EmployeeApplicationContext employeeApplicationContext)
        {
            _employeeApplicationContext=employeeApplicationContext;
        }
        public async Task<int> AddUser(User user)
        {
             await _employeeApplicationContext.users.AddAsync(user);
             return await _employeeApplicationContext.SaveChangesAsync();
                
        }

      

        public async Task<User?> LoginUser(string userName)
        {
           
            try
            {

               return await _employeeApplicationContext.users.FirstOrDefaultAsync(x=>x.UserName==userName);
              

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
