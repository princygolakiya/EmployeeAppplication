using EmployeeApplication.Core.Domain.Exception;
using EmployeeApplication.Core.Domain.Exceptions;
using EmployeeApplication.Core.Domain.RequestModel;
using EmployeeApplication.Core.Service;
using EmployeeApplication.Core.Service.Helper;
using EmployeeApplication.Infra.Contract;
using EmployeeApplication.Infra.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceTest
{
    public class UserServiceTest
    {
        private readonly Mock<IRegistrationRepository> _registrationRepository;
        private readonly RegistrationService registrationService;
        private readonly Mock<IGenrateToken> _genrateToken;

        public UserServiceTest()
        {
            _registrationRepository = new Mock<IRegistrationRepository>();
            _genrateToken = new Mock<IGenrateToken>();
            registrationService = new RegistrationService(_registrationRepository.Object,_genrateToken.Object);

        }

        [Fact]
        public async Task Login_Fail()
        {
            LoginModel loginModel = new LoginModel()
            {
                UserName = "princy123",  
                Password="princy123"
            };
            User user = new User()
            {
                UserName = "princy123",
            };

            _registrationRepository.Setup(x => x.LoginUser(It.IsAny<string>())).ReturnsAsync(null as User);
            await Assert.ThrowsAsync<NotFoundException>(async() =>await registrationService.LoginAsync(loginModel));
          

        }
        [Fact]
        public async Task Login_Success()
        {
            LoginModel loginModel = new LoginModel()
            {
                UserName = "princy123",
                Password = "princy123"
            };
            byte[] bytes = Encoding.ASCII.GetBytes("princy123");
            User user = new User()
            {
                UserName = "princy123",
                Password = bytes,
                PasswordKey = bytes,
            };
            string password = Encoding.ASCII.GetString(bytes);
             password = loginModel.Password;
            user.UserName = loginModel.UserName;

            _registrationRepository.Setup(x => x.LoginUser(It.IsAny<string>())).ReturnsAsync(user);
           // _genrateToken.Setup(x => x.TokenGenrate(It.IsAny<User>())).Returns();
            await Assert.ThrowsAsync<NotFoundException>(async () => await registrationService.LoginAsync(loginModel));
        }
    }
}
