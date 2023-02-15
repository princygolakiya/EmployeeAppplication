using System.Runtime.CompilerServices;
using EmployeeApplication.Infra.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAppplication.Configurations
{
    public static class SqlServerConfiguration
    {
      
        public static void AddSqlServer(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:Default"];

            services.AddDbContext<EmployeeApplicationContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(connectionString, x =>
                {
                    x.MigrationsAssembly("EmployeeApplication.Infra.Domain");
                    x.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                });
            }, ServiceLifetime.Singleton);
        }       
    }
}
