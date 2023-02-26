using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IRequestHelperService
    {
        RequestCalendarDto ConvertToCalendarUnit(DateTime day, RequestT request, int? clientId = null);
        List<RequestCalendarDto> ConvertToCalendar(DateTime startTime, DateTime endTime, List<RequestT> requestList, int? clientId = null);
        Task<List<RequestCalendarDto>> GetClientCalendar(int clientId, int? clientSubscriptionId, DateTime? startTime, DateTime? endTime);
        Task<List<RequestCalendarDto>> GetEmployeeCalendar(string employeeId, DateTime? startTime, DateTime? endTime);
        Task<List<RequestCalendarDto>> GetEmployeeDayCalendar(string employeeId, DateTime? day, int? clientId = null);
        List<RequestTrackItemDto> GetRequestTrackItemList();
        Task<RequestTrackerDto> GetTracking(int requestId);
    }
}
