using App.Global.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class EmployeeRequestController : APIBaseAuthorizeController
    {
        private readonly IEmployeeRequestService employeeRequestService;

        public EmployeeRequestController(IEmployeeRequestService employeeRequestService, CommonService commonService) : base(commonService)
        {
            this.employeeRequestService = employeeRequestService;
        }

        [HttpGet("GetFreeList")]
        public async Task<ActionResult<Result<List<EmployeeT>>>> GetFreeList(DateTime dateTime, int departmentId, int branchId)
        {
            try
            {
                var list = await employeeRequestService.GetFreeEmployeeListByBranch(dateTime, departmentId, branchId);
                return Ok(ResultFactory<List<EmployeeT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<EmployeeT>>.CreateExceptionResponse(ex));
            }
        }
    }
}
