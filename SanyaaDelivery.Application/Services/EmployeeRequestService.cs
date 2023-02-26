using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class EmployeeRequestService : IEmployeeRequestService
    {
        private readonly IRepository<RequestT> requestRepository;
        private readonly IRepository<DepartmentEmployeeT> employeeDepartmentRepository;
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly IRepository<CityT> cityRepository;
        private readonly IRepository<AddressT> addressRepository;

        public EmployeeRequestService(IRepository<RequestT> requestRepository, 
            IRepository<DepartmentEmployeeT> employeeDepartmentRepository, IRepository<EmployeeT> employeeRepository,
            IRepository<CityT> cityRepository, IRepository<AddressT> addressRepository)
        {
            this.requestRepository = requestRepository;
            this.employeeDepartmentRepository = employeeDepartmentRepository;
            this.employeeRepository = employeeRepository;
            this.cityRepository = cityRepository;
            this.addressRepository = addressRepository;
        }


        private void GetStartAndEndTime(DateTime dateTime, int departmentId, out DateTime startTime, out DateTime endTime)
        {
            if (departmentId == GeneralSetting.CleaningDepartmentId)
            {
                startTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 1);
                endTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 57);
            }
            else
            {
                startTime = dateTime.AddHours(-GeneralSetting.RequestExcutionHours);
                endTime = dateTime.AddHours(GeneralSetting.RequestExcutionHours);
            }
        }

        private IQueryable<EmployeeT> GetFreeEmployeeConditionQuery(DateTime dateTime, int? departmentId = null, int? branchId = null, 
            bool getActiveOnly = true, bool getOnlineOnly = false)
        {
            var query = employeeRepository.DbSet.AsQueryable();
            if (branchId.HasValue)
            {
                query = query.Where(d => d.EmployeeWorkplacesT.Any(w => w.BranchId == branchId));
            }
            if (departmentId.HasValue)
            {
                query = query.Where(d => d.DepartmentEmployeeT.Any(t => t.DepartmentId == departmentId));
            }
            if (getActiveOnly)
            {
                query = query.Where(d => d.LoginT.LoginAccountState.Value);
            }
            if (getOnlineOnly)
            {
                query = query.Where(d => d.LoginT.LastActiveTimestamp >= DateTime.Now.AddMinutes(-3));
            }
            query = query.Where(d => d.FiredStaffT == null);
            query = query.Where(d => d.VacationT.Count(t => t.Day.Year == dateTime.Year && t.Day.Month == dateTime.Month && t.Day.Day == dateTime.Day) == 0);
            return query;
        }

        public async Task<List<EmployeeT>> GetFreeEmployeeListAddress(DateTime dateTime, int departmentId, int addressId)
        {
            var city = await addressRepository.Where(d => d.CityId == d.CityId).Select(d => d.City).FirstOrDefaultAsync();
            return await GetFreeEmployeeListByBranch(dateTime, departmentId, city.BranchId.GetValueOrDefault(0));
        }

        public Task<List<EmployeeT>> GetFreeEmployeeListByBranch(DateTime dateTime, int departmentId, int branchId, bool includeReview = false,
            bool includeClientWithReview = false, bool includeFavourite = false)
        {
            GetStartAndEndTime(dateTime, departmentId, out DateTime startTime, out DateTime endTime);
            var query = GetFreeEmployeeConditionQuery(dateTime, departmentId, branchId);    
            query = query.Where(d => d.RequestT.Count(t => t.RequestTimestamp > startTime && t.RequestTimestamp < endTime && t.IsCanceled == false) == 0);
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

        public async Task<List<EmployeeT>> GetFreeEmployeeListCity(DateTime dateTime, int departmentId, int cityId)
        {
            var city = await cityRepository.Where(d => d.CityId == d.CityId).FirstOrDefaultAsync();
            return await GetFreeEmployeeListByBranch(dateTime, departmentId, city.BranchId.GetValueOrDefault(0));
        }

        public async Task<bool> IsThisEmployeeFree(string employeeId, DateTime dateTime)
        {
            if (string.IsNullOrEmpty(employeeId))
            {
                return true;
            }
            DateTime startTime;
            DateTime endTime;
            var departmentList = await employeeDepartmentRepository.Where(d => d.EmployeeId == employeeId)
                .ToListAsync();
            if(departmentList.Any(d => d.DepartmentId == GeneralSetting.CleaningDepartmentId))
            {
                startTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 1);
                endTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 57);
            }
            else
            {
                startTime = dateTime.AddHours(-GeneralSetting.RequestExcutionHours);
                endTime = dateTime.AddHours(GeneralSetting.RequestExcutionHours);
            }
            var query = GetFreeEmployeeConditionQuery(dateTime, null, null);
            query = query.Where(d => d.EmployeeId == employeeId);
            query = query.Where(d => d.RequestT.Count(t => t.RequestTimestamp > startTime && t.RequestTimestamp < endTime && t.IsCanceled == false) == 0);
            var requestCount = await query.CountAsync();
            return requestCount > 0;
        }
    }
}
