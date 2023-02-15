using AutoMapper;
using EmployeeApplication.Core.Builder;
using EmployeeApplication.Core.contract;
using EmployeeApplication.Core.Domain.Exception;
using EmployeeApplication.Core.Domain.Exceptions;
using EmployeeApplication.Core.Domain.RequestModel;
using EmployeeApplication.Core.Domain.ResponseModel;
using EmployeeApplication.Core.Service;
using EmployeeApplication.Core.Service.Helper;
using EmployeeApplication.Infra.Contract;
using EmployeeApplication.Infra.Domain;
using EmployeeApplication.Infra.Domain.Entities;
using EmployeeApplication.Infra.Repository;
using EmployeeApplication.Shared;
using EmployeeAppplication.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Moq;

namespace ServiceTest
{
    public class EmployeeServiceTest
    {
        private readonly Mock<IEmployeeRepository> _employeeRepository;
        private readonly MapperConfiguration config;
        private readonly IMapper mapper;
        private readonly Mock<IFileUpload> _fileUpload;
        private readonly EmployeeService employeeService;
        private readonly Mock<IFormFile> _formFile; 

        public EmployeeServiceTest()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            mapper = config.CreateMapper();
            _fileUpload = new Mock<IFileUpload>();
            _formFile = new Mock<IFormFile>();
            employeeService = new EmployeeService(_employeeRepository.Object, mapper, _fileUpload.Object);
        }

        [Fact]
        public async Task GetEmployee_MustPass()
        {
            PagedList<Employee> employeeList = new PagedList<Employee>
            {
                Items = EmployeeList(),
                CurrentPage = 1,
                TotalPage = 1,
                PageSize = 1,
                TotalCount = 1
            };

            _employeeRepository.Setup(repo => repo.GetEmployees(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(employeeList);

            var result = await employeeService.GetEmployeesAsync();

            Assert.NotNull(result);
        }

        [Fact]

        public async Task AddEmployee_MustPass()
        {

            EmployeeRequestModel employeeRequestModels = new EmployeeRequestModel()
            {
                Name = "Princy",
                DateOfBirth = DateTime.Now,
                Email = "p@gmail.com",
                Salary = 55000,
                DesignationId = 1,
            };

            _fileUpload.Setup(fileUp => fileUp.UploadCV(It.IsAny<IFormFile>())).ReturnsAsync("file Key");
            _employeeRepository.Setup(repo => repo.AddEmployee(It.IsAny<Employee>())).ReturnsAsync(1);
            employeeService.AddEmployeeAsync(employeeRequestModels);
        }

        [Fact]
        public async Task InsertEmployee_Fail()
        {

            EmployeeRequestModel employeeRequestModels = new EmployeeRequestModel()
            {
                Name = "Princy",
                DateOfBirth = DateTime.Now,
                Email = "p@gmail.com",
                Salary = 55000,
                DesignationId = 1,
            };


            _fileUpload.Setup(fileUp => fileUp.UploadCV(It.IsAny<IFormFile>())).ReturnsAsync("file Key");

            _employeeRepository.Setup(repo => repo.AddEmployee(It.IsAny<Employee>())).ReturnsAsync(0);

            await Assert.ThrowsAsync<BadRequestException>
                (async () => await employeeService.AddEmployeeAsync(employeeRequestModels));

        }

        [Fact]
        public async Task GetEmployeeByName_MustPass()
        {
            PagedList<Employee> employeeList = new PagedList<Employee>
            {
                Items = EmployeeList(),
                CurrentPage = 1,
                TotalPage = 1,
                PageSize = 1,
                TotalCount = 1
            };

            _employeeRepository.Setup(repo => repo.GetEmployeeByName(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(employeeList);

            var result = await employeeService.GetEmployeeAsync();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteEmployee_Success()
        {
            Employee employee = new Employee()
            {
                Name = "Princy",
                DateOfBirth = DateTime.Now,
                Email = "p@gmail.com",
                Salary = 55000,
                DesignationId = 1,
                Id = 1
            };

            _employeeRepository.Setup(repo => repo.GetEmployeeId(employee.Id)).ReturnsAsync(employee);
            _employeeRepository.Setup(x => x.UpdateEmployee(employee)).ReturnsAsync(1);
            await employeeService.DeleteEmployeeAsync(employee.Id);
        }

        [Fact]
        public async Task DeleteEmployee_ValidId()
        {
            _employeeRepository.Setup(repo => repo.GetEmployeeId(It.IsAny<int>())).ReturnsAsync(null as Employee);

            await Assert.ThrowsAsync<NotFoundException>
                (async () => await employeeService.DeleteEmployeeAsync(It.IsAny<int>()));
        }

        [Fact]
        public async Task DeleteEmployee_Fail()
        {

            Employee employee = new Employee()
            {
                Id = 1,
                Name = "Princy",
                DateOfBirth = DateTime.Now,
                Email = "p@gmail.com",
                Salary = 55000,
                DesignationId = 1

            };

            _employeeRepository.Setup(repo => repo.GetEmployeeId(employee.Id)).ReturnsAsync(employee);
            _employeeRepository.Setup(x => x.UpdateEmployee(employee)).ReturnsAsync(0);
            await Assert.ThrowsAsync<BadRequestException>
                (async () => await employeeService.DeleteEmployeeAsync(employee.Id));

        }
        [Fact]
        public async Task UpdateEmployee_Success()
        {
            Employee employee = new Employee()
            {
                Name = "Princy",
                DateOfBirth = DateTime.Now,
                Email = "p@gmail.com",
                Salary = 55000,
                DesignationId = 1,
                Id = 1
            };
            EmployeeRequestModel employeeRequestModel = new EmployeeRequestModel()
            {
                Name = "Princy",
                DateOfBirth = DateTime.Now,
                Email = "p@gmail.com",
                Salary = 55000,
                DesignationId = 1,

            };

            _employeeRepository.Setup(repo => repo.GetEmployeeId(employee.Id)).ReturnsAsync(employee);
            _employeeRepository.Setup(repo => repo.GetEmployeeById(employee.Id)).ReturnsAsync(EmployeeList());
            _fileUpload.Setup(fileUp => fileUp.UploadCV(It.IsAny<IFormFile>())).ReturnsAsync("file Key");
            _employeeRepository.Setup(x => x.UpdateEmployee(employee)).ReturnsAsync(1);
            await employeeService.UpdateEmployeeAsync(employeeRequestModel, employee.Id);

        }

        [Fact]
        public async Task UpdateEmployee_ValidId()
        {
            _employeeRepository.Setup(repo => repo.GetEmployeeId(It.IsAny<int>())).ReturnsAsync(null as Employee);

            await Assert.ThrowsAsync<NotFoundException>
                (async () => await employeeService.UpdateEmployeeAsync(It.IsAny<EmployeeRequestModel>(),It.IsAny<int>()));
        }

        [Fact]
        public async Task UpdateEmployee_Fail()
        {
            Employee employee = new Employee()
            {
                Name = "Princy",
                DateOfBirth = DateTime.Now,
                Email = "p@gmail.com",
                Salary = 55000,
                DesignationId = 1,
                Id = 1,
                CvFile="abc.jpg",
            };
            EmployeeRequestModel employeeRequestModel = new EmployeeRequestModel()
            {
                Name = "Princy",
                DateOfBirth = DateTime.Now,
                Email = "p@gmail.com",
                Salary = 55000,
                DesignationId = 1,
                CvFile=_formFile.Object,

            };

            _employeeRepository.Setup(repo => repo.GetEmployeeId(employee.Id)).ReturnsAsync(employee);
            _employeeRepository.Setup(repo => repo.GetEmployeeById(employee.Id)).ReturnsAsync(EmployeeList());
            _fileUpload.Setup(fileUp => fileUp.UploadCV(It.IsAny<IFormFile>())).ReturnsAsync("file Key");
            _employeeRepository.Setup(x => x.UpdateEmployee(It.IsAny<Employee>())).ReturnsAsync(0);
            await Assert.ThrowsAsync<BadRequestException>
                (async () => await employeeService.UpdateEmployeeAsync(employeeRequestModel, employee.Id));
        }

        [NonAction]
        private List<Employee> EmployeeList()
        {
            List<Employee> employees = new List<Employee>
        {
                new Employee
                {
                    Id = 1,
                    Name = "Princy",
                    DateOfBirth = DateTime.Now,
                    Email = "p@gmail.com",
                    Salary = 55000,
                    DesignationId = 1,
                    CvFile = "photo.jpg"
                },
                 new Employee
                {
                    Id = 2,
                    Name = "savan",
                    DateOfBirth = DateTime.Now,
                    Email = "s@gmail.com",
                    Salary = 55000,
                    DesignationId = 1,
                    CvFile = "photo.jpg"
                },
        };
            return employees;
        }

    }
}