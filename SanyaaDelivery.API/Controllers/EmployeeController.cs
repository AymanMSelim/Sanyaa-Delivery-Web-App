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

        public EmployeeController(IEmployeeService employeeService, IRequestService orderService, 
            IEmployeeAppAccountService employeeAppAccountService, CommonService commonService, ICityService cityService)
        {
            this.employeeService = employeeService;
            this.orderService = orderService;
            this.employeeAppAccountService = employeeAppAccountService;
            this.commonService = commonService;
            this.cityService = cityService;
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<EmployeeT>>> Add(EmployeeT employee)
        {
            try
            {
                if(employee == null)
                {
                    return Ok(OpreationResultMessageFactory<EmployeeT>.CreateModelNotValidResponse("Employee can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                var result = await employeeService.AddAsync(employee);
                if (result > 0)
                {
                    return Ok(OpreationResultMessageFactory<EmployeeT>.CreateSuccessResponse(employee));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<EmployeeT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<EmployeeT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<EmployeeWorkplacesT>>> AddWorkplace(EmployeeWorkplacesT employeeWorkplace)
        {
            try
            {
                if (employeeWorkplace == null)
                {
                    return BadRequest(OpreationResultMessageFactory<EmployeeWorkplacesT>.CreateModelNotValidResponse("EmployeeWorkplace can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                var result = await employeeService.AddWorkplace(employeeWorkplace);
                if (result > 0)
                {
                    return Ok(OpreationResultMessageFactory<EmployeeWorkplacesT>.CreateSuccessResponse(employeeWorkplace));
                }
                else
                {
                    return BadRequest(OpreationResultMessageFactory<EmployeeWorkplacesT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<EmployeeWorkplacesT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteWorkplace/{employeeWorkplaceId}")]
        public async Task<ActionResult<OpreationResultMessage<EmployeeWorkplacesT>>> DeleteWorkplace(int employeeWorkplaceId)
        {
            try
            {
                var result = await employeeService.DeleteWorkplace(employeeWorkplaceId);
                if (result > 0)
                {
                    return Ok(OpreationResultMessageFactory<EmployeeWorkplacesT>.CreateSuccessResponse());
                }
                else
                {
                    return BadRequest(OpreationResultMessageFactory<EmployeeWorkplacesT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<EmployeeWorkplacesT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetWorkplaceList/{employeeId}")]
        public async Task<ActionResult<OpreationResultMessage<List<EmployeeWorkplacesT>>>> GetWorkplaceList(string employeeId)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeId))
                {
                    return BadRequest(OpreationResultMessageFactory<List<EmployeeWorkplacesT>>.CreateModelNotValidResponse("EmployeeId can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                var employeeWorkspaceList = await employeeService.GetWorkplaceList(employeeId);
                if (employeeWorkspaceList.HasItem())
                {
                    return Ok(OpreationResultMessageFactory<List<EmployeeWorkplacesT>>.CreateSuccessResponse(employeeWorkspaceList));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<List<EmployeeWorkplacesT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<DepartmentEmployeeT>>> AddDepartment(DepartmentEmployeeT departmentEmployee)
        {
            try
            {
                if (departmentEmployee == null)
                {
                    return BadRequest(OpreationResultMessageFactory<DepartmentEmployeeT>.CreateModelNotValidResponse("DepartmentEmployee can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                var result = await employeeService.AddDepartment(departmentEmployee);
                if (result > 0)
                {
                    return Ok(OpreationResultMessageFactory<DepartmentEmployeeT>.CreateSuccessResponse(departmentEmployee));
                }
                else
                {
                    return BadRequest(OpreationResultMessageFactory<DepartmentEmployeeT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<DepartmentEmployeeT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteDepartment/{departmentEmployeeId}")]
        public async Task<ActionResult<OpreationResultMessage<DepartmentEmployeeT>>> DeleteDepartment(int departmentEmployeeId)
        {
            try
            {
                var result = await employeeService.DeleteDepartment(departmentEmployeeId);
                if (result > 0)
                {
                    return Ok(OpreationResultMessageFactory<DepartmentEmployeeT>.CreateSuccessResponse());
                }
                else
                {
                    return BadRequest(OpreationResultMessageFactory<DepartmentEmployeeT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<DepartmentEmployeeT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetDepartmentList/{employeeId}")]
        public async Task<ActionResult<OpreationResultMessage<List<DepartmentEmployeeT>>>> GetDepartmentList(string employeeId)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeId))
                {
                    return BadRequest(OpreationResultMessageFactory<List<DepartmentEmployeeT>>.CreateModelNotValidResponse("EmployeeId can't be null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                var departmentList = await employeeService.GetDepartmentList(employeeId);
                if (departmentList.HasItem())
                {
                    return Ok(OpreationResultMessageFactory<List<DepartmentEmployeeT>>.CreateSuccessResponse(departmentList));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<List<DepartmentEmployeeT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<EmployeeT>>> Update(EmployeeT employee)
        {
            var result = await employeeService.UpdateAsync(employee);
            if (result > 0)
            {
                return Ok(OpreationResultMessageFactory<EmployeeT>.CreateSuccessResponse(employee));
            }
            else
            {
                return Ok(OpreationResultMessageFactory<EmployeeT>.CreateErrorResponse());
            }
        }

        [HttpGet("GetInfo/{employeeId}")]
        public async Task<ActionResult<EmployeeT>> GetInfo(string employeeId)
        {
            var employee = await employeeService.Get(employeeId);
            //employee.EmployeeWorkplacesT = employee.EmployeeWorkplacesT;
            if(employee == null)
            {
                return Ok(new { Message = $"Employee {employeeId} Not Found" });
            }
            return Ok(employee);
        }

        [HttpGet("Get/{employeeId}")]
        public async Task<ActionResult<OpreationResultMessage<EmployeeT>>> Get(string employeeId)
        {
            try
            {
                var employee = await employeeService.Get(employeeId);
                if (employee == null)
                {
                    return NoContent();
                }
                return Ok(OpreationResultMessageFactory<EmployeeT>.CreateSuccessResponse(employee));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<EmployeeT>.CreateExceptionResponse(ex));
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
            if(orderCount > 0)
            {
                employeeDayStatusDto.Status = "HaveOrders";
            }
            else
            {
                employeeDayStatusDto.Status = "Free";
            }
            return employeeDayStatusDto;
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


        [HttpGet]
        public async Task<ActionResult<List<AppEmployeeDto>>> GetAppList(DateTime selectedDate, int? clientId = null,  int? departmentId = null, int? cityId = null, int? branchId = null)
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
                     cart = await commonService.GetClientCartAsync(clientId);
                     departmentId = cart.DepartmentId;
                }
                var employeeList = await employeeService.GetListAsync(departmentId, branchId, true);
                var list = employeeList.Select(d => new AppEmployeeDto
                {
                    Id = d.EmployeeId,
                    Name = d.EmployeeName,
                    Image = d.EmployeeImageUrl,
                    Rate = new Random().Next(5),
                    IsFavourite = false,
                }).ToList();
                employeeList.ForEach(d => d.EmployeeWorkplacesT = null);
                employeeList.ForEach(d => d.LoginT = null);
                employeeList.ForEach(d => d.DepartmentEmployeeT = null);
                return Ok(OpreationResultMessageFactory<List<AppEmployeeDto>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<List<AppEmployeeDto>>.CreateExceptionResponse(ex));
            }
        }
    }
}
