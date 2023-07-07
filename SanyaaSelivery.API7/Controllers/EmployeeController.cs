using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using SanyaaDelivery.Application;

namespace SanyaaDelivery.API.Controllers
{
    [Authorize]
    public class EmployeeController : APIBaseAuthorizeController
    {
        private readonly IEmployeeService employeeService;
        private readonly IRequestService orderService;
        private readonly IEmployeeAppAccountService employeeAppAccountService;
        private readonly CommonService commonService;
        private readonly ICityService cityService;
        private readonly IFavouriteEmployeeService favouriteEmployeeService;
        private readonly IClientSubscriptionService clientSubscriptionService;
        private readonly IEmployeeRequestService employeeRequestService;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeService employeeService, IRequestService orderService, 
            IEmployeeAppAccountService employeeAppAccountService, CommonService commonService, ICityService cityService, 
            IFavouriteEmployeeService favouriteEmployeeService, IClientSubscriptionService clientSubscriptionService,
            IEmployeeRequestService employeeRequestService, IMapper mapper) : base(commonService)
        {
            this.employeeService = employeeService;
            this.orderService = orderService;
            this.employeeAppAccountService = employeeAppAccountService;
            this.commonService = commonService;
            this.cityService = cityService;
            this.favouriteEmployeeService = favouriteEmployeeService;
            this.clientSubscriptionService = clientSubscriptionService;
            this.employeeRequestService = employeeRequestService;
            this.mapper = mapper;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Result<EmployeeT>>> Add(EmployeeT employee)
        {
            try
            {
                if(employee == null)
                {
                    return Ok(ResultFactory<EmployeeT>.CreateModelNotValidResponse("Employee can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                employee.SystemId = commonService.GetSystemUserId();
                var affectedRows = await employeeService.AddAsync(employee);
                return Ok(ResultFactory<EmployeeT>.CreateAffectedRowsResult(affectedRows, data: employee));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<EmployeeT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetCustomList")]
        public async Task<ActionResult<Result<List<EmployeeDto>>>> GetCustomList(string? id = null, string? name = null, string? phone = null, int? departmentId = null, int? branchId = null, bool? isNewEmployee = null)
        {
            try
            {
               var list = await employeeService.GetCustomListAsync(id, name, phone, departmentId, branchId, isNewEmployee);
                return ResultFactory<List<EmployeeDto>>.CreateSuccessResponse(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<EmployeeDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddWorkplace")]
        public async Task<ActionResult<Result<EmployeeWorkplacesT>>> AddWorkplace(EmployeeWorkplacesT employeeWorkplace)
        {
            try
            {
                if (employeeWorkplace == null)
                {
                    return BadRequest(ResultFactory<EmployeeWorkplacesT>.CreateModelNotValidResponse("EmployeeWorkplace can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                var result = await employeeService.AddWorkplace(employeeWorkplace);
                if (result > 0)
                {
                    return Ok(ResultFactory<EmployeeWorkplacesT>.CreateSuccessResponse(employeeWorkplace));
                }
                else
                {
                    return BadRequest(ResultFactory<EmployeeWorkplacesT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<EmployeeWorkplacesT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteWorkplace/{employeeWorkplaceId}")]
        public async Task<ActionResult<Result<object>>> DeleteWorkplace(int employeeWorkplaceId)
        {
            try
            {
                var result = await employeeService.DeleteWorkplace(employeeWorkplaceId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetWorkplaceList/{employeeId}")]
        public async Task<ActionResult<Result<List<EmployeeWorkplacesT>>>> GetWorkplaceList(string employeeId)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeId))
                {
                    return BadRequest(ResultFactory<List<EmployeeWorkplacesT>>.CreateModelNotValidResponse("EmployeeId can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                var employeeWorkspaceList = await employeeService.GetWorkplaceList(employeeId);
                if (employeeWorkspaceList.HasItem())
                {
                    return Ok(ResultFactory<List<EmployeeWorkplacesT>>.CreateSuccessResponse(employeeWorkspaceList));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<EmployeeWorkplacesT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetAppAccountIndex/{employeeId}")]
        public async Task<ActionResult<Result<AppEmployeeAccountIndexDto>>> GetAppAccountIndex(string? employeeId = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    employeeId = commonService.GetEmployeeId(employeeId);
                }
                if (string.IsNullOrEmpty(employeeId))
                {
                    return Ok(ResultFactory<AppEmployeeAccountIndexDto>.ReturnEmployeeError());
                }
                var model = await employeeService.GetAppAccountIndex(employeeId);
                model.EmployeeImage = commonService.GetHost() + model.EmployeeImage;
                return Ok(ResultFactory<AppEmployeeAccountIndexDto>.CreateSuccessResponse(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppEmployeeAccountIndexDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddWorkplaceByCity")]
        public async Task<ActionResult<Result<AppEmployeeAccountIndexDto>>> AddWorkplaceByCity(AddWorkplaceByCityDto model)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    model.EmployeeId = commonService.GetEmployeeId(model.EmployeeId);
                }
                if (string.IsNullOrEmpty(model.EmployeeId))
                {
                    return Ok(ResultFactory<AppEmployeeAccountIndexDto>.ReturnEmployeeError());
                }
                var affectedRows = await employeeService.AddWorkplaceByCity(model);
                return Ok(ResultFactory<AppEmployeeAccountIndexDto>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppEmployeeAccountIndexDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddDepartment")]
        public async Task<ActionResult<Result<DepartmentEmployeeT>>> AddDepartment(DepartmentEmployeeT departmentEmployee)
        {
            try
            {
                if (departmentEmployee == null)
                {
                    return BadRequest(ResultFactory<DepartmentEmployeeT>.CreateModelNotValidResponse("DepartmentEmployee can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                var result = await employeeService.AddDepartment(departmentEmployee);
                if (result > 0)
                {
                    return Ok(ResultFactory<DepartmentEmployeeT>.CreateSuccessResponse(departmentEmployee));
                }
                else
                {
                    return BadRequest(ResultFactory<DepartmentEmployeeT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<DepartmentEmployeeT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteDepartment/{departmentEmployeeId}")]
        public async Task<ActionResult<Result<DepartmentEmployeeT>>> DeleteDepartment(int departmentEmployeeId)
        {
            try
            {
                var result = await employeeService.DeleteDepartment(departmentEmployeeId);
                if (result > 0)
                {
                    return Ok(ResultFactory<DepartmentEmployeeT>.CreateSuccessResponse());
                }
                else
                {
                    return BadRequest(ResultFactory<DepartmentEmployeeT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<DepartmentEmployeeT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetDepartmentList/{employeeId}")]
        public async Task<ActionResult<Result<List<DepartmentEmployeeT>>>> GetDepartmentList(string employeeId)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeId))
                {
                    return BadRequest(ResultFactory<List<DepartmentEmployeeT>>.CreateModelNotValidResponse("EmployeeId can't be null", App.Global.Enums.ResultStatusCode.NullableObject));
                }
                var departmentList = await employeeService.GetDepartmentList(employeeId);
                if (departmentList.HasItem())
                {
                    return Ok(ResultFactory<List<DepartmentEmployeeT>>.CreateSuccessResponse(departmentList));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<DepartmentEmployeeT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Update")]
        public async Task<ActionResult<Result<EmployeeT>>> Update(EmployeeT employee)
        {
            var result = await employeeService.UpdateAsync(employee);
            if (result > 0)
            {
                return Ok(ResultFactory<EmployeeT>.CreateSuccessResponse(employee));
            }
            else
            {
                return Ok(ResultFactory<EmployeeT>.CreateErrorResponse());
            }
        }

        [HttpGet("GetInfo/{employeeId}")]
        public async Task<ActionResult<EmployeeT>> GetInfo(string employeeId)
        {
            var employee = await employeeService.GetAsync(employeeId);
            //employee.EmployeeWorkplacesT = employee.EmployeeWorkplacesT;
            if(employee == null)
            {
                return Ok(new { Message = $"Employee {employeeId} Not Found" });
            }
            return Ok(employee);
        }

        [HttpGet("Get/{employeeId}")]
        public async Task<ActionResult<Result<EmployeeT>>> Get(string employeeId, bool includeWorkplace = false, bool includeDepartment = false,
            bool includeLocation = false, bool includeLogin = false, bool includeSubscription = false, bool includeReview = false, bool includeFavourite = false, bool includeOperation = false)
        {
            try
            {
                var employee = await employeeService.GetAsync(employeeId, includeWorkplace, includeDepartment,
                    includeLocation, includeLogin, includeSubscription, includeReview, includeReview, includeFavourite, includeOperation);
                return Ok(ResultFactory<EmployeeT>.CreateSuccessResponse(employee));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<EmployeeT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetCleanerInfo")]
        public async Task<ActionResult<EmployeeT>> GetCleanerInfoWithOrders(string employeeId, DateTime? dateTime = null)
        {
            EmployeeT employee;
            if(dateTime == null) { dateTime = DateTime.Now; };
            employee = await employeeService.GetWithBeancesAndTimetable(employeeId.Trim());
            employee.RequestT = await orderService.GetEmployeeOrdersExceptCanceledList(employeeId.Trim(), dateTime.Value);
            if (employee == null)
            {
                return Ok(new { Message = $"Employee {employeeId} Not Found" });
            }
            return Ok(employee);
        }

        [HttpGet("GetDayStatus")]
        public async Task<ActionResult<EmployeeDayStatusDto>> GetDayStatus(string employeeId, DateTime dateOfDay)
        {
            EmployeeDayStatusDto employeeDayStatusDto = new EmployeeDayStatusDto(employeeId, dateOfDay);
            int orderCount = await orderService.GetOrdersExceptCanceledCountByEmployee(employeeId, dateOfDay);
            if (orderCount > 0)
            {
                employeeDayStatusDto.Status = "HaveOrders";
            }
            else
            {
                employeeDayStatusDto.Status = "Free";
            }
            return employeeDayStatusDto;
        }


        [HttpGet("ValidateEmployee")]
        public async Task<ActionResult<Result<EmployeeT>>> ValidateEmployee(string employeeId, DateTime date, int branchId, int departmentId)
        {
            try
            {
                var result = await employeeRequestService.ValidateEmployeeForRequest(employeeId, date, branchId, departmentId);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<EmployeeT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetDayStatusWithOrders")]
        public async Task<ActionResult<EmployeeDayStatusDto>> GetDayStatusWithOrders(string employeeId, DateTime dateOfDay)
        {
            EmployeeDayStatusDto employeeDayStatusDto = new EmployeeDayStatusDto(employeeId, dateOfDay);
            List<OrderDto> employeeOrders = await orderService.GetEmployeeOrdersCustomList(employeeId, dateOfDay);
            if (employeeOrders.Count > 0)
            {
                employeeDayStatusDto.Status = "HaveOrders";
                employeeDayStatusDto.OrdersList = employeeOrders;
            }
            else
            {
                employeeDayStatusDto.Status = "Free";
            }
            return employeeDayStatusDto;
        }

        [HttpGet("GetAppReviewList/{employeeId?}")]
        public async Task<ActionResult<Result<AppEmployeeDto>>> GetAppReviewList(string? employeeId = null)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeId))
                {
                    return Ok(ResultFactory<object>.ReturnEmployeeError());
                }
                var employee = await employeeService.GetAsync(employeeId, includeReview: true, includeReviewClient: true, includeFavourite: true, includeWorkplace: true);
                var employeeDto = mapper.Map<AppEmployeeDto>(employee);
                if (commonService.IsViaClientApp())
                {
                    var clientId = commonService.GetClientId();
                    var clientSubscriptionList = await clientSubscriptionService.GetListAsync(clientId, includeSubscription: true);
                    if (clientSubscriptionList.HasItem())
                    {
                        if (clientSubscriptionList.Any(d => d.Subscription.DepartmentId == employeeDto.DepartmentId))
                        {
                            employeeDto.ShowCalendar = true;
                        }
                    }
                }
                return Ok(ResultFactory<AppEmployeeDto>.CreateSuccessResponse(employeeDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppEmployeeDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetAppReviewIndex/{employeeId?}")]
        public async Task<ActionResult<Result<AppReviewIndexDto>>> GetAppReviewIndex(string? employeeId = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    employeeId = commonService.GetEmployeeId(employeeId);
                }
                if (string.IsNullOrEmpty(employeeId))
                {
                    return Ok(ResultFactory<AppReviewIndexDto>.ReturnEmployeeError());
                }
                var model = await employeeService.GetAppReviewIndexAsync(employeeId);
                return Ok(ResultFactory<AppReviewIndexDto>.CreateSuccessResponse(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppReviewIndexDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetAppList")]
        public async Task<ActionResult<List<AppEmployeeDto>>> GetAppList(DateTime selectedDate, int? clientId = null,  int? departmentId = null,
            int? cityId = null, int? branchId = null, bool getAll = false, bool isNewSubscription = false)
        {
            BranchT currentBranch;
            CartT cart;
            try
            {
                clientId = commonService.GetClientId(clientId);
                if (branchId.IsNull())
                {
                    if (cityId.IsNotNull())
                    {
                        currentBranch = await cityService.GetCityBranchAsync(cityId.Value);
                    }
                    else
                    {
                        currentBranch = await commonService.GetCurrentAddressBranch(clientId);
                    }
                    branchId = currentBranch.BranchId;
                }
                if (departmentId.IsNull())
                {
                     cart = await commonService.GetCurrentClientCartAsync(clientId);
                     departmentId = cart.DepartmentId;
                }
                var employeeList = await employeeRequestService.GetFreeEmployeeListByBranch(selectedDate, departmentId.Value, branchId.Value, true, true, true);
                var mapList = mapper.Map<List<AppEmployeeDto>>(employeeList);
                mapList.ForEach(d => d.EmployeeReviews = null);
                if (isNewSubscription == false)
                {
                    if (departmentId.Value == GeneralSetting.CleaningDepartmentId)
                    {
                        var subscriptionList = await clientSubscriptionService.GetListAsync(clientId, departmentId);
                        if (subscriptionList.HasItem())
                        {
                            mapList.ForEach(d => d.ShowCalendar = true);
                        }
                    }
                }
                if (mapList.IsEmpty())
                {
                    return Ok(ResultFactory<List<AppEmployeeDto>>.CreateErrorResponseMessageFD("No any technicial available at this time please select another time"));
                }
                else
                {
                    return Ok(ResultFactory<List<AppEmployeeDto>>.CreateSuccessResponse(mapList));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<AppEmployeeDto>>.CreateExceptionResponse(ex));
            }
        }
        
        [HttpGet("GetFavouriteList")]
        public async Task<ActionResult<List<AppEmployeeDto>>> GetFavouriteList(int? clientId = null)
        {
            try
            {
                List<AppEmployeeDto> mapList = null;
                clientId = commonService.GetClientId(clientId);
                var list = await favouriteEmployeeService.GetListAsync(clientId.Value, true);
                if (list.HasItem())
                {
                    var employeeList = list.Select(d => d.Employee).ToList();
                    mapList = mapper.Map<List<AppEmployeeDto>>(employeeList);
                    mapList.ForEach(d => d.EmployeeReviews = null);
                    var clientSubscriptionList = await clientSubscriptionService.GetListAsync(clientId, includeSubscription: true);
                    if (clientSubscriptionList.HasItem())
                    {
                        foreach (var item in mapList)
                        {
                            if(clientSubscriptionList.Any(d => d.Subscription.DepartmentId == item.DepartmentId))
                            {
                                item.ShowCalendar = true;
                            }
                        }
                    }
                }
                return Ok(ResultFactory<List<AppEmployeeDto>>.CreateSuccessResponse(mapList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<AppEmployeeDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("FavouriteSwitch")]
        public async Task<ActionResult<FavouriteEmployeeT>> FavouriteSwitch(string employeeId, int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId(clientId);
                var client = await commonService.GetClient(clientId);
                if (client.IsGuest)
                {
                    return Ok(ResultFactory<List<ClientSubscriptionT>>.CreateRequireRegisterResponse());
                }
                int affectedRows = await favouriteEmployeeService.FavouriteSwitch(clientId.Value, employeeId);
                return Ok(ResultFactory<List<AppEmployeeDto>>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<AppEmployeeDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("IsThisEmployeeExist/{nationalNumber}")]
        public async Task<ActionResult<bool>> IsThisEmployeeExist(string nationalNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(nationalNumber))
                {
                    return Ok(ResultFactory<bool>.CreateErrorResponse(message: "Empty natioal number", resultStatusCode: App.Global.Enums.ResultStatusCode.EmptyData));
                }
                var result = await employeeService.IsThisEmployeeExist(nationalNumber);
                return Ok(ResultFactory<bool>.CreateSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<bool>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("FireEmployee")]
        public async Task<ActionResult<Result<object>>> FireEmployee(FireEmployeeDto model)
        {
            try
            {
                var affectedRows = await employeeService.FireEmpolyeeAsync(model);
                return Ok(ResultFactory<object>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("ApproveEmployee")]
        public async Task<ActionResult<Result<object>>> ApproveEmployee(StringIdDto model)
        {
            try
            {
                var affectedRows = await employeeService.ApproveEmployeeAsync(model.Id);
                return Ok(ResultFactory<object>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteEmployee")]
        public async Task<ActionResult<Result<object>>> DeleteEmployee(StringIdDto model)
        {
            try
            {
                var affectedRows = await employeeService.DeleteEmployeeAsync(model.Id);
                return Ok(ResultFactory<object>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("ReturnEmployee")]
        public async Task<ActionResult<Result<object>>> ReturnEmployee(StringIdDto model)
        {
            try
            {
                var affectedRows = await employeeService.ReturnEmployeeAsync(model.Id);
                return Ok(ResultFactory<object>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }
    }
}
