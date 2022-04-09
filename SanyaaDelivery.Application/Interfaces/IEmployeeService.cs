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

        Task<List<EmployeeT>> GetByDepartment(string departmentName);

        Task<List<EmployeeT>> GetByDepartment(int departmentId);

        Task<int> AddAsync(EmployeeT employee);

        Task<int> AddDepartment(DepartmentEmployeeT departmentEmployee);

        Task<int> DeleteDepartment(int id);

        Task<int> AddBranch(EmployeeWorkplacesT employeeWorkplace);

        Task<int> DeleteBranch(int id);

        Task<int> UpdateAsync(EmployeeT employee);
    }
}
