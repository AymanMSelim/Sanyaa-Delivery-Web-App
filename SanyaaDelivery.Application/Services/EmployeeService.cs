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
using App.Global.DTOs;
using App.Global.DateTimeHelper;

namespace SanyaaDelivery.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly IRepository<DepartmentEmployeeT> employeeDepartmentRepository;
        private readonly IRepository<FiredStaffT> firedRepository;
        private readonly IRepository<DepartmentT> departmentRepository;
        private readonly IRepository<EmployeeWorkplacesT> employeeWorkplaceRepository;
        private readonly IRepository<EmploymentApplicationsT> employmentApplicationRepository;
        private readonly INotificatonService notificatonService;
        private readonly IRepository<FollowUpT> followUpRepository;
        private readonly IRepository<CityT> cityRepository;
        private readonly IHelperService helperService;

        public EmployeeService(IRepository<EmployeeT> employeeRepository, IRepository<DepartmentEmployeeT> employeeDepartmentRepository, IRepository<FiredStaffT> firedRepository,
            IRepository<DepartmentT> departmentRepository, IRepository<EmployeeWorkplacesT> employeeWorkplaceRepository,
            IRepository<EmploymentApplicationsT> employmentApplicationRepository, INotificatonService notificatonService,
           IRepository<FollowUpT> followUpRepository, IRepository<CityT> cityRepository, IHelperService helperService)
        {
            this.employeeRepository = employeeRepository;
            this.employeeDepartmentRepository = employeeDepartmentRepository;
            this.firedRepository = firedRepository;
            this.departmentRepository = departmentRepository;
            this.employeeWorkplaceRepository = employeeWorkplaceRepository;
            this.employmentApplicationRepository = employmentApplicationRepository;
            this.notificatonService = notificatonService;
            this.followUpRepository = followUpRepository;
            this.cityRepository = cityRepository;
            this.helperService = helperService;
        }

        public Task<EmployeeT> GetAsync(string id, bool includeWorkplace = false, bool includeDepartment = false, 
            bool includeLocation = false, bool includeLogin = false, bool includeSubscription = false, bool includeReview = false,
            bool inculdeReviewClient = false, bool includeFavourite = false, bool includeOpreation = false)
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
            if (includeOpreation)
            {
                query = query.Include(d => d.OpreationT);
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

        public async Task<Result<object>> DeleteWorkplace(int id)
        {
            var employeeId = await employeeWorkplaceRepository.Where(d => d.EmployeeWorkplaceId == id)
                .Select(d => d.EmployeeId)
                .FirstOrDefaultAsync();
            int count = await employeeWorkplaceRepository.DbSet.CountAsync(d => d.EmployeeId == employeeId);
            if(count <= 1)
            {
               return ResultFactory<object>.CreateErrorResponseMessageFD("This operation can't be done");
            }
            await employeeWorkplaceRepository.DeleteAsync(id);
            var affectedRows = await employeeWorkplaceRepository.SaveAsync();
            return ResultFactory<object>.CreateAffectedRowsResult(affectedRows);
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
                    DepartmentName = string.Join(", ", d.DepartmentEmployeeT.Select(t => t.DepartmentName).ToList()),
                    d.EmployeeImageUrl
                })
                .FirstOrDefaultAsync();
            model.EmployeeId = employee.EmployeeId;
            model.EmployeeName = employee.EmployeeName;
            if (!string.IsNullOrEmpty(employee.EmployeeImageUrl))
            {
                model.Image = helperService.GetHost() + employee.EmployeeImageUrl;
            }
            if (string.IsNullOrEmpty(employee.Title))
            {
                model.Title = employee.DepartmentName;
            }
            else
            {
                model.Title = employee.Title;
            }
            var dateBefore15Days = DateTime.Now.AddDays(-15);
            var reviewList = await followUpRepository.Where(d => d.Request.EmployeeId == employeeId && d.Timestamp < dateBefore15Days && d.Request.IsCompleted)
                .Select(d => new EmployeeReviewDto
                {
                    ClientId = d.Request.ClientId,
                    ClientName = d.Request.Client.ClientName,
                    Rate = d.Rate,
                    CreationTime = d.Timestamp,
                    RequestId = d.RequestId,
                    Review = ""//d.Review
                })
                .OrderByDescending(d => d.CreationTime)
                .Take(50)
                .ToListAsync();
            if(reviewList.HasItem())
            {
                reviewList.ForEach(d => d.Rate = d.Rate.GetValueOrDefault(0));
                model.Rate = reviewList.Sum(d => d.Rate.Value) / reviewList.Count;
            }
            model.ReviewList = reviewList;
            return model;
        }

        public Task<AppEmployeeAccountIndexDto> GetAppAccountIndex(string employeeId)
        {
            return employeeRepository.Where(d => d.EmployeeId == employeeId)
                .Select(d => new AppEmployeeAccountIndexDto
                {
                    EmployeeId = d.EmployeeId,
                    CreationTime = d.EmployeeHireDate.HasValue ? d.EmployeeHireDate.Value : DateTime.Now,
                    EmployeeImage = d.EmployeeImageUrl,
                    EmployeeName = d.EmployeeName,
                    EmployeePhone = d.EmployeePhone,
                    EmployeePhone1 = d.EmployeePhone1,
                    EmployeeDepartmenText = string.Join(", ", d.DepartmentEmployeeT.Select(t => t.Department.DepartmentName).ToList()),
                    EmployeeDepartmentList = d.DepartmentEmployeeT.Select(t => new EmployeeDepartmentDto
                    {
                        DepartmentId = t.DepartmentId.Value,
                        DepartmentName = t.Department.DepartmentName,
                        Id = t.DepartmentEmployeeId
                    }).ToList(),
                    EmployeeWorkplaceText = string.Join(", ", d.EmployeeWorkplacesT.Select(t => t.Branch.BranchName).ToList()),
                    EmployeeWorkplaceList = d.EmployeeWorkplacesT.Select(t => new EmployeeWorkplacesDto
                    {
                        Id = t.EmployeeWorkplaceId,
                        BranchId = t.BranchId,
                        BranchName = t.Branch.BranchName
                    }).ToList()

                }).FirstOrDefaultAsync();
        }

        public async Task<int> AddWorkplaceByCity(AddWorkplaceByCityDto model)
        {
            var branchId = await cityRepository.Where(d => d.CityId == model.CityId)
                .Select(d => d.BranchId)
                .FirstOrDefaultAsync();
            await employeeWorkplaceRepository.AddAsync(new EmployeeWorkplacesT
            {
                BranchId = branchId.Value,
                EmployeeId = model.EmployeeId
            });
            return await employeeWorkplaceRepository.SaveAsync();
        }

        public async Task<int> FireEmpolyeeAsync(FireEmployeeDto model)
        {
            var employee = await employeeRepository.Where(d => d.EmployeeId == model.EmployeeId)
                .Include(d => d.FiredStaffT)
                .FirstOrDefaultAsync();
            employee.IsFired = true;
            if (employee.FiredStaffT.IsNull()) {
                employee.FiredStaffT = new FiredStaffT
                {
                    EmployeeId = model.EmployeeId,
                    FiredDate = DateTime.Now.EgyptTimeNow(),
                    FiredReasons = model.Reason
                };
            }
            string title = "رفد موظف";
            string body =  $"لقد تم رفدك بسبب {model.Reason}";
            try { await notificatonService.SendFirebaseNotificationAsync(Domain.Enum.AccountType.Employee, model.EmployeeId, title, body); } catch { }
            return await employeeRepository.SaveAsync();
        }

        public async Task<int> ReturnEmployeeAsync(string employeeId)
        {
            var employee = await employeeRepository.Where(d => d.EmployeeId == employeeId)
                .FirstOrDefaultAsync();
            employee.IsFired = false;
            await firedRepository.DeleteAsync(employeeId);
            string title = "إعادة تعيين";
            string body = $"لقد تم اعادة تعيينك مرة أخرى";
            try { await notificatonService.SendFirebaseNotificationAsync(Domain.Enum.AccountType.Employee, employeeId, title, body); } catch { }
            return await employeeRepository.SaveAsync();
        }
    }
}
