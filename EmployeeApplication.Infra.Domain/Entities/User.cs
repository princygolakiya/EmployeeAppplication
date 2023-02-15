using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Infra.Domain.Entities
{
    public class User
    {
        public User()
        {
        }

        public User(string userName, byte[] password, byte[] paswordKey)
        {
         
            UserName = userName;
            Password = password;
            PasswordKey = paswordKey;
           
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordKey { get; set; }

       
    }
}
