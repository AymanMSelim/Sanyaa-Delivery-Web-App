using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IEmpDeptService
    {
        Task<int> AddAsync(DepartmentEmployeeT departmentEmployee);

        Task<int> DeleteAsync(int id);

        List<EmployeeT> GetEmployees(string department);

        List<EmployeeT> GetEmployees(int departmentId);
    }
}
