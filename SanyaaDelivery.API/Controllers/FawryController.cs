using App.Global.DTOs;
using App.Global.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SanyaaDelivery.Application.Interfaces;
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

        public FawryController(IFawryService fawryService, IConfiguration configuration,
            IRequestService orderService, IEmployeeService employeeService)
        {
            this.fawryService = fawryService;
            this.configuration = configuration;
            this.orderService = orderService;
            this.employeeService = employeeService;
        }

        [HttpGet("GenerateRefNumber/{employeeId}")]
        public async Task<ActionResult<App.Global.Models.Fawry.FawryRefNumberResponse>> GenerateRefNumber(string employeeId, bool includeAllUnPaid)
        {
            try
            {
                var result = await fawryService.SendAllUnpaidRequestAsync(employeeId);  
                if (result != null)
                {
                    return Ok(OpreationResultMessageFactory<App.Global.Models.Fawry.FawryRefNumberResponse>.CreateSuccessResponse(result));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<App.Global.Models.Fawry.FawryRefNumberResponse>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<App.Global.Models.Fawry.FawryRefNumberResponse>.CreateExceptionResponse(ex));
            }
        }
    }
}
