using SanyaaDelivery.Domain.DTOs;
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

        Task<EmployeeT> GetWithBeancesAndTimetable(string id);

        EmployeeDto GetCustomInfo(string id);

        List<EmployeeT> GetByDepartment(string departmentName);

        Task<int> Add(EmployeeT employee);
    }
}
