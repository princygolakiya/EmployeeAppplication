using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApplication.Core.Domain.Exceptions;

public class NotFoundException : RankException
{
    public NotFoundException(string message) : base(message) { }



}

