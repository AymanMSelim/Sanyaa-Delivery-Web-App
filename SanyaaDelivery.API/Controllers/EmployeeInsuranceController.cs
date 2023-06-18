using App.Global.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    
    public class EmployeeInsuranceController : APIBaseAuthorizeController
    {
        private readonly IEmployeeInsuranceService employeeInsuranceService;
        private readonly CommonService commonService;

        public EmployeeInsuranceController(IEmployeeInsuranceService employeeInsuranceService, CommonService commonService) : base(commonService)
        {
            this.employeeInsuranceService = employeeInsuranceService;
            this.commonService = commonService;
        }

        [HttpGet("GetIndex/{employeeId?}")]
        public async Task<ActionResult<Result<EmployeeInsuranceIndexDto>>> GetIndex(string employeeId = null)
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
                var model = await employeeInsuranceService.GetIndexAsync(employeeId);
                return Ok(ResultFactory<EmployeeInsuranceIndexDto>.CreateSuccessResponse(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<EmployeeInsuranceIndexDto>.CreateExceptionResponse(ex));
            }
        }
    }
}
