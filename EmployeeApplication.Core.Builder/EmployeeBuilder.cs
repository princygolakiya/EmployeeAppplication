using EmployeeApplication.Core.Domain.RequestModel;
using EmployeeApplication.Infra.Domain.Entities;

namespace EmployeeApplication.Core.Builder
{
    public class EmployeeBuilder
    {
        public static Employee Build(EmployeeRequestModel model, string cvKey)
        {
            return new Employee(model.Name, model.DateOfBirth, model.Email, model.Salary, model.DesignationId,cvKey);
        }
    }
}