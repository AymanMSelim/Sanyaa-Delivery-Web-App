﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpDeptController : ControllerBase
    {
        private readonly IEmpDeptService empDeptService;


        public EmpDeptController(IEmpDeptService empDeptService)
        {
            this.empDeptService = empDeptService;
        }

        [HttpGet("GetByDeptartment/{departmentName}")]
        public IActionResult GetEmpByDept(string departmentName)
        {
            var employees = empDeptService.GetEmployees(departmentName);
            if(employees == null)
            {
                return NotFound(new { Message = $"Can't find any employees in this department {departmentName}" });
            }
            return Ok(employees);
        }
    }
}
