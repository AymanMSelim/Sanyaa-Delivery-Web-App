using App.Global.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Global.ExtensionMethods;
using SanyaaDelivery.Application;

namespace SanyaaDelivery.API.Controllers
{
    public partial class RequestController
    {
        [HttpGet("GetEmployeeAppList")]
        public async Task<ActionResult<Result<List<AppRequestDto>>>> GetEmployeeAppList(string? employeeId = null, int? status = null)
        {
            try
            {
                var list = await requestService.GetAppList(employeeId: employeeId, status: status);
                if (list.HasItem())
                {
                    list.RemoveAll(d => d.RequestStatus == Domain.Enum.RequestStatus.Broadcast.ToString());
                    list.RemoveAll(d => d.RequestStatus == Domain.Enum.RequestStatus.WaitingApprove.ToString());
                }
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


        [HttpPost("StartRequest")]
        public async Task<ActionResult<Result<EmpAppRequestDetailsDto>>> StartRequest(IntIdDto model)
        {
            try
            {
                var employeeId = commonService.GetEmployeeId();
                if (employeeId.IsNull())
                {
                    return ResultFactory<EmpAppRequestDetailsDto>.ReturnEmployeeError();
                }
                var result = await requestUtilityService.StartRequestAsync(model.Id, employeeId);
                if (result.IsSuccess)
                {
                    var request = await requestService.GetEmpAppDetails(model.Id);
                    return Ok(ResultFactory<EmpAppRequestDetailsDto>.CreateSuccessResponse(request));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<EmpAppRequestDetailsDto>.CreateExceptionResponse(ex));
            }
        }
    }
}
