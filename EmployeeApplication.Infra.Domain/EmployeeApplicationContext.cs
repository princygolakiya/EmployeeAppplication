using EmployeeApplication.Infra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace EmployeeApplication.Infra.Domain
{
    public class EmployeeApplicationContext : DbContext
    {
        public EmployeeApplicationContext(DbContextOptions<EmployeeApplicationContext> options) : base(options)
            {
            }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Designation> designations { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<UserRoleMapping> userRoleMappings { get; set; }
        public DbSet<Role> roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Designation>().HasData(new List<Designation> {
            new Designation(1,"HR"),
            new Designation(2,"ReactJS Developer"),
            new Designation(3,"Software Developer")
            });

        }
    }
}