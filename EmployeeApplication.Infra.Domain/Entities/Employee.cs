



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Infra.Domain.Entities
{
    public class Employee : Audit
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public long Salary { get; set; }
        public long DesignationId { get; set; }

        public string CvFile { get; set; }
        public virtual Designation Designation { get; set; }

        public Employee()
        {
        }

        public Employee(string name, DateTime dateOfBirth, string email, long salary, long designationId, string cvFile)
        {

            Name = name;
            DateOfBirth = dateOfBirth;
            Email = email;
            Salary = salary;
            DesignationId = designationId;
            CvFile=cvFile;
            CreatedOn = DateTime.UtcNow;
            IsDeleted = false;
        }



        public Employee Delete()
        {
            UpdatedOn = DateTime.UtcNow;
            IsDeleted = true;
            return this;
        }
        public Employee Update(string name, DateTime dateOfBirth, string email, long salary, long designationId,string cvFile)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Email = email;
            Salary = salary;
            DesignationId = designationId;
            CvFile= cvFile ;
            UpdatedOn = DateTime.UtcNow;

            return this;
        }
    }
}
