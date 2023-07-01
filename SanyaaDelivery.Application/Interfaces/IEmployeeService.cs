using App.Global.DTOs;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Domain.OtherModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeT> GetAsync(string id, bool includeWorkplace = false, bool includeDepartment = false,
            bool includeLocation = false, bool includeLogin = false, bool includeSubscription = false,
            bool includeReview = false, bool includeReviewClient = false, bool includeFavourite = false, bool includeOperation = false);

        Task<List<EmployeeT>> GetListAsync(int? departmentId, int? branchId, bool? getActive = null, bool includeReview = false, 
            bool includeClientWithReview = false, bool includeFavourite = false);

        Task<List<EmployeeT>> GetFreeListAsync(int departmentId, int branchId, DateTime requestTime, bool? getActive, bool? getOnline);

        Task<decimal> GetEmployeePrecentageAsync(string employeeId, int departmentId);

        Task<EmployeeT> GetWithBeancesAndTimetable(string id);

        Task<List<EmployeeDto>> GetCustomListAsync(string id = null, string name = null, string phone = null, int? departmentId = null, int? branchId = null, bool? isNewEmployee = null);
        Task<decimal> GetDepartmentPrecentageAsync(string employeeId, int departmentId);
        Task<bool> IsCleaningEmployeeAsync(string employeeId);

        Task<List<EmployeeT>> GetByDepartment(string departmentName);

        Task<List<EmployeeT>> GetByDepartment(int departmentId);

        Task<int> AddAsync(EmployeeT employee);

        Task<int> AddDepartment(DepartmentEmployeeT departmentEmployee);

        Task<List<DepartmentEmployeeT>> GetDepartmentList(string employeeId);

        Task<int> DeleteDepartment(int id);

        Task<int> AddWorkplace(EmployeeWorkplacesT employeeWorkplace);

        Task<List<EmployeeWorkplacesT>> GetWorkplaceList(string employeeId);
        Task<AppEmployeeAccountIndexDto> GetAppAccountIndex(string employeeId);
        Task<int> AddWorkplaceByCity(AddWorkplaceByCityDto mode);

        Task<Result<object>> DeleteWorkplace(int id);

        Task<int> UpdateAsync(EmployeeT employee);

        Task<List<FollowUpT>> GetReviewListAsync(string employeeId);
        Task<List<FollowUpT>> GetReviewListAsync(List<string> employeeIdList);
        Task<AppReviewIndexDto> GetAppReviewIndexAsync(string employeeId);
        Task<bool> IsThisEmployeeExist(string natioalNumber);

        Task<int> ReturnEmployeeAsync(string employeeId);

        Task<int> FireEmpolyeeAsync(FireEmployeeDto model);
    }
}
