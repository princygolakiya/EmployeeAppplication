using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Core.Domain.RequestModel
{
    public record EmployeeRequestModel
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public long Salary { get; set; }

        public long DesignationId { get; set; }
        public IFormFile CvFile { get; set; }

    }
}
