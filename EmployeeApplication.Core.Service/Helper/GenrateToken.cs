using EmployeeApplication.Infra.Domain;
using EmployeeApplication.Infra.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Core.Service.Helper
{
    public class GenrateToken:IGenrateToken
    {
       // private readonly EmployeeApplicationContext _dbContext;
        private readonly IConfiguration _config;
        public GenrateToken(IConfiguration config)
        {
           // _dbContext = dbContext;
            _config = config;
        }
        public async Task<string> TokenGenrate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName)            

            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(90),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
