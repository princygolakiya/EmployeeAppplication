using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Infra.Domain.Entities
{
    public class Designation
    {
        public Designation()
        {
        }

        public Designation(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id  { get; set; }

        [StringLength(55)]
        public string Name { get; set; }
         
        
       
    }
}
