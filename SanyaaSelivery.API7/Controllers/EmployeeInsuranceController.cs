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
    
    public class EmployeeInsuranceController : APIBaseAuthorizeController
    {
        private readonly IEmployeeSubscriptionService employeeSubscriptionService;
        private readonly CommonService commonService;

        public EmployeeInsuranceController(IEmployeeSubscriptionService employeeSubscriptionService, 
            CommonService commonService) : base(commonService)
        {
            this.employeeSubscriptionService = employeeSubscriptionService;
            this.commonService = commonService;
        }

        [HttpGet("GetIndex/{employeeId?}")]
        public async Task<ActionResult<Result<EmployeeInsuranceIndexDto>>> GetIndex(string? employeeId = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    employeeId = commonService.GetEmployeeId(employeeId);
                }
                if (string.IsNullOrEmpty(employeeId))
                {
                    return Ok(ResultFactory<EmployeeInsuranceIndexDto>.ReturnEmployeeError());
                }
                var model = await employeeSubscriptionService.GetIndexAsync(employeeId);
                return Ok(ResultFactory<EmployeeInsuranceIndexDto>.CreateSuccessResponse(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<EmployeeInsuranceIndexDto>.CreateExceptionResponse(ex));
            }
        }

        
        [HttpGet("GetPaymentCustomList/{employeeId}")]
        public async Task<ActionResult<Result<List<InsurancePaymentDto>>>> GetPaymentCustomList(string employeeId)
        {
            try
            {
                var model = await employeeSubscriptionService.GetPaymentCustomListAsync(employeeId);
                return Ok(ResultFactory<List<InsurancePaymentDto>>.CreateSuccessResponse(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<InsurancePaymentDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddPayment")]
        public async Task<ActionResult<Result<InsurancePaymentT>>> AddPayment(InsurancePaymentT model)
        {
            try
            {
                var affectedRows = await employeeSubscriptionService.AddPaymentAsync(model);
                return Ok(ResultFactory<InsurancePaymentT>.CreateAffectedRowsResult(affectedRows, data: model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<InsurancePaymentT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeletePayment")]
        public async Task<ActionResult<Result<InsurancePaymentT>>> DeletePayment(IntIdDto model)
        {
            try
            {
                var affectedRows = await employeeSubscriptionService.DeletePaymentAsync(model.Id);
                return Ok(ResultFactory<InsurancePaymentT>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<InsurancePaymentT>.CreateExceptionResponse(ex));
            }
        }


    }
}
