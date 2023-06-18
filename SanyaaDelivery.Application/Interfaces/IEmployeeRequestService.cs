using App.Global.DTOs;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IEmployeeRequestService
    {
        Task<Result<EmployeeT>> ValidateEmployeeForRequest(string employeeId, DateTime dateTime, int branchId, int? departmentId = null, int? requestId = null);
        Task<List<EmployeeT>> GetFreeEmployeeListByBranch(DateTime dateTime, int departmentId, int branchId, bool includeReview = false,
            bool includeClientWithReview = false, bool includeFavourite = false);
        Task<List<EmployeeT>> GetFreeEmployeeListCity(DateTime dateTime, int departmentId, int cityId);
        Task<List<EmployeeT>> GetFreeEmployeeListAddress(DateTime dateTime, int departmentId, int addressId);
        Task<List<string>> GetFreeEmployeeIdListAsync(DateTime dateTime, int departmentId, int branchId);
        Task<List<FreeEmployeeDto>> GetFreeEmployeeListAsync(DateTime dateTime, int departmentId, int branchId);
    }
}
