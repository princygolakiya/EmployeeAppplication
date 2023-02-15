using EmployeeApplication.Core.Builder;
using EmployeeApplication.Core.Contract;
using EmployeeApplication.Core.Domain.Exception;
using EmployeeApplication.Core.Domain.Exceptions;
using EmployeeApplication.Core.Domain.RequestModel;
using EmployeeApplication.Core.Service.Helper;
using EmployeeApplication.Infra.Contract;
using EmployeeApplication.Infra.Domain;
using EmployeeApplication.Infra.Domain.Entities;
using EmployeeApplication.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Core.Service
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IGenrateToken _genrateToken;
        
        public RegistrationService(IRegistrationRepository registrationRepository, IGenrateToken genrateToken)
        {
            _registrationRepository = registrationRepository;
            _genrateToken = genrateToken;
        }
        public async Task AddUserAsync(UserRequestModel userRequestModel)
        {
            try
            {
                var hmc = new HMACSHA512();
                var password = hmc.ComputeHash(Encoding.ASCII.GetBytes(userRequestModel.Password));
                var passwordKey = hmc.Key;
                var user = UserBuilder.Build(userRequestModel, password,passwordKey);
                var count =await  _registrationRepository.AddUser(user);
                if (count == 0)
                {
                    throw new BadRequestException("User not Created succefully");
                }
            }
            catch (Exception)
            {

                throw;
            }           
        }

        public async Task<string> LoginAsync(LoginModel loginModel)
        {
            try
            {
                var user = await _registrationRepository.LoginUser(loginModel.UserName);

                if (user == null)
                {
                    throw new NotFoundException("User does not exist");
                }               
                var hmc = new HMACSHA512(user.PasswordKey);
                var computePassword = hmc.ComputeHash(Encoding.ASCII.GetBytes(loginModel.Password));
                user.Password = computePassword;
                user.UserName = loginModel.UserName;
                
                var passwordCompare = computePassword.SequenceEqual(user.Password);
                if (passwordCompare)
                { 
                    var token = await _genrateToken.TokenGenrate(user);
                    return token;

                }
                else
                {
                    throw new  BadRequestException("not found");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
