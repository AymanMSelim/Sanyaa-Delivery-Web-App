using App.Global.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class PaymentController : APIBaseAuthorizeController
    {
        private readonly IRequestUtilityService requestUtilityService;
        private readonly CommonService commonService;

        public PaymentController(IRequestUtilityService requestUtilityService, CommonService commonService)
        {
            this.requestUtilityService = requestUtilityService;
            this.commonService = commonService;
        }

        [HttpPost("Pay")]
        public async Task<ActionResult<Result<PaymentT>>> Pay(PayRequestDto payRequestDto)
        {
            try
            {
                int systemUserId = commonService.GetSystemUserId();
                var result = await requestUtilityService.PayAsync(payRequestDto.RequestId, systemUserId, payRequestDto.Amount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<PaymentT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("PayAll")]
        public async Task<ActionResult<Result<List<PaymentT>>>> PayAll(PayAllRequestDto payAllRequestDto)
        {
            try
            {
                int systemUserId = commonService.GetSystemUserId();
                var result = await requestUtilityService.PayAllAsync(payAllRequestDto.EmployeeId, systemUserId, payAllRequestDto.TotalAmount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<PaymentT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetNotPaidSummary")]
        public async Task<ActionResult<Result<List<EmployeeNotPaidRequestSummaryDto>>>> GetNotPaidSummary(DateTime? startTime = null,
            DateTime? endTime = null, int? departmentId = null, string employeeId = null, int? requestId = null)
        {
            try
            {
                var list = await requestUtilityService.GetNotPaidSummaryAsync(startTime, endTime, departmentId, employeeId, requestId);
                var result = ResultFactory<List<EmployeeNotPaidRequestSummaryDto>>.CreateSuccessResponse(list);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<EmployeeNotPaidRequestSummaryDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetNotPaid/{employeeId}")]
        public async Task<ActionResult<Result<List<EmployeeNotPaidRequestDto>>>> GetNotPaid(string employeeId)
        {
            try
            {
                var list = await requestUtilityService.GetNotPaidAsync(employeeId);
                var result = ResultFactory<List<EmployeeNotPaidRequestDto>>.CreateSuccessResponse(list);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<EmployeeNotPaidRequestDto>.CreateExceptionResponse(ex));
            }
        }
    }
}
