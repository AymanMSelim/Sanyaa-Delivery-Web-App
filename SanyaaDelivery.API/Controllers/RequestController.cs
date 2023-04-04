﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.API.ActionsFilter;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Global.DTOs;
using App.Global.ExtensionMethods;
using SanyaaDelivery.Domain.OtherModels;
using AutoMapper;
using App.Global.DateTimeHelper;

namespace SanyaaDelivery.API.Controllers
{
    [Authorize]
    public partial class RequestController : APIBaseAuthorizeController
    {
        private readonly IRequestService requestService;
        private readonly CommonService commonService;
        private readonly IRequestStatusService requestStatusService;
        private readonly IMapper mapper;
        private readonly IRequestHelperService requestHelperService;
        private readonly IRequestUtilityService requestUtilityService;

        public RequestController(IRequestService requestService, 
            CommonService commonService,
            IRequestStatusService requestStatusService,
            IMapper mapper, IRequestHelperService requestHelperService, IRequestUtilityService requestUtilityService)
        {
            this.requestService = requestService;
            this.commonService = commonService;
            this.requestStatusService = requestStatusService;
            this.mapper = mapper;
            this.requestHelperService = requestHelperService;
            this.requestUtilityService = requestUtilityService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Result<RequestT>>> Add(RequestT request)
        {
            try
            {
                if (request.IsNull())
                {
                    return Ok(ResultFactory<RequestT>.CreateErrorResponse(null, App.Global.Enums.ResultStatusCode.NullableObject));
                }
                int affectedRecords = await requestService.AddAsync(request);
                if (affectedRecords > 0)
                {
                    return Ok(ResultFactory<RequestT>.CreateSuccessResponse(request, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<RequestT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RequestT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdatePrice")]
        public async Task<ActionResult<Result<object>>> UpdatePrice(UpdateRequestPriceDto model)
        {
            try
            {
                if (model.IsNull())
                {
                    return Ok(ResultFactory<object>.CreateErrorResponse(null, App.Global.Enums.ResultStatusCode.NullableObject));
                }
                int affectedRecords = await requestService.UpdatePriceAsync(model);
                return Ok(ResultFactory<object>.CreateAffectedRowsResult(affectedRecords));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdatePhone")]
        public async Task<ActionResult<Result<object>>> UpdatePhone(UpdateRequestPhoneDto model)
        {
            try
            {
                if (model.IsNull())
                {
                    return Ok(ResultFactory<object>.CreateErrorResponse(null, App.Global.Enums.ResultStatusCode.NullableObject));
                }
                int affectedRecords = await requestService.UpdatePhoneAsync(model);
                return Ok(ResultFactory<object>.CreateAffectedRowsResult(affectedRecords));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateAddress")]
        public async Task<ActionResult<Result<object>>> UpdateAddress(UpdateRequestAddressDto model)
        {
            try
            {
                if (model.IsNull())
                {
                    return Ok(ResultFactory<object>.CreateErrorResponse(null, App.Global.Enums.ResultStatusCode.NullableObject));
                }
                int affectedRecords = await requestService.UpdateAddressAsync(model);
                return Ok(ResultFactory<object>.CreateAffectedRowsResult(affectedRecords));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }
        [HttpPost("AddCustom")]
        public async Task<ActionResult<Result<AppRequestDto>>> AddCustom(AddRequestDto requestDto)
        {
            try
            {
                if (requestDto.IsNull())
                {
                    return ResultFactory<AppRequestDto>.CreateErrorResponseMessage("No data sent", App.Global.Enums.ResultStatusCode.EmptyData, App.Global.Enums.ResultAleartType.FailedDialog);

                }
                requestDto.ClientId = commonService.GetClientId(requestDto.ClientId);
                if (requestDto.ClientId.IsNull())
                {
                    return ResultFactory<AppRequestDto>.CreateErrorResponseMessage("Client id can't be null", App.Global.Enums.ResultStatusCode.EmptyData, App.Global.Enums.ResultAleartType.FailedDialog);
                }
                var client = await commonService.GetClient(requestDto.ClientId);
                if (client.IsGuest)
                {
                    return Ok(ResultFactory<List<ClientSubscriptionT>>.CreateRequireRegisterResponse());
                }
                if (requestDto.AddressId.IsNull())
                {
                    var address = await commonService.GetDefaultAddress();
                    requestDto.AddressId = address.AddressId;

                }
                if (requestDto.PhoneId.IsNull())
                {
                    var phone = await commonService.GetDefaultPhone();
                    requestDto.PhoneId = phone.ClientPhoneId;
                }
                if (requestDto.AddressId.IsNull() || requestDto.AddressId == 0)
                {
                    return ResultFactory<AppRequestDto>.CreateErrorResponseMessage("Address can't be null", App.Global.Enums.ResultStatusCode.EmptyData, App.Global.Enums.ResultAleartType.FailedDialog);
                }
                if (requestDto.PhoneId.IsNull() || requestDto.PhoneId == 0)
                {
                    return ResultFactory<AppRequestDto>.CreateErrorResponseMessage("Phone can't be null", App.Global.Enums.ResultStatusCode.EmptyData, App.Global.Enums.ResultAleartType.FailedDialog);
                }
                int systemUserId = commonService.GetSystemUserId();
                bool isViaApp = commonService.IsViaApp();
                var result = await requestService.AddAsync(requestDto, isViaApp, systemUserId);
                if (result.IsSuccess)
                {
                    var mapRequest = mapper.Map<AppRequestDto>(result.Data);
                    return ResultFactory<AppRequestDto>.CreateSuccessResponse(mapRequest);
                }
                else

                {
                    return ResultFactory<AppRequestDto>.CreateErrorResponseMessage(result.Message, (App.Global.Enums.ResultStatusCode)result.StatusCode, (App.Global.Enums.ResultAleartType)result.AleartType);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppRequestDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddSubscriptionRequest")]
        public async Task<ActionResult<Result<RequestCalendarDto>>> AddSubscriptionRequest(AddRequestDto requestDto)
        {
            try
            {
                if (requestDto.IsNull())
                {
                    return ResultFactory<RequestCalendarDto>.CreateErrorResponseMessage("No data sent", App.Global.Enums.ResultStatusCode.EmptyData, App.Global.Enums.ResultAleartType.FailedDialog);

                }
                if (requestDto.ClientSubscriptionId.IsNull() || requestDto.ClientSubscriptionId == 0)
                {
                    return ResultFactory<RequestCalendarDto>.CreateErrorResponseMessage("Client subscription id can't be null", App.Global.Enums.ResultStatusCode.EmptyData, App.Global.Enums.ResultAleartType.FailedDialog);
                }
                requestDto.ClientId = commonService.GetClientId(requestDto.ClientId);
                if (requestDto.ClientId.IsNull())
                {
                    return ResultFactory<RequestCalendarDto>.CreateErrorResponseMessage("Client id can't be null", App.Global.Enums.ResultStatusCode.EmptyData, App.Global.Enums.ResultAleartType.FailedDialog);
                }
                var client = await commonService.GetClient(requestDto.ClientId);
                if (client.IsGuest)
                {
                    return Ok(ResultFactory<List<ClientSubscriptionT>>.CreateRequireRegisterResponse());
                }
                int systemUserId = commonService.GetSystemUserId();
                bool isViaApp = commonService.IsViaApp();
                var result = await requestService.AddAsync(requestDto.ClientSubscriptionId.Value, requestDto.RequestTime.Value, systemUserId, isViaApp);
                if (result.IsSuccess)
                {
                    var r1 = await requestService.GetList(requestId: result.Data.RequestId, includeStatus: true);
                    var r = r1.FirstOrDefault();
                    var request = requestHelperService.ConvertToCalendarUnit(r.RequestTimestamp.Value, r, r.ClientId);
                    return ResultFactory<RequestCalendarDto>.CreateSuccessResponse(request);
                }
                else

                {
                    return ResultFactory<RequestCalendarDto>.CreateErrorResponseMessage(result.Message, (App.Global.Enums.ResultStatusCode)result.StatusCode, (App.Global.Enums.ResultAleartType)result.AleartType);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppRequestDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetInfo/{requestId}")]
        public async Task<ActionResult<Result<RequestT>>> GetInfo(int requestId)
        {
            try
            {
                var request = await requestService.GetInfo(requestId);
                return ResultFactory<RequestT>.CreateSuccessResponse(request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RequestT>.CreateExceptionResponse(ex));
            }
        }
        [Authorize(Roles = "Test")]
        [HttpGet("Test")]
        public async Task<ActionResult<Result<RequestT>>> Test()
        {
            try
            {
                return ResultFactory<RequestT>.CreateSuccessResponseSD(message: $"Datetime now: {DateTime.Now.ToString()}, Egypt now: {DateTime.Now.EgyptTimeNow().ToString()}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RequestT>.CreateExceptionResponse(ex));
            }
        }


        [HttpGet("GetAppList")]
        public async Task<ActionResult<Result<List<AppRequestDto>>>> GetAppList(int? clientId = null, int? status = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                var list = await requestService.GetAppList(clientId: clientId.Value, status: status);
                var mapList = mapper.Map<List<AppRequestDto>>(list);
                return ResultFactory<List<AppRequestDto>>.CreateSuccessResponse(mapList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<AppRequestDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetAppRequestStatusList")]
        public async Task<ActionResult<Result<List<RequestGroupStatusDto>>>> GetAppRequestStatusList()
        {
            try
            {
                List<RequestGroupStatusDto> statusList = null;
                var list = await requestStatusService.GetGroupListAsync();
                if (list.HasItem())
                {
                    statusList = mapper.Map<List<RequestGroupStatusDto>>(list);
                }
                return ResultFactory<List<RequestGroupStatusDto>>.CreateSuccessResponse(statusList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<RequestGroupStatusDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetAppDetails/{requestId}")]
        public async Task<ActionResult<Result<AppRequestDetailsDto>>> GetAppDetails(int requestId)
        {
            try
            {
                AppRequestDetailsDto request = await requestService.GetAppDetails(requestId);
                if (request.IsNotNull())
                {
                    if (request.IsCanceled || request.IsCompleted)
                    {
                        request.ShowCancelRequestButton = false;
                        request.ShowAddServiceButton = false;
                        request.ShowReAssignEmployeeButton = false;
                        request.ShowDelayRequestButton = false;
                    }
                    else
                    {
                        request.ShowCancelRequestButton = true;
                        request.ShowAddServiceButton = true;
                        request.ShowReAssignEmployeeButton = true;
                        request.ShowDelayRequestButton = true;
                    }
                    if (request.Employee.IsNotNull() && DateTime.Now.EgyptTimeNow() > request.RequestTimestamp.AddHours(-1))
                    {
                        request.Employee.ShowContact = true;
                        request.ShowReAssignEmployeeButton = false;
                        request.ShowDelayRequestButton = false;
                    }
                }
                return ResultFactory<AppRequestDetailsDto>.CreateSuccessResponse(request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppRequestDetailsDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<Result<List<RequestDto>>>> GetList(int? requestId = null, int? branchId = null, DateTime? startDate = null, DateTime? endDate = null,
            string employeeId = null, int? clientId = null, int? departmentId = null, int? status = null, int? systemUserId = null,
            bool? isCompleted = null, bool? isCanceled = null, bool? isReviewed = null, bool? isFollowUp = null, bool? isPaid = null, bool includeEmployeeLogin = false)
        {
            try
            {
                var list = await requestService.GetList(requestId: requestId, branchId: branchId, startDate: startDate, endDate: endDate, employeeId: employeeId, clientId: clientId, departmentId: departmentId, requestStatus: status,
                    getCanceled: isCanceled, isPaid: isPaid, isCompleted: isCompleted, isFollowUp: isFollowUp, isReviewed: isReviewed, systemUserId: systemUserId,
                    includeAddress: true, includePhone: true, includeClient: true, includeDepartment: true, includeSubscription: true, includeRequestService: true, includeService: true,
                    includeEmployee: true, includeStatus: true, includeBranch: true, includeSystemUser: true, includeEmployeeLogin: includeEmployeeLogin);
                var mapList = mapper.Map<List<RequestDto>>(list);
                return ResultFactory<List<RequestDto>>.CreateSuccessResponse(mapList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<RequestDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetClientCalender")]
        public async Task<ActionResult<Result<List<RequestCalendarDto>>>> GetClientCalender(int? clientSubscriptionId, int? clientId = null, DateTime? startTime = null, DateTime? endTime = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (clientId.IsNull())
                {
                    return ResultFactory<List<RequestCalendarDto>>.ReturnClientError();
                }
                var clientCalendatList = await requestHelperService.GetClientCalendar(clientId.Value, clientSubscriptionId, startTime, endTime);
                return ResultFactory<List<RequestCalendarDto>>.CreateSuccessResponse(clientCalendatList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<RequestCalendarDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetEmployeeCalender")]
        public async Task<ActionResult<Result<List<RequestCalendarDto>>>> GetEmployeeCalender(string employeeId, DateTime? startTime = null, DateTime? endTime = null, int? clientSubscriptionId = null)
        {
            try
            {
                var clientCalendatList = await requestHelperService.GetEmployeeCalendar(employeeId, startTime, endTime);
                return ResultFactory<List<RequestCalendarDto>>.CreateSuccessResponse(clientCalendatList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<RequestCalendarDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetEmployeeDayCalender")]
        public async Task<ActionResult<Result<List<RequestCalendarDto>>>> GetEmployeeDayCalender(string employeeId, DateTime? day, int? clientSubscriptionId = null, int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                var clientCalendatList = await requestHelperService.GetEmployeeDayCalendar(employeeId, day, clientId);
                return ResultFactory<List<RequestCalendarDto>>.CreateSuccessResponse(clientCalendatList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<RequestCalendarDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Cancel")]
        public async Task<ActionResult<Result<RequestCanceledT>>> Cancel(int requestId, string reason)
        {
            try
            {
                var result = await requestService.CancelAsync(requestId, reason, commonService.GetSystemUserId());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RequestCanceledT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("ChangeTime")]
        public async Task<ActionResult<Result<RequestDelayedT>>> ChangeTime(int requestId, string reason, DateTime newTime, bool skipCheckEmployee = false)
        {
            try
            {
                var result = await requestService.ChangeTimeAsync(requestId, newTime, reason, commonService.GetSystemUserId(), skipCheckEmployee);
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RequestDelayedT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("ReAssignEmployee")]
        public async Task<ActionResult<Result<AppEmployeeDto>>> ReAssignEmployee(ReAssignEmployeeDto employeeDto)
        {
            AppEmployeeDto employee = null;
            try
            {
                var result = await requestService.ReAssignEmployeeAsync(employeeDto);
                
                if (result.IsSuccess && result.Data.IsNotNull())
                {
                    employee = mapper.Map<AppEmployeeDto>(result.Data);
                }
                return result.Convert(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppEmployeeDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddComplaint")]
        public async Task<ActionResult<Result<RequestComplaintT>>> AddComplaint(int requestId, string complaint)
        {
            try
            {
                var requestCompaiant = new RequestComplaintT
                {
                    RequestId = requestId,
                    ComplaintDes = complaint,
                    SystemUserId = commonService.GetSystemUserId(),
                    ComplaintIsSolved = "لا"
                };
                var affectedRows = await requestService.AddComplaintAsync(requestCompaiant);
                return ResultFactory<RequestComplaintT>.CreateAffectedRowsResult(affectedRows);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RequestComplaintT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetTracking/{requestId}")]
        public async Task<ActionResult<Result<RequestTrackerDto>>> GetTracking(int requestId)
        {
            try
            {
                var requestTrackerDto = await requestHelperService.GetTracking(requestId);
                return ResultFactory<RequestTrackerDto>.CreateSuccessResponse(requestTrackerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RequestTrackerDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddUpdateService")]
        public async Task<ActionResult<Result<AppRequestDetailsDto>>> AddUpdateService(AddUpdateeRequestServiceDto model)
        {
            AppRequestDetailsDto request = null;
            Result<RequestServicesT> result;
            try
            {
                var service = new RequestServicesT
                {
                    RequestId = model.RequestId,
                    AddTimestamp = DateTime.Now,
                    RequestServicesQuantity = model.ServiceQuantity,
                    ServiceId = model.ServiceId
                };
                result =  await requestService.AddUpdateServiceAsync(service);
                if (result.IsFail)
                {
                    return result.Convert(request);
                }
                request = await requestService.GetAppDetails(model.RequestId);
                if (request.IsNotNull())
                {
                    if (request.IsCanceled || request.IsCompleted)
                    {
                        request.ShowCancelRequestButton = false;
                        request.ShowAddServiceButton = false;
                        request.ShowReAssignEmployeeButton = false;
                        request.ShowDelayRequestButton = false;
                    }
                    else
                    {
                        request.ShowCancelRequestButton = true;
                        request.ShowAddServiceButton = true;
                        request.ShowReAssignEmployeeButton = true;
                        request.ShowDelayRequestButton = true;
                    }
                    if (request.Employee.IsNotNull() && DateTime.Now.EgyptTimeNow() > request.RequestTimestamp.AddHours(-1)
                        && request.IsCanceled == false && request.IsCompleted == false)
                    {
                        request.Employee.ShowContact = true;
                        request.ShowReAssignEmployeeButton = false;
                        request.ShowDelayRequestButton = false;
                    }
                }
                return ResultFactory<AppRequestDetailsDto>.CreateSuccessResponse(request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RequestCanceledT>.CreateExceptionResponse(ex));
            }
        }
        [AllowAnonymous]
        [HttpPost("AddUpdateServiceO")]
        public async Task<ActionResult<Result<RequestServicesT>>> AddUpdateServiceO(AddUpdateeRequestServiceODto model)
        {
           
            try
            {
                var result = await requestService.AddUpdateServiceOAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RequestServicesT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("SetReviewed")]
        public async Task<ActionResult<Result<object>>> SetReviewed(int requestId)
        {
            try
            {
                var result = await requestUtilityService.SetReviewedAsync(requestId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("SetAsUnReviewed")]
        public async Task<ActionResult<Result<object>>> SetAsUnReviewed(int requestId)
        {
            try
            {
                var result = await requestUtilityService.SetAsUnReviewedAsync(requestId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("ResetRequest")]
        public async Task<ActionResult<Result<object>>> ResetRequest(int requestId)
        {
            try
            {
                var result = await requestUtilityService.ResetRequestAsync(requestId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("SetComplete")]
        public async Task<ActionResult<Result<object>>> SetComplete(int requestId)
        {
            try
            {
                var result = await requestUtilityService.CompleteAsync(requestId, commonService.GetSystemUserId());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("FollowUp")]
        public async Task<ActionResult<Result<object>>> FollowUp(FollowUpT followUp)
        {
            try
            {
                followUp.SystemUserId = commonService.GetSystemUserId();
                var result = await requestUtilityService.FollowAsync(followUp);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("Get/{requestId}")]
        public async Task<ActionResult<Result<RequestTrackerDto>>> GetTrack0ing(int requestId)
        {
            try
            {
                var requestTrackerDto = await requestHelperService.GetTracking(requestId);
                return ResultFactory<RequestTrackerDto>.CreateSuccessResponse(requestTrackerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RequestTrackerDto>.CreateExceptionResponse(ex));
            }
        }

    }
}