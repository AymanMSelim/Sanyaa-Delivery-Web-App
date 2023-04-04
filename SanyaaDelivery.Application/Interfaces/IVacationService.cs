using App.Global.DTOs;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IVacationService
    {
        Task<Result<VacationT>> AddAsync(VacationT vacation);
        Task<Result<AppVacationDto>> SwitchAsync(int? vacationId, VacationT vacation);
        Task<int> DeleteAsync(int id);
        Task<List<VacationT>> GetListAsync(string employeeId = null, DateTime? fromDate = null, DateTime? toDate = null, bool includeEmployee = false);
        Task<VacationIndexDto> GetAppIndexAsync(string employeeId);
    }
}
