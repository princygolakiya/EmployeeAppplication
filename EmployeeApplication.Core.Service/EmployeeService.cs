using AutoMapper;
using EmployeeApplication.Core.Domain.Exceptions;
using EmployeeApplication.Core.Builder;
using EmployeeApplication.Core.contract;
using EmployeeApplication.Core.Domain.Exception;
using EmployeeApplication.Core.Domain.RequestModel;
using EmployeeApplication.Core.Domain.ResponseModel;
using EmployeeApplication.Infra.Contract;
using EmployeeApplication.Infra.Domain.Entities;
using EmployeeApplication.Shared;
using EmployeeApplication.Core.Service.Helper;
using System.Runtime.InteropServices;
using EmployeeApplication.Infra.Domain;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeApplication.Core.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IFileUpload _fileUpload;
        private readonly EmployeeApplicationContext _employeeApplicationContext;
     

        public EmployeeService(IEmployeeRepository employeerepository, IMapper mapper, IFileUpload fileUpload)
        {
            _employeeRepository = employeerepository;
            _mapper = mapper;
            _fileUpload = fileUpload;
         
         
        }
        public async Task AddEmployeeAsync(EmployeeRequestModel employeeModel)
        {
            try
            {
                var fileKey = await _fileUpload.UploadCV(employeeModel.CvFile);
                var employee = EmployeeBuilder.Build(employeeModel, fileKey);
                var count = await _employeeRepository.AddEmployee(employee);

                if (count == 0)
                {
                    throw new BadRequestException("Employee is not created succssfully.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteEmployeeAsync(long employeeId)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeId(employeeId);
                if (employee == null)
                {
                    throw new NotFoundException($"{employeeId} is not found");
                }
                employee.Delete();
                var count = await _employeeRepository.UpdateEmployee(employee);
                if (count == 0)
                {
                    throw new BadRequestException("Employee is not updated successfully");
                }

            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<PagedList<EmployeeResponseModel>> GetEmployeeAsync(string name = null, int page = 1, int pageSize = 25)
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeeByName(name, page, pageSize);
                var result = _mapper.Map<PagedList<EmployeeResponseModel>>(employees);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<PagedList<EmployeeResponseModel>> GetEmployeesAsync(int page = 1, int pageSize = 25)
        {
            try
            {
                var employees = await _employeeRepository.GetEmployees(page, pageSize);
                var result = _mapper.Map<PagedList<EmployeeResponseModel>>(employees);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task UpdateEmployeeAsync(EmployeeRequestModel employeeModel, long employeeId)
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeeId(employeeId);


                if (employees == null)
                {
                    throw new NotFoundException($"{employeeId} is not found");
                }
                var data = await _employeeRepository.GetEmployeeById(employeeId);
                foreach (var item in data)
                {
                    var photo = item.CvFile;
                    File.Delete(photo);
                }
                var fileKey = await _fileUpload.UploadCV(employeeModel.CvFile);
                employees.Update(employeeModel.Name, employeeModel.DateOfBirth, employeeModel.Email, employeeModel.Salary, employeeModel.DesignationId, fileKey);
                var count = await _employeeRepository.UpdateEmployee(employees);
                if (count == 0)
                {
                    throw new BadRequestException("Employee is not updated successfully");
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}