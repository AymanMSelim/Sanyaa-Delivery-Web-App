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
using App.Global.ExtensionMethods;
using SanyaaDelivery.Domain.OtherModels;

namespace SanyaaDelivery.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly IRepository<DepartmentEmployeeT> employeeDepartmentRepository;
        private readonly IRepository<DepartmentT> departmentRepository;
        private readonly IRepository<EmployeeWorkplacesT> employeeWorkplaceRepository;
        private readonly IRepository<EmploymentApplicationsT> employmentApplicationRepository;
        private readonly IRepository<FollowUpT> followUpRepository;
        private readonly IRepository<DepartmentT> departmentRepository1;

        public EmployeeService(IRepository<EmployeeT> employeeRepository, IRepository<DepartmentEmployeeT> employeeDepartmentRepository, 
            IRepository<DepartmentT> departmentRepository, IRepository<EmployeeWorkplacesT> employeeWorkplaceRepository, IRepository<EmploymentApplicationsT> employmentApplicationRepository,
           IRepository<FollowUpT> followUpRepository)
        {
            this.employeeRepository = employeeRepository;
            this.employeeDepartmentRepository = employeeDepartmentRepository;
            this.departmentRepository = departmentRepository;
            this.employeeWorkplaceRepository = employeeWorkplaceRepository;
            this.employmentApplicationRepository = employmentApplicationRepository;
            this.followUpRepository = followUpRepository;
        }

        public Task<EmployeeT> GetAsync(string id, bool includeWorkplace = false, bool includeDepartment = false, 
            bool includeLocation = false, bool includeLogin = false, bool includeSubscription = false, bool includeReview = false,
            bool inculdeReviewClient = false, bool includeFavourite = false)
        {
            var query = employeeRepository.Where(d => d.EmployeeId == id);
            if (includeDepartment)
            {
                query = query.Include(d => d.DepartmentEmployeeT);
            }
            if (includeLocation)
            {
                query = query.Include(d => d.EmployeeLocation);
            }
            if (includeLogin)
            {
                query = query.Include(d => d.LoginT);
            }
            if (includeSubscription)
            {
                query = query.Include(d => d.Subscription);
            }
            if (includeWorkplace)
            {
                query = query.Include(d => d.EmployeeWorkplacesT);
            }
            if (includeReview)
            {
                query = query.Include(d => d.EmployeeReviewT);
            }
            if (includeReview)
            {
                if (inculdeReviewClient)
                {
                    query = query.Include(d => d.EmployeeReviewT).ThenInclude(d => d.Client);
                }
                else
                {
                    query = query.Include(d => d.EmployeeReviewT);
                }
            }
            if (includeFavourite)
            {
                query = query.Include(d => d.FavouriteEmployeeT);
            }
            return query.FirstOrDefaultAsync();
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

        public Task<List<EmployeeT>> GetListAsync(int? departmentId, int? branchId, bool? getActive = null, bool includeReview = false,
            bool includeClientWithReview = false, bool includeFavourite = false)
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
                query = query.Include(d => d.LoginT).Where(d => d.LoginT.LoginAccountState == getActive);
            }
            if (includeReview)
            {
                if (includeClientWithReview)
                {
                    query = query.Include(d => d.EmployeeReviewT).ThenInclude(d => d.Client);
                }
                else
                {
                    query = query.Include(d => d.EmployeeReviewT);
                }
            }
            if (includeFavourite)
            {
                query = query.Include(d => d.FavouriteEmployeeT);
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

        public async Task<decimal> GetEmployeePrecentageAsync(string employeeId, int departmentId)
        {
            var departmentPercentage = await departmentRepository.Where(d => d.DepartmentId == departmentId).Select(d => d.DepartmentPercentage).FirstOrDefaultAsync();
            var employeeDepartmentPercentage = await employeeDepartmentRepository
                .Where(d => d.DepartmentId == departmentId && d.EmployeeId == employeeId).Select(d => d.Percentage)
                .FirstOrDefaultAsync();
            if (employeeDepartmentPercentage.HasValue)
            {
                return employeeDepartmentPercentage.Value;
            }
            if (departmentPercentage.HasValue)
            {
                return departmentPercentage.Value;
            }
            return GeneralSetting.EmployeePercentage;
        }

        public async Task<decimal> GetDepartmentPrecentageAsync(string employeeId, int departmentId)
        {
            decimal percentage = GeneralSetting.EmployeePercentage;
            var department = await departmentRepository.GetAsync(departmentId);
            if (department.IsNotNull() && department.DepartmentPercentage.HasValue)
            {
                percentage = department.DepartmentPercentage.Value;
            }
            if (string.IsNullOrEmpty(employeeId))
            {
                return percentage;
            }

            var employee = await GetAsync(employeeId, includeDepartment: true);
            if (employee.IsNull())
            {
                return percentage;
            }

            var requestDepartment = employee.DepartmentEmployeeT.FirstOrDefault(d => d.DepartmentId == departmentId);
            if (requestDepartment.IsNotNull() && requestDepartment.Percentage.GetValueOrDefault() > 0)
            {
                percentage = requestDepartment.Percentage.Value;
            }
            return percentage;       
        }

        public Task<bool> IsCleaningEmployeeAsync(string employeeId)
        {
            return employeeDepartmentRepository.Where(d => d.EmployeeId == employeeId && d.DepartmentId == GeneralSetting.CleaningDepartmentId).AnyAsync();
        }

        public Task<bool> IsThisEmployeeExist(string natioalNumber)
        {
            return employeeRepository.Where(d => d.EmployeeId == natioalNumber)
                .AnyAsync();
        }

        public async Task<AppReviewIndexDto> GetAppReviewIndexAsync(string employeeId)
        {
            AppReviewIndexDto model = new AppReviewIndexDto();
            var employee = await employeeRepository.Where(d => d.EmployeeId == employeeId)
                .Select(d => new 
                { 
                    d.EmployeeId,
                    d.EmployeeName,
                    d.Title,
                    d.DepartmentEmployeeT.FirstOrDefault().DepartmentName,
                    d.EmployeeImageUrl
                })
                .FirstOrDefaultAsync();
            model.EmployeeId = employee.EmployeeId;
            model.EmployeeName = employee.EmployeeName;
            model.Image = employee.EmployeeImageUrl;
            if (string.IsNullOrEmpty(employee.Title))
            {
                model.Title = employee.DepartmentName;
            }
            else
            {
                model.Title = employee.Title;
            }
            var reviewList = await followUpRepository.Where(d => d.Request.EmployeeId == employeeId)
                .Select(d => new EmployeeReviewDto
                {
                    ClientId = d.Request.ClientId,
                    ClientName = d.Request.Client.ClientName,
                    Rate = d.Rate.Value,
                    CreationTime = d.Timestamp,
                    RequestId = d.RequestId,
                    Review = d.Review
                }).ToListAsync();
            model.Rate = reviewList.Sum(d => d.Rate) / reviewList.Count;
            model.ReviewList = reviewList;
            return model;
        }
    }
}
