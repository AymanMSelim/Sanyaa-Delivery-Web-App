using App.Global.DTOs;
using App.Global.ExtensionMethods;
using App.Global.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class FawryController : APIBaseController
    {
        private readonly IFawryService fawryService;
        private readonly IConfiguration configuration;
        private readonly IRequestService orderService;
        private readonly IEmployeeService employeeService;
        private readonly CommonService commonService;

        public FawryController(IFawryService fawryService, IConfiguration configuration,
            IRequestService orderService, IEmployeeService employeeService, CommonService commonService) : base(commonService)
        {
            this.fawryService = fawryService;
            this.configuration = configuration;
            this.orderService = orderService;
            this.employeeService = employeeService;
            this.commonService = commonService;
        }

        [HttpGet("GenerateRefNumber/{employeeId}")]
        public async Task<ActionResult<Result<App.Global.Models.Fawry.FawryRefNumberResponse>>> GenerateRefNumber(string employeeId, bool ignoreValidFawryRequest = false)
        {
            try
            {
                var result = await fawryService.SendAllUnpaidRequestAsync(employeeId, ignoreValidFawryRequest: ignoreValidFawryRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<App.Global.Models.Fawry.FawryRefNumberResponse>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("GenerateRefNumberForRequest")]
        public async Task<ActionResult<Result<App.Global.Models.Fawry.FawryRefNumberResponse>>> GenerateRefNumberForRequest(GenerateRefNumberForRequestDto model)
        {
            try
            {
                model.EmployeeId = commonService.GetEmployeeId(model.EmployeeId);
                if (string.IsNullOrEmpty(model.EmployeeId))
                {
                    return Ok(ResultFactory<App.Global.Models.Fawry.FawryRefNumberResponse>.ReturnEmployeeError());
                }
                var result = await fawryService.SendUnpaidRequestAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<App.Global.Models.Fawry.FawryRefNumberResponse>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("StartFarwy")]
        public async Task<ActionResult<Result<List<App.Global.Models.Fawry.FawryRefNumberResponse>>>> StartFarwy(bool ignoreValidFawryRequest = false)
        {
            try
            {
                var result = await fawryService.SendAllUnpaidRequestAsync(ignoreValidFawryRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<App.Global.Models.Fawry.FawryRefNumberResponse>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateStatus")]
        public async Task<ActionResult<Result<object>>> UpdateStatus()
        {
            try
            {
                await fawryService.UpdateStatusTask();
                return Ok(ResultFactory<object>.CreateSuccessResponse());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<App.Global.Models.Fawry.FawryRefNumberResponse>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateEmployeeValidCharge")]
        public async Task<ActionResult<Result<object>>> UpdateEmployeeValidCharge(StringIdDto idDto)
        {
            try
            {
                await fawryService.UpdateEmployeeValidChargeAsync(idDto.Id);
                return Ok(ResultFactory<object>.CreateSuccessResponse());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<App.Global.Models.Fawry.FawryRefNumberResponse>>.CreateExceptionResponse(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost("CallbackNotification")]
        public async Task<ActionResult> CallbackNotification(App.Global.Models.Fawry.FawryNotificationCallback model)
        {
            try
            {
                var result = await fawryService.CallbackNotification(model);
                if (result > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
