using EmployeeApplication.Infra.Domain.Entities;
using EmployeeApplication.Infra.Contract;
using EmployeeApplication.Infra.Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using EmployeeApplication.Shared;
using System;

namespace EmployeeApplication.Infra.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeApplicationContext _context;
        public EmployeeRepository(EmployeeApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> AddEmployee(Employee employee)
        {
            await _context.employees.AddAsync(employee);
            return await _context.SaveChangesAsync();

        }

        public async Task<List<Employee?>> GetEmployeeById(long employeeId)
        {
            var employee=await _context.employees.Where(x => x.Id == employeeId).ToListAsync();
            return employee;
        }

        public Task<List<Employee>> GetEmployeeByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<Employee>> GetEmployeeByName(string name, int page = 1, int pageSize = 25)
        {
            try
            {
                //var employees = _context.employees.Include(e => e.designation).where(e => !e.isdeleted).orderbydescending(e => e.createdon);
                var employees = _context.employees.Include(e => e.Designation).Where(e => !e.IsDeleted).OrderByDescending(e => e.CreatedOn);
                if (!string.IsNullOrEmpty(name))
                {

                    employees = (IOrderedQueryable<Employee>)employees.Where(x => EF.Functions.Like(x.Name, $"%{name}%"));
                }
                var count = await employees.LongCountAsync();
                var pagedList = employees.ToPagedList(page, pageSize, count);

                return pagedList;

            }

            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Employee?> GetEmployeeId(long employeeId)
        {
            var employee = await _context.employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            return employee;
        }

        public async Task<PagedList<Employee>> GetEmployees(int page = 1, int pageSize = 25)
        {
            try
            {
                var employees = _context.employees.Include(e => e.Designation).AsQueryable();
                var count = await employees.LongCountAsync();
                var pagedList = employees.ToPagedList(page, pageSize, count);
                return pagedList;

            }
            catch (Exception)
            {


                throw;
            }
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            _context.employees.Update(employee);
            return await _context.SaveChangesAsync();
        }

    }
}