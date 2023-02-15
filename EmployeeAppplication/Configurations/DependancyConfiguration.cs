using EmployeeApplication.Core.Builder;
using EmployeeApplication.Core.contract;
using EmployeeApplication.Core.Contract;
using EmployeeApplication.Core.Service;
using EmployeeApplication.Core.Service.Helper;
using EmployeeApplication.Infra.Contract;
using EmployeeApplication.Infra.Repository;

namespace EmployeeAppplication.Configurations
{
    public static class DependancyConfiguration
    {
        public static void AddDependancy(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IRegistrationRepository, RegistrationRepository>();
            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddTransient<IUserRoleRepository, UserRoleRepository>();
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddTransient<IFileUpload,FileUpload>();
            services.AddTransient<IGenrateToken,GenrateToken>();
        }
    }
}
