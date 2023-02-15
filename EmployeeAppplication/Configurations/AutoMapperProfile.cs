using EmployeeApplication.Core.Domain.ResponseModel;
using EmployeeApplication.Infra.Domain.Entities;
using AutoMapper;
using EmployeeApplication.Shared;

namespace EmployeeAppplication.Configurations;

public class AutoMapperProfile:Profile
{
    public AutoMapperProfile()
    {
        CreateMap<PagedList<Employee>, PagedList<EmployeeResponseModel>>();
        CreateMap<Employee, EmployeeResponseModel>();
    }


}
