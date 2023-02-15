using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Core.Domain.ResponseModel;

public record EmployeeResponseModel

{
    public long Id { get; set; }

    public string Name { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Email { get; set; }

    public long Salary { get; set; }

    public string DesignationName { get; set; }

    public string CvFile { get; set; }
}
