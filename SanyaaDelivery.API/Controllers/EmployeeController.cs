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

namespace SanyaaDelivery.API.Controllers
{
    [Authorize]
    public class EmployeeController : APIBaseAuthorizeController
    {
        private readonly IEmployeeService employeeService;
        private readonly IOrderService orderService;
        private readonly IEmployeeAppAccountService employeeAppAccountService;

        public EmployeeController(IEmployeeService employeeService, IOrderService orderService, IEmployeeAppAccountService employeeAppAccountService)
        {
            this.employeeService = employeeService;
            this.orderService = orderService;
            this.employeeAppAccountService = employeeAppAccountService;
        }

        [HttpPost]
        public async Task<ActionResult<HttpResponseDto<EmployeeT>>> Add(EmployeeT employee)
        {
            var result = await employeeService.AddAsync(employee);
            if(result > 0)
            {
                return Created("", HttpResponseDtoFactory<EmployeeT>.CreateSuccessResponse(employee));
            }
            else
            {
                return BadRequest(HttpResponseDtoFactory<EmployeeT>.CreateErrorResponse());
            }
        }

        [HttpPost]
        public async Task<ActionResult<HttpResponseDto<EmployeeT>>> Update(EmployeeT employee)
        {
            var result = await employeeService.UpdateAsync(employee);
            if (result > 0)
            {
                return Ok(HttpResponseDtoFactory<EmployeeT>.CreateSuccessResponse(employee));
            }
            else
            {
                return BadRequest(HttpResponseDtoFactory<EmployeeT>.CreateErrorResponse());
            }
        }

        [HttpGet("GetInfo/{employeeId}")]
        public async Task<ActionResult<EmployeeT>> GetInfo(string employeeId)
        {
            var employee = await employeeService.Get(employeeId);
            //employee.EmployeeWorkplacesT = employee.EmployeeWorkplacesT;
            if(employee == null)
            {
                return NotFound(new { Message = $"Employee {employeeId} Not Found" });
            }
            return Ok(employee);
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
                return NotFound(new { Message = $"Employee {employeeId} Not Found" });
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

    }
}
