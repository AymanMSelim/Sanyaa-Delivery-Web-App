using App.Global.DateTimeHelper;
using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class VacationService : IVacationService
    {
        private readonly IRepository<VacationT> vacationRepository;
        private readonly IRepository<OpreationT> operationRepository;

        public VacationService(IRepository<VacationT> vacationRepository, IRepository<OpreationT> operationRepository)
        {
            this.vacationRepository = vacationRepository;
            this.operationRepository = operationRepository;
        }
        public async Task<Result<VacationT>> AddAsync(VacationT vacation)
        {
            bool isExist = await vacationRepository.DbSet.AnyAsync(d => d.EmployeeId == vacation.EmployeeId && d.Day.Date == vacation.Day.Date);
            if (isExist)
            {
                return ResultFactory<VacationT>.CreateErrorResponseMessageFD("This vacation is already registered");
            }
            vacation.CreationTime = DateTime.Now.EgyptTimeNow();
            await vacationRepository.AddAsync(vacation);
            var affectedRows = await vacationRepository.SaveAsync();
            return ResultFactory<VacationT>.CreateAffectedRowsResult(affectedRows, data: vacation);
        }

        public async Task<int> DeleteAsync(int id)
        {
            await vacationRepository.DeleteAsync(id);
            return await vacationRepository.SaveAsync();
        }

        public async Task<VacationIndexDto> GetAppIndexAsync(string employeeId)
        {
            DateTime startDate = App.Global.DateTimeHelper.DateTimeHelperService.GetStartTimeOfDayS();
            DateTime endDate = startDate.AddDays(7);
            VacationIndexDto vacationIndexDto = new VacationIndexDto();
            vacationIndexDto.VacationList = new List<AppVacationDto>(); 
            var operation = await operationRepository.DbSet.FirstOrDefaultAsync(d => d.EmployeeId == employeeId);
            if (operation.IsNotNull())
            {
                vacationIndexDto.OpenVacation = operation.OpenVacation;
                vacationIndexDto.PreferredWorkingStartHour = operation.PreferredWorkingStartHour;
                vacationIndexDto.PreferredWorkingEndHour = operation.PreferredWorkingEndHour;
            }
            var vacationList = await vacationRepository.Where(d => d.EmployeeId == employeeId && d.Day >= startDate && d.Day <= endDate).ToListAsync();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var appVacationDto = new AppVacationDto();
                appVacationDto.Day = date;
                appVacationDto.DayCaption = date.ToString("yyyy-MM-dd dddd");
                var dayVacation = vacationList.FirstOrDefault(d => d.Day.Date == date.Date);
                if (dayVacation.IsNotNull())
                {
                    appVacationDto.VacationId = dayVacation.VacationId;
                    appVacationDto.IsVacation = true;
                }
                vacationIndexDto.VacationList.Add(appVacationDto);
            }
            return vacationIndexDto;
        }

        public Task<List<VacationT>> GetListAsync(string employeeId = null, DateTime? fromDate = null, DateTime? toDate = null, bool includeEmployee = false)
        {
            var query = vacationRepository.DbSet.AsQueryable();
            if(string.IsNullOrEmpty(employeeId) is false)
            {
                query = query.Where(d => d.EmployeeId == employeeId);
            }
            if (fromDate.HasValue)
            {
                query = query.Where(d => d.Day >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                query = query.Where(d => d.Day <= toDate.Value);
            }
            if (includeEmployee)
            {
                query = query.Include(d => d.Employee);
            }
            return query.ToListAsync();
        }

        public async Task<Result<AppVacationDto>> SwitchAsync(int? vacationId, VacationT vacation)
        {
            AppVacationDto appVacationDto = new AppVacationDto
            {
                Day = vacation.Day,
                DayCaption = vacation.Day.ToString("yyyy-MM-dd dddd"),
            };
            if (vacationId.HasValue)
            {
                var affectedRows = await DeleteAsync(vacationId.Value);
                return ResultFactory<AppVacationDto>.CreateAffectedRowsResult(affectedRows, data: appVacationDto);
            }
            var result = await AddAsync(vacation);
            if (result.IsSuccess)
            {
                appVacationDto.IsVacation = true;
                appVacationDto.VacationId = vacation.VacationId;
            }
            return ResultFactory<AppVacationDto>.CreateSuccessResponse(data: appVacationDto);

        }
    }
}
