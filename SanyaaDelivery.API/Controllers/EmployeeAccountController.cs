using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Global.DTOs;
using SanyaaDelivery.Domain.Models;

namespace SanyaaDelivery.API.Controllers
{
    public class EmployeeAccountController : APIBaseAuthorizeController
    {
        private readonly IEmployeeAppAccountService appAccountService;
        private readonly CommonService commonService;
        private readonly IRegisterService registerService;

        public EmployeeAccountController(IEmployeeAppAccountService appAccountService, CommonService commonService,
            IRegisterService registerService)
        {
            this.appAccountService = appAccountService;
            this.commonService = commonService;
            this.registerService = registerService;
        }

        [HttpGet("Get/{employeeId}")]
        public async Task<ActionResult<Result<LoginT>>> Get(string employeeId)
        {
            try
            {
                var login = await appAccountService.Get(employeeId);
                if (login == null)
                {
                    return NoContent();
                }
                return Ok(ResultFactory<LoginT>.CreateSuccessResponse(login));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<LoginT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Update")]
        public async Task<ActionResult<Result<LoginT>>> Update(LoginT login)
        {
            try
            {
                var result = await appAccountService.Update(login);
                if (result > 0)
                {
                    return Ok(ResultFactory<LoginT>.CreateSuccessResponse(login));
                }
                else
                {
                    return Ok(ResultFactory<LoginT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<LoginT>.CreateExceptionResponse(ex));
            }
        }


        [HttpGet("GetAccountInfo/{employeeId?}")]
        public async Task<ActionResult<EmployeeAppAccountDto>> GetAccountInfo(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId)) return Ok(new { Message = $"Empty Employee id" });

            var account = await appAccountService.Get(employeeId);
            if (account == null) { return Ok(new { Message = $"Employee account {employeeId} not found" }); };

            return Ok(new EmployeeAppAccountDto
            {
                EmployeeId = employeeId,
                LastActive = account.LastActiveTimestamp.Value,
                IsActive = account.LastActiveTimestamp > DateTime.Now.AddMinutes(-3),
                IsEnabled = account.LoginAccountState,
                Message = account.LoginAccountDeactiveMessage
            });
        }


       
    }
}
