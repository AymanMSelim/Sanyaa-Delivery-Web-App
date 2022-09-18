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

        public EmployeeAccountController(IEmployeeAppAccountService appAccountService)
        {
            this.appAccountService = appAccountService;
        }

        [HttpGet("Get/{employeeId}")]
        public async Task<ActionResult<OpreationResultMessage<LoginT>>> Get(string employeeId)
        {
            try
            {
                var login = await appAccountService.Get(employeeId);
                if (login == null)
                {
                    return NoContent();
                }
                return Ok(OpreationResultMessageFactory<LoginT>.CreateSuccessResponse(login));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<LoginT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<LoginT>>> Update(LoginT login)
        {
            try
            {
                var result = await appAccountService.Update(login);
                if (result > 0)
                {
                    return Ok(OpreationResultMessageFactory<LoginT>.CreateSuccessResponse(login));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<LoginT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<LoginT>.CreateExceptionResponse(ex));
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
                IsEnabled = account.LoginAccountState != 0,
                Message = account.LoginAccountDeactiveMessage
            });
        }

    }
}
