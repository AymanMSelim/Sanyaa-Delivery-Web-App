using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data.Context;
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
        private readonly IRepository<VacationT> vacationRepository;
        private readonly SanyaaDatabaseContext dbContext;

        public EmployeeRequestService(IRepository<RequestT> requestRepository, 
            IRepository<DepartmentEmployeeT> employeeDepartmentRepository, IRepository<EmployeeT> employeeRepository,
            IRepository<CityT> cityRepository, IRepository<AddressT> addressRepository, IRepository<VacationT> vacationRepository, SanyaaDatabaseContext dbContext)
        {
            this.requestRepository = requestRepository;
            this.employeeDepartmentRepository = employeeDepartmentRepository;
            this.employeeRepository = employeeRepository;
            this.cityRepository = cityRepository;
            this.addressRepository = addressRepository;
            this.vacationRepository = vacationRepository;
            this.dbContext = dbContext;
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
                query = query.Where(d => d.LoginT.LoginAccountState);
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
            var query = GetFreeEmployeeConditionQuery(dateTime);
            query = query.Where(d => d.EmployeeId == employeeId);
            query = query.Where(d => d.RequestT.Count(t => t.RequestTimestamp > startTime && t.RequestTimestamp < endTime && t.IsCanceled == false) == 0);
            var requestCount = await query.CountAsync();
            return requestCount == 0;
        }

        public async Task<Result<EmployeeT>> ValidateEmployeeForRequest(string employeeId, DateTime dateTime, int branchId, int? departmentId = null, int? requestId = null)
        {
            DateTime startTime;
            DateTime endTime;
            if (string.IsNullOrEmpty(employeeId))
            {
                return ResultFactory<EmployeeT>.CreateSuccessResponse();
            }
            var employee = await employeeRepository.Where(d => d.EmployeeId == employeeId)
                .Include(d => d.LoginT)
                .Include(d => d.FiredStaffT)
                .Include(d => d.DepartmentEmployeeT)
                .Include(d => d.EmployeeWorkplacesT)
                .Include(d => d.OpreationT)
                .FirstOrDefaultAsync();
            if (employee.IsNull())
            {
                return ResultFactory<EmployeeT>.CreateNotFoundResponse("Employee not found");
            }
            //if (employee.IsApproved == false)
            //{
            //    return ResultFactory<EmployeeT>.CreateNotFoundResponse("Employee not approved");
            //}
            //if (employee.IsActive == false)
            //{
            //    return ResultFactory<EmployeeT>.CreateNotFoundResponse("Employee not active");
            //}
            if (employee.LoginT.IsNotNull() && employee.LoginT.LoginAccountState == false)
            {
                return ResultFactory<EmployeeT>.CreateErrorResponseMessage("Employee account not active");
            }
            if (employee.FiredStaffT.IsNotNull())
            {
                return ResultFactory<EmployeeT>.CreateErrorResponseMessage("This employee is fired");
            }
            var isInVacation = await vacationRepository.DbSet.AnyAsync(t => t.Day.Year == dateTime.Year && t.Day.Month == dateTime.Month && t.Day.Day == dateTime.Day);
            if (isInVacation)
            {
                return ResultFactory<EmployeeT>.CreateErrorResponseMessage("This employee in vacation");
            }
            if (departmentId.HasValue && !employee.DepartmentEmployeeT.Any(d => d.DepartmentId == departmentId.Value))
            {
                return ResultFactory<EmployeeT>.CreateErrorResponseMessage("This employee department not matched");
            }
            if (!employee.EmployeeWorkplacesT.Any(d => d.BranchId == branchId))
            {
                return ResultFactory<EmployeeT>.CreateErrorResponseMessage("This employee branch not matched");
            }
            if (employee.DepartmentEmployeeT.Any(d => d.DepartmentId == GeneralSetting.CleaningDepartmentId))
            {
                startTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 1);
                endTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 57);
            }
            else
            {
                startTime = dateTime.AddHours(-GeneralSetting.RequestExcutionHours);
                endTime = dateTime.AddHours(GeneralSetting.RequestExcutionHours);
            }
            var requestQuery = requestRepository.Where(t => t.EmployeeId == employeeId && t.RequestTimestamp > startTime && t.RequestTimestamp < endTime
            && t.IsCanceled == false && t.IsCompleted == false);
            if (requestId.HasValue)
            {
                requestQuery = requestQuery.Where(d => d.RequestId != requestId);
            }
            var requestIdList = await requestQuery.Select(d => d.RequestId).ToListAsync();
            if (requestIdList.HasItem())
            {
                var result = ResultFactory<EmployeeT>.CreateErrorResponseMessage("This employee have request in this time");
                result.Message += string.Join(" , ", requestIdList);
                return result;
            }
            return ResultFactory<EmployeeT>.CreateSuccessResponse(employee);
        }

        public async Task<List<string>> GetFreeEmployeeListAsync(DateTime dateTime, int departmentId, int branchId)
        {
            DateTime startTime;
            DateTime endTime;
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
            var query = from e in dbContext.EmployeeT
                        join o in dbContext.OpreationT on new { e.EmployeeId, OpenVacation = false, IsActive = true } equals new { o.EmployeeId, o.OpenVacation, o.IsActive }
                        join ew in dbContext.EmployeeWorkplacesT on new { e.EmployeeId, BranchId = branchId } equals new { ew.EmployeeId, ew.BranchId }
                        join de in dbContext.DepartmentEmployeeT on new { DEmployeeId = e.EmployeeId, DDepartmentId = departmentId } equals new { DEmployeeId = de.EmployeeId, DDepartmentId = de.DepartmentId.Value }
                        join fs in dbContext.FiredStaffT on e.EmployeeId equals fs.EmployeeId into fsJoin
                        from fs in fsJoin.DefaultIfEmpty()
                        join v in dbContext.VacationT on new { e.EmployeeId, Date = dateTime.Date} equals new { v.EmployeeId, v.Day.Date } into vJoin
                        from v in vJoin.DefaultIfEmpty()
                        join r in dbContext.RequestT on 
                            new { REmployeeId = e.EmployeeId, ST = true, ET = true, IsCompleted = false, IsCanceled  = false} equals 
                            new { REmployeeId = r.EmployeeId, ST = r.RequestTimestamp >= startTime, ET = r.RequestTimestamp <= endTime, r.IsCompleted, r.IsCanceled} into rJoin
                        from r in rJoin.DefaultIfEmpty()
                        where e.IsApproved == true
                              && e.IsActive == true
                              && (fs == null)
                              && (v == null)
                              && (r == null)
                        select e.EmployeeId;

            var distinctEmployeeIds = await query.Distinct().ToListAsync();
            return distinctEmployeeIds;
        }
    }
}
