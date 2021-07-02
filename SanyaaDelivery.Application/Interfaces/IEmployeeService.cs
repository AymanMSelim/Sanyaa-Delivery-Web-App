using SanyaaDelivery.Application.ModelViews;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeT Get(string id);

        EmployeeModelView GetCustomInfo(string id);

        List<EmployeeT> GetByDepartment(string departmentName);
    }
}
