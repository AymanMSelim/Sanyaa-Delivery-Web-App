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

        Task<List<EmployeeT>> GetListAsync(int? departmentId, int? branchId, bool? getActive);

        Task<List<EmployeeT>> GetFreeListAsync(int departmentId, int branchId, DateTime requestTime, bool? getActive, bool? getOnline);

        Task<EmployeeT> GetWithBeancesAndTimetable(string id);

        EmployeeDto GetCustomInfo(string id);

        Task<List<EmployeeT>> GetByDepartment(string departmentName);

        Task<List<EmployeeT>> GetByDepartment(int departmentId);

        Task<int> AddAsync(EmployeeT employee);

        Task<int> AddDepartment(DepartmentEmployeeT departmentEmployee);

        Task<List<DepartmentEmployeeT>> GetDepartmentList(string employeeId);

        Task<int> DeleteDepartment(int id);

        Task<int> AddWorkplace(EmployeeWorkplacesT employeeWorkplace);

        Task<List<EmployeeWorkplacesT>> GetWorkplaceList(string employeeId);

        Task<int> DeleteWorkplace(int id);

        Task<int> UpdateAsync(EmployeeT employee);

        Task<List<FollowUpT>> GetReviewListAsync(string employeeId)
            ;
        Task<List<FollowUpT>> GetReviewListAsync(List<string> employeeIdList);
    }
}
