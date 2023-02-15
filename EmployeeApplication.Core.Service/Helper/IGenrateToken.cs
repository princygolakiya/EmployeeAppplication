using EmployeeApplication.Infra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Core.Service.Helper
{
    public interface IGenrateToken
    {
        Task<string> TokenGenrate(User user);
    }
}
