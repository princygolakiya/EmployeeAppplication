using EmployeeApplication.Core.Domain.RequestModel;
using EmployeeApplication.Core.Domain.ResponseModel;
using EmployeeApplication.Shared;

namespace EmployeeApplication.Core.contract
{
    public interface IEmployeeService
    {
        Task AddEmployeeAsync(EmployeeRequestModel employee);
        Task<PagedList<EmployeeResponseModel?>> GetEmployeesAsync(int page = 1, int pageSize = 25);
        Task DeleteEmployeeAsync(long employeeId);
        Task UpdateEmployeeAsync(EmployeeRequestModel employee,long employeeId);
        Task<PagedList<EmployeeResponseModel>> GetEmployeeAsync(string name = null, int page = 1, int pageSize = 25);

    }
}