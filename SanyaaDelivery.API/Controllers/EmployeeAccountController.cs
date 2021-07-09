using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.DTOs;
using SanyaaDelivery.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class EmployeeAccountController : APIBaseAuthorizeController
    {
        private readonly IEmployeeAppAccountService appAccountService;

        public EmployeeAccountController(IEmployeeAppAccountService appAccountService)
        {
            this.appAccountService = appAccountService;
        }

        [HttpGet("GetAccountInfo/{employeeId?}")]
        public async Task<ActionResult<EmployeeAppAccountDto>> GetAccountInfo(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId)) return BadRequest(new { Message = $"Empty Employee id" });

            var account = await appAccountService.Get(employeeId);
            if (account == null) { return NotFound(new { Message = $"Employee account {employeeId} not found" }); };

            return Ok(new EmployeeAppAccountDto
            {
                EmployeeId = employeeId,
                LastActive = account.LastActiveTimestamp,
                IsActive = account.LastActiveTimestamp > DateTime.Now.AddMinutes(-3),
                IsEnabled = account.LoginAccountState == 0 ? false : true,
                Message = account.LoginAccountDeactiveMessage
            });
        }
    }
}
