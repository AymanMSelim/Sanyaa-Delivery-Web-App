using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IEmpDeptService
    {
        List<EmployeeT> GetEmployees(string department);
    }
}
