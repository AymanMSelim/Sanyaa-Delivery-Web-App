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
    
    public class VacationController : APIBaseAuthorizeController
    {
        private readonly IVacationService vacationService;
        private readonly IOperationService operationService;
        private readonly CommonService commonService;

        public VacationController(IVacationService vacationService, IOperationService operationService, CommonService commonService) : base(commonService)
        {
            this.vacationService = vacationService;
            this.operationService = operationService;
            this.commonService = commonService;
        }


        [HttpGet("GetAppIndex")]
        public async Task<ActionResult<Result<VacationIndexDto>>> GetAppIndex(string employeeId = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    employeeId = commonService.GetEmployeeId(employeeId);
                }
                if (string.IsNullOrEmpty(employeeId))
                {
                    return Ok(ResultFactory<VacationIndexDto>.ReturnEmployeeError());
                }
                var model = await vacationService.GetAppIndexAsync(employeeId);
                return Ok(ResultFactory<VacationIndexDto>.CreateSuccessResponse(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<VacationIndexDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("SwitchVacation")]
        public async Task<ActionResult<Result<AppVacationDto>>> SwitchVacation(SwitchVacationDto model)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    model.EmployeeId = commonService.GetEmployeeId(model.EmployeeId);
                }
                if (string.IsNullOrEmpty(model.EmployeeId))
                {
                    return Ok(ResultFactory<AppVacationDto>.ReturnEmployeeError());
                }
                var result = await vacationService.SwitchAsync(model.VacationId, new Domain.Models.VacationT 
                {
                    Day = model.Day,
                    EmployeeId = model.EmployeeId,
                    SystemUserId = commonService.GetSystemUserId(),
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppVacationDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("SwitchOpenVacation")]
        public async Task<ActionResult<Result<object>>> SwitchOpenVacation(StringIdDto idDto)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    idDto.Id = commonService.GetEmployeeId(idDto.Id);
                }
                if (string.IsNullOrEmpty(idDto.Id))
                {
                    return Ok(ResultFactory<object>.ReturnEmployeeError());
                }
                var affectedRows = await operationService.SwitchOpenVacationAsync(idDto.Id);
                return Ok(ResultFactory<object>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }
    }
}
