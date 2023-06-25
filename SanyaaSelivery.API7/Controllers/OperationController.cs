using App.Global.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    
    public class OperationController : APIBaseAuthorizeController
    {
        private readonly IOperationService operationService;
        private readonly IEmployeeRequestService employeeRequestService;
        private readonly CommonService commonService;

        public OperationController(IOperationService operationService, IEmployeeRequestService employeeRequestService, CommonService commonService) : base(commonService)
        {
            this.operationService = operationService;
            this.employeeRequestService = employeeRequestService;
            this.commonService = commonService;
        }

        [HttpGet("GetAppLandingIndex/{employeeId?}")]
        public async Task<ActionResult<Result<AppLandingIndexDto>>> GetAppLandingIndex(string? employeeId = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    employeeId = commonService.GetEmployeeId(employeeId);
                }
                if (string.IsNullOrEmpty(employeeId))
                {
                    return Ok(ResultFactory<AppLandingIndexDto>.ReturnEmployeeError());
                }
                var model = await operationService.GetAppLandingIndexAsync(employeeId);
                return Ok(ResultFactory<AppLandingIndexDto>.CreateSuccessResponse(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppLandingIndexDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetRequestDetails")]
        public async Task<ActionResult<Result<BroadcastRequestDetailsDto>>> GetRequestDetails(int requestId, string? employeeId = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    employeeId = commonService.GetEmployeeId(employeeId);
                }
                if (string.IsNullOrEmpty(employeeId))
                {
                    return Ok(ResultFactory<BroadcastRequestDetailsDto>.ReturnEmployeeError());
                }
                var model = await operationService.GetRequestDetailsAsync(new BroadcastRequestActionDto
                {
                    EmployeeId = employeeId,
                    RequestId = requestId
                });
                return Ok(ResultFactory<BroadcastRequestDetailsDto>.CreateSuccessResponse(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<BroadcastRequestDetailsDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("SwitchActive")]
        public async Task<ActionResult<Result<AppLandingIndexDto>>> SwitchActive(StringIdDto stringIdDto)
        {
            try
            {
                string employeeId = stringIdDto.Id;
                if (commonService.IsViaApp())
                {
                    employeeId = commonService.GetEmployeeId(employeeId);
                }
                if (string.IsNullOrEmpty(employeeId))
                {
                    return Ok(ResultFactory<AppLandingIndexDto>.ReturnEmployeeError());
                }
                var model = await operationService.SwitchActiveAsync(employeeId);
                return Ok(ResultFactory<AppLandingIndexDto>.CreateSuccessResponse(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<BroadcastRequestDetailsDto>.CreateExceptionResponse(ex));
            }
        }

        //[HttpPost("TakeBroadcastRequest")]
        //public async Task<ActionResult<Result<BroadcastRequestT>>> TakeBroadcastRequest(BroadcastRequestActionDto model)
        //{
        //    try
        //    {
        //        string employeeId = model.EmployeeId;
        //        if (commonService.IsViaApp())
        //        {
        //            employeeId = commonService.GetEmployeeId(employeeId);
        //        }
        //        if (string.IsNullOrEmpty(employeeId))
        //        {
        //            return Ok(ResultFactory<BroadcastRequestT>.ReturnEmployeeError());
        //        }
        //        var result = await operationService.TakeBroadcastRequestAsync(model);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ResultFactory<BroadcastRequestT>.CreateExceptionResponse(ex));
        //    }
        //}

        //[HttpPost("RejectBroadcastRequest")]
        //public async Task<ActionResult<Result<BroadcastRequestT>>> RejectBroadcastRequest(BroadcastRequestActionDto model)
        //{
        //    try
        //    {
        //        string employeeId = model.EmployeeId;
        //        if (commonService.IsViaApp())
        //        {
        //            employeeId = commonService.GetEmployeeId(employeeId);
        //        }
        //        if (string.IsNullOrEmpty(employeeId))
        //        {
        //            return Ok(ResultFactory<BroadcastRequestT>.ReturnEmployeeError());
        //        }
        //        var result = await operationService.RejectBroadcastRequestAsync(model);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ResultFactory<BroadcastRequestT>.CreateExceptionResponse(ex));
        //    }
        //}

        //[HttpPost("ApproveRequest")]
        //public async Task<ActionResult<Result<object>>> ApproveRequest(BroadcastRequestActionDto model)
        //{
        //    try
        //    {
        //        string employeeId = model.EmployeeId;
        //        if (commonService.IsViaApp())
        //        {
        //            employeeId = commonService.GetEmployeeId(employeeId);
        //        }
        //        if (string.IsNullOrEmpty(employeeId))
        //        {
        //            return Ok(ResultFactory<object>.ReturnEmployeeError());
        //        }
        //        var result = await operationService.ApproveRequestAsync(model);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
        //    }
        //}

        //[HttpPost("RejectRequest")]
        //public async Task<ActionResult<Result<RejectRequestT>>> RejectRequest(BroadcastRequestActionDto model)
        //{
        //    try
        //    {
        //        string employeeId = model.EmployeeId;
        //        if (commonService.IsViaApp())
        //        {
        //            employeeId = commonService.GetEmployeeId(employeeId);
        //        }
        //        if (string.IsNullOrEmpty(employeeId))
        //        {
        //            return Ok(ResultFactory<RejectRequestT>.ReturnEmployeeError());
        //        }
        //        var result = await operationService.RejectRequestAsync(model);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ResultFactory<RejectRequestT>.CreateExceptionResponse(ex));
        //    }
        //}

        [HttpPost("Accept")]
        public async Task<ActionResult<Result<object>>> Accept(BroadcastRequestActionDto model)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    model.EmployeeId = commonService.GetEmployeeId(model.EmployeeId);
                }
                if (string.IsNullOrEmpty(model.EmployeeId))
                {
                    return Ok(ResultFactory<object>.ReturnEmployeeError());
                }
                if(model.Status.ToLower() == "broadcast")
                {
                    var result = await operationService.TakeBroadcastRequestAsync(model);
                    return Ok(result.Convert(""));
                }
                else if(model.Status.ToLower() == "waitingapprove")
                {
                    var result = await operationService.ApproveRequestAsync(model);
                    return Ok(result.Convert(""));
                }
                else
                {
                    return Ok(ResultFactory<object>.CreateModelNotValidResponse("Invaid status"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Reject")]
        public async Task<ActionResult<Result<RejectRequestT>>> Reject(BroadcastRequestActionDto model)
        {
            try
            {
                string employeeId = model.EmployeeId;
                if (commonService.IsViaApp())
                {
                    employeeId = commonService.GetEmployeeId(employeeId);
                }
                if (string.IsNullOrEmpty(employeeId))
                {
                    return Ok(ResultFactory<RejectRequestT>.ReturnEmployeeError());
                }
                if (model.Status.ToLower() == "broadcast")
                {
                    var result = await operationService.RejectBroadcastRequestAsync(model);
                    return Ok(result.Convert(""));

                }
                else if (model.Status.ToLower() == "waitingapprove")
                {
                    var result = await operationService.RejectRequestAsync(model);
                    return Ok(result);
                }
                else
                {
                    var result = await operationService.RejectRequestAsync(model);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RejectRequestT>.CreateExceptionResponse(ex));
            }
        }


        [HttpPost("UpdatePreferredWorkingHour")]
        public async Task<ActionResult<Result<object>>> UpdatePreferredWorkingHour(UpdatePreferredWorkingHourDto model)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    model.EmployeeId = commonService.GetEmployeeId(model.EmployeeId);
                }
                if (string.IsNullOrEmpty(model.EmployeeId))
                {
                    return Ok(ResultFactory<object>.ReturnEmployeeError());
                }
                var affectedRows = await operationService.UpdatePreferredWorkingHourAsync(model);
                return Ok(ResultFactory<object>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("BroadcastRequest")]
        public async Task<ActionResult<Result<List<BroadcastRequestT>>>> BroadcastRequest(IntIdDto model)
        {
            try
            {
                var result = await operationService.BroadcastAsync(model.Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<BroadcastRequestT>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetFreeEmployeeList")]
        public async Task<ActionResult<Result<List<FreeEmployeeDto>>>> GetFreeEmployeeList(DateTime time, int departmentId, int branchId)
        {
            try
            {
                var list = await employeeRequestService.GetFreeEmployeeListAsync(time, departmentId, branchId);
                return Ok(ResultFactory<List<FreeEmployeeDto>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<List<FreeEmployeeDto>>.CreateExceptionResponse(ex));
            }
        }
    }
}
