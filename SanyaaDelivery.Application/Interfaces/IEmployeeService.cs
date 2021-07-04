using SanyaaDelivery.Application.DTO;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeT> Get(string id);

        EmployeeDto GetCustomInfo(string id);

        List<EmployeeT> GetByDepartment(string departmentName);
    }
}
