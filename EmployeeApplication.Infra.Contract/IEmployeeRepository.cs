using EmployeeApplication.Infra.Domain.Entities;
using EmployeeApplication.Shared;

namespace EmployeeApplication.Infra.Contract
{
    public interface IEmployeeRepository
    {
        Task<PagedList<Employee?>> GetEmployees(int page = 1, int pageSize = 25);
        Task<int> AddEmployee(Employee employee);
        Task<int> UpdateEmployee(Employee employee);
        Task<Employee?> GetEmployeeId(long employeeId);

        Task<PagedList<Employee>> GetEmployeeByName(string name=null, int page = 1, int pageSize = 25);
        Task<List<Employee?>> GetEmployeeById(long employeeId);
    }

   
}


