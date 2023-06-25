using App.Global.ExtensionMethods;
using App.Global.Translation;
using Newtonsoft.Json;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class RequestHelperService : IRequestHelperService
    {
        private readonly Translator translator;
        private readonly IRequestService requestService;
        private readonly IEmployeeService employeeService;

        public RequestHelperService(Translator translator, IRequestService requestService, IEmployeeService employeeService)
        {
            this.translator = translator;
            this.requestService = requestService;
            this.employeeService = employeeService;
        }

        private string GetRequestTimeDescripion(RequestT request, DateTime day)
        {
            string des;
            if (request.IsNull())
            {
                des = $"{translator.Tranlate(day.ToString("dddd"))} {day.Day} {translator.Tranlate(day.ToString("MMM"))}";
            }
            else
            {
                day = request.RequestTimestamp.Value;
                if (request.DepartmentId == GeneralSetting.CleaningDepartmentId)
                {
                    des = $"{translator.Tranlate(day.ToString("dddd"))} {day.Day} {translator.Tranlate(day.ToString("MMM"))} - {translator.Tranlate("All Day")}";
                }
                else
                {
                    des = $"{translator.Tranlate(day.ToString("dddd"))} {day.Day} {translator.Tranlate(day.ToString("MMM"))} - {day.ToString("hh:mm")} {translator.Tranlate(day.ToString("tt"))} : {day.AddHours(3).ToString("hh:mm")} {translator.Tranlate(day.AddHours(3).ToString("tt"))}";
                }
            }
            return des;
        }

        public RequestCalendarDto ConvertToCalendarUnit(DateTime day, RequestT request, int? clientId = null)
        {
            RequestCalendarDto clientCalendar = new RequestCalendarDto();
            clientCalendar.Day = GetRequestTimeDescripion(request, day);
            clientCalendar.DayOfTheWeek = translator.Tranlate(day.DayOfWeek.ToString());
            if (request.IsNull())
            {
                clientCalendar.RequestCaption = translator.Tranlate("No requests, Order Now!");
                clientCalendar.Status = 0;
                clientCalendar.CanAdd = true;
            }
            else
            {
                clientCalendar.RequestId = request.RequestId;
                if (request.IsCanceled)
                {
                    clientCalendar.IsCanceled = true;
                    clientCalendar.Status = 1;
                    clientCalendar.CanAdd = true;
                    clientCalendar.RequestCaption = $"{translator.Tranlate("Request")} #{request.RequestId} - {translator.Tranlate("Canceled")}";
                }
                else
                {
                    clientCalendar.IsCanceled = false;
                    clientCalendar.Status = 2;
                    clientCalendar.CanAdd = false;
                    if(clientId.HasValue && clientId.Value == request.ClientId)
                    {
                        clientCalendar.RequestCaption = $"{translator.Tranlate("Request")} #{request.RequestId} ({translator.Tranlate(request.RequestStatusNavigation.RequestStatusName)}) - {request.CustomerPrice} {translator.Tranlate("LE")}";
                    }
                    else
                    {
                        clientCalendar.RequestCaption = $"{translator.Tranlate("Request")} #{request.RequestId} ({translator.Tranlate(request.RequestStatusNavigation.RequestStatusName)})";
                    }
                }
            }
            clientCalendar.PreferredTime = day;
            return clientCalendar;
        }

        public List<RequestCalendarDto> ConvertToCalendar(DateTime startTime, DateTime endTime, List<RequestT> requestList, int? clientId = null)
        {
            DateTime tempTime = startTime;
            List<RequestCalendarDto> clientCalendarList = new List<RequestCalendarDto>();
            RequestT request;
            while(tempTime < endTime)
            {
                request = null;
                if (requestList.HasItem())
                {
                    request = requestList
                        .OrderBy(d => d.RequestId)
                        .LastOrDefault(
                        d => d.RequestTimestamp.HasValue &&
                        d.RequestTimestamp.Value.Year == tempTime.Year &&
                        d.RequestTimestamp.Value.Month == tempTime.Month &&
                        d.RequestTimestamp.Value.Day == tempTime.Day && d.IsCanceled == false);
                    if(request == null)
                    {
                        request = requestList
                        .OrderBy(d => d.RequestId)
                        .LastOrDefault(
                        d => d.RequestTimestamp.HasValue &&
                        d.RequestTimestamp.Value.Year == tempTime.Year &&
                        d.RequestTimestamp.Value.Month == tempTime.Month &&
                        d.RequestTimestamp.Value.Day == tempTime.Day);
                    }
                }
                var clientCalnder = ConvertToCalendarUnit(tempTime, request, clientId);
                clientCalendarList.Add(clientCalnder);
                tempTime = tempTime.AddDays(1);
            }
            return clientCalendarList;
        }

        public List<RequestCalendarDto> ConvertToDayCalendar(DateTime startTime, DateTime endTime, List<RequestT> requestList, bool isCleaningEmployee, int? clientId = null)
        {
            DateTime tempTime = startTime;
            List<RequestCalendarDto> clientCalendarList = new List<RequestCalendarDto>();
            RequestT request;
            while (tempTime < endTime)
            {
                request = null;
                if (requestList.HasItem())
                {
                    request = requestList
                        .Where(
                        d => d.RequestTimestamp.HasValue &&
                        d.RequestTimestamp >= tempTime &&
                        d.RequestTimestamp < tempTime.AddHours(GeneralSetting.RequestExcutionHours))
                        .OrderBy(d => d.IsCanceled).FirstOrDefault();
                }
                var clientCalnder = ConvertToCalendarUnit(tempTime, request, clientId);
                clientCalnder.PreferredTime = tempTime;
                clientCalendarList.Add(clientCalnder);
                if (isCleaningEmployee)
                {
                    tempTime = tempTime.AddDays(1); 
                }
                else
                {
                    if(request != null)
                    {
                        tempTime = request.RequestTimestamp.Value;
                    }
                    else
                    {
                        tempTime = tempTime.AddHours(GeneralSetting.RequestExcutionHours);
                    }

                }
            }
            return clientCalendarList;
        }

        public async Task<List<RequestCalendarDto>> GetClientCalendar(int clientId, int? clientSubscriptionId, DateTime? startTime, DateTime? endTime)
        {
            if (startTime.IsNull())
            {
                startTime = App.Global.DateTimeHelper.DateTimeHelperService.GetStartDateOfMonthS();
            }
            if (endTime.IsNull())
            {
                endTime = App.Global.DateTimeHelper.DateTimeHelperService.GetEndDateOfMonthS();
            }
            var requestList = await requestService.GetList(clientId: clientId, startDate: startTime, endDate: endTime, clientSubscriptionId: clientSubscriptionId,  includeStatus: true);
            return ConvertToCalendar(startTime.Value, endTime.Value, requestList, clientId);
        }

        public async Task<List<RequestCalendarDto>> GetEmployeeCalendar(string employeeId, DateTime? startTime, DateTime? endTime)
        {
            if (startTime.IsNull())
            {
                startTime = App.Global.DateTimeHelper.DateTimeHelperService.GetStartDateOfMonthS();
            }
            if (endTime.IsNull())
            {
                endTime = App.Global.DateTimeHelper.DateTimeHelperService.GetEndDateOfMonthS();
            }
            var requestList = await requestService.GetList(employeeId: employeeId, startDate: startTime, endDate: endTime);
            return ConvertToCalendar(startTime.Value, endTime.Value, requestList);
        }

        public async Task<List<RequestCalendarDto>> GetEmployeeDayCalendar(string employeeId, DateTime? day, int? clientId = null)
        {
            DateTime startTime;
            DateTime endTime;
            startTime = App.Global.DateTimeHelper.DateTimeHelperService.GetStartWorkingDayTimeS(day);
            endTime = App.Global.DateTimeHelper.DateTimeHelperService.GetEndWorkingDayTimeS(day);
            var isCleaningEmployee = await employeeService.IsCleaningEmployeeAsync(employeeId);
            var requestList = await requestService.GetList(employeeId: employeeId, startDate: startTime, endDate: endTime, includeStatus: true);
            return ConvertToDayCalendar(startTime, endTime, requestList, isCleaningEmployee, clientId);
        }

        public List<RequestTrackItemDto> GetRequestTrackItemList()
        {
            var data = System.IO.File.ReadAllText(@"requesttrackitems.json");
            var requestTrackItemList = JsonConvert.DeserializeObject<List<RequestTrackItemDto>>(data);
            return requestTrackItemList;
        }

        //public async Task<RequestTrackerDto> GetTracking(int requestId)
        //{
        //    var requestTrackItemList = GetRequestTrackItemList();
        //    var requestList = await requestService.GetList(requestId: requestId, includeEmployee: true);
        //    var request = requestList.FirstOrDefault();
        //    var employee = request.Employee;
        //    return new RequestTrackerDto
        //    {
        //        RequestTrackItemList = requestTrackItemList,
        //        SelectedEmployee = null,
        //        IsReqestCanceled = request.IsCanceled,
        //        SelectedTrackId = 1
        //    };
        //}


        public async Task<RequestTrackerDto> GetTracking(int requestId)
        {
            var requestList = await requestService.GetList(requestId: requestId, includeRequestStage: true, includeStatus: true, includeEmployee: true, includeCancelT: true, includeDepartment: true);
            var request = requestList.FirstOrDefault();
            var track = new RequestTrackerDto
            {
                IsReqestCanceled = request.IsCanceled,
                SelectedTrackId = 1,
                RequestTrackItemList = new List<RequestTrackItemDto>
               {
                   new RequestTrackItemDto
                   {
                       Id = 1,
                       ActionTime = request.RequestCurrentTimestamp.ToString("yyyy MMM dd hh:mm tt"),
                       Name = "تم استلام الطلب",
                   },
                   new RequestTrackItemDto
                   {
                       Id = 2,
                       Name = "تم تعيين فنى",
                       ActionTime = request.RequestStagesT.SentTimestamp.HasValue ? request.RequestStagesT.SentTimestamp.Value.ToString("yyyy MMM dd hh:mm tt") : "",
                   },
                   new RequestTrackItemDto
                   {
                       Id = 3,
                       Name = "الفنى فى الطريق اليك",
                       ActionTime = request.RequestTimestamp > DateTime.Now.AddHours(-1) ? DateTime.Now.ToString("yyyy MMM dd hh:mm tt") : "",
                       ShowEmployee =  request.RequestTimestamp > DateTime.Now.AddHours(-1) ? true : false,
                   }
               }
            };
            if(request.Employee != null)
            {
                track.SelectedEmployee = new Domain.OtherModels.AppEmployeeDto
                {
                    DepartmentId = request.DepartmentId.Value,
                    Id = request.EmployeeId,
                    Image = request.Employee.EmployeeImageUrl,
                    Name = request.Employee.EmployeeName,
                    Phone = request.Employee.EmployeePhone,
                    ShowContact = true,
                    Title = request.Department.DepartmentName
                };
            }
            if (request.IsCanceled)
            {
                track.RequestTrackItemList.Add(new RequestTrackItemDto
                {
                    Id = 5,
                    Name = "تم الغاء الطلب",
                    ActionTime = request.RequestCanceledT.OrderBy(d => d.RequestId).LastOrDefault().CancelRequestTimestamp.ToString("yyyy MMM dd hh:mm tt"),
                }
                );
            }
            else
            {
                track.RequestTrackItemList.Add(new RequestTrackItemDto
                {
                    Id = 4,
                    Name = "سعدنا بخدمتك",
                    ActionTime = request.RequestStagesT.FinishTimestamp.HasValue ? request.RequestStagesT.FinishTimestamp.Value.ToString("yyyy MMM dd hh:mm tt") : "",
                    ShowReview = true,
                    ShowCompliment = true,
                });
            }
            if (request.IsCanceled)
            {
                track.SelectedTrackId = 5;
            }
            else
            {
                if(request.Employee != null)
                {
                    track.SelectedTrackId = 2;
                }
                if (request.IsCompleted)
                {
                    track.SelectedTrackId = 4;
                }
            }
            return track;
        }

    }
}
