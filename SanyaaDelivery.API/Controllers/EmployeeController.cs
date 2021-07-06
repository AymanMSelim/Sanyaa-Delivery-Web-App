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

    public class EmployeeController : APIBaseAuthorizeController
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet("GetInfo/{employeeId}")]
        public async Task<ActionResult<EmployeeT>> GetInfo(string employeeId)
        {
            var employee = await employeeService.Get(employeeId);
            if(employee == null)
            {
                return NotFound(new { Message = $"Employee {employeeId} Not Found" });
            }
            return Ok(employee);
        }
    }
}
