using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeApplication.Core.Domain.Exception
{
    public class ErrorDetails:IOException
    {
        public int statusCode { get; set; }
        public string? message { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);

    }
}
