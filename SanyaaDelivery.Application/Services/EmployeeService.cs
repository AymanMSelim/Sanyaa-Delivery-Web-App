using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SanyaaDelivery.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly IRepository<DepartmentEmployeeT> employeeDepartmentRepository;
        private readonly IRepository<EmployeeWorkplacesT> employeeWorkplaceRepository;
        private readonly IRepository<EmploymentApplicationsT> employmentApplicationRepository;
        private readonly IRepository<FollowUpT> followUpRepository;

        public EmployeeService(IRepository<EmployeeT> employeeRepository, IRepository<DepartmentEmployeeT> employeeDepartmentRepository, 
            IRepository<EmployeeWorkplacesT> employeeWorkplaceRepository, IRepository<EmploymentApplicationsT> employmentApplicationRepository,
           IRepository<FollowUpT> followUpRepository)
        {
            this.employeeRepository = employeeRepository;
            this.employeeDepartmentRepository = employeeDepartmentRepository;
            this.employeeWorkplaceRepository = employeeWorkplaceRepository;
            this.employmentApplicationRepository = employmentApplicationRepository;
            this.followUpRepository = followUpRepository;
        }

        public Task<EmployeeT> Get(string id)
        {
            return employeeRepository
                 .Where(d => d.EmployeeId == id)
                 .Include(d => d.DepartmentEmployeeT)
                 .Include(d => d.EmployeeWorkplacesT)
                 .Include(d => d.EmployeeLocation)
                 .Include(d => d.LoginT)
                 .Include(d => d.Subscription)
                 .FirstOrDefaultAsync();
        }

        public Task<EmployeeT> GetWithBeancesAndTimetable(string id)
        {
            return employeeRepository
                .Where(d=> d.EmployeeId == id)
                .Include("EmployeeWorkplacesT")
                .Include("TimetableT")
                .Include("FiredStaffT")
                .FirstOrDefaultAsync();
        }

        public async Task<List<EmployeeT>> GetByDepartment(string departmentName)
        {
            return await employeeDepartmentRepository.Where(d => d.DepartmentName == departmentName).Select(d => d.Employee).ToListAsync();
        }

        public async Task<List<EmployeeT>> GetByDepartment(int departmentId)
        {
            return await employeeDepartmentRepository.Where(d => d.DepartmentId == departmentId).Select(d => d.Employee).ToListAsync();
        }

        public async Task<int> AddAsync(EmployeeT employee)
        {
            await employeeRepository.AddAsync(employee);
            return await employeeRepository.SaveAsync();
        }

        public EmployeeDto GetCustomInfo(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(EmployeeT employee)
        {
            employeeRepository.Update(employee.EmployeeId, employee);
            return employeeRepository.SaveAsync();
        }

        public async Task<int> AddDepartment(DepartmentEmployeeT departmentEmployee)
        {
            await employeeDepartmentRepository.AddAsync(departmentEmployee);
            return await employeeDepartmentRepository.SaveAsync();
        }

        public async Task<int> DeleteDepartment(int id)
        {
            await employeeDepartmentRepository.DeleteAsync(id);
            return await employeeDepartmentRepository.SaveAsync();
        }

        public async Task<int> AddWorkplace(EmployeeWorkplacesT employeeWorkplace)
        {
            await employeeWorkplaceRepository.AddAsync(employeeWorkplace);
            return await employeeWorkplaceRepository.SaveAsync();
        }

        public async Task<int> DeleteWorkplace(int id)
        {
            await employeeWorkplaceRepository.DeleteAsync(id);
            return await employeeWorkplaceRepository.SaveAsync();
        }

        public Task<List<DepartmentEmployeeT>> GetDepartmentList(string employeeId)
        {
            return employeeDepartmentRepository.Where(d => d.EmployeeId == employeeId).ToListAsync();
        }

        public Task<List<EmployeeWorkplacesT>> GetWorkplaceList(string employeeId)
        {
            return employeeWorkplaceRepository.Where(d => d.EmployeeId == employeeId).ToListAsync();
        }

        public Task<List<EmployeeT>> GetListAsync(int? departmentId, int? branchId, bool? getActive)
        {
            var query = employeeRepository.DbSet.AsQueryable();
            if (departmentId.HasValue)
            {
                query = query.Include(d => d.DepartmentEmployeeT).Where(d => d.DepartmentEmployeeT.Any(t => t.DepartmentId == departmentId.Value));
            }
            if (branchId.HasValue)
            {
                query = query.Include(d => d.EmployeeWorkplacesT).Where(d => d.EmployeeWorkplacesT.Any(t => t.BranchId == branchId.Value));
            }
            if (getActive.HasValue)
            {
                query = query.Include(d => d.LoginT).Where(d => d.LoginT.LoginAccountState == Convert.ToSByte(getActive));
            }
            return query.ToListAsync();
        }

        public Task<List<FollowUpT>> GetReviewListAsync(string employeeId)
        {
            return GetReviewListAsync(new List<string> { employeeId });
        }

        public Task<List<FollowUpT>> GetReviewListAsync(List<string> employeeIdList)
        {
            return followUpRepository
                .Where(d => employeeIdList.Contains(d.Request.EmployeeId))
                .Include(d => d.Request.Client)
                .ToListAsync();
        }

        public Task<List<EmployeeT>> GetFreeListAsync(int departmentId, int branchId, DateTime requestTime, bool? getActive, bool? getOnline)
        {
            throw new NotImplementedException();
        }
    }
}
