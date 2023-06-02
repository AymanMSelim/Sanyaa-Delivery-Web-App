﻿using App.Global.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Global.ExtensionMethods;

namespace SanyaaDelivery.API.Controllers
{
    public partial class RequestController
    {
        [HttpGet("GetEmployeeAppList")]
        public async Task<ActionResult<Result<List<AppRequestDto>>>> GetEmployeeAppList(string employeeId = null, int? status = null)
        {
            try
            {
                var list = await requestService.GetAppList(employeeId: employeeId, status: status);
                var mapList = mapper.Map<List<AppRequestDto>>(list);
                return ResultFactory<List<AppRequestDto>>.CreateSuccessResponse(mapList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<AppRequestDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetEmployeeAppRequestStatusList")]
        public async Task<ActionResult<Result<List<RequestGroupStatusDto>>>> GetEmployeeAppRequestStatusList()
        {
            try
            {
                List<RequestGroupStatusDto> statusList = null;
                var list = await requestStatusService.GetGroupListAsync();
                if (list.HasItem())
                {
                    statusList = mapper.Map<List<RequestGroupStatusDto>>(list);
                }
                return ResultFactory<List<RequestGroupStatusDto>>.CreateSuccessResponse(statusList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<RequestGroupStatusDto>>.CreateExceptionResponse(ex));
            }
        }



        [HttpGet("GetEmpAppRequestDetails/{requestId}")]
        public async Task<ActionResult<Result<EmpAppRequestDetailsDto>>> GetEmpAppRequestDetails(int requestId)
        {
            try
            {
                var employeeId = commonService.GetEmployeeId();
                var request = await requestService.GetEmpAppDetails(requestId);
                if(request.EmployeeId != employeeId)
                {
                    return ResultFactory<EmpAppRequestDetailsDto>.CreateErrorResponseMessageFD("This request not belong to this employee");
                }
                return ResultFactory<EmpAppRequestDetailsDto>.CreateSuccessResponse(request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<EmpAppRequestDetailsDto>.CreateExceptionResponse(ex));
            }
        }
    }
}
