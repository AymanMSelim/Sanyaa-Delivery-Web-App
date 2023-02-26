using App.Global.DTOs;
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
    public class BranchController : APIBaseAuthorizeController
    {
        private readonly IBranchService branchService;

        public BranchController(IBranchService branchService)
        {
            this.branchService = branchService;
        }
        [HttpPost("Add")]
        public async Task<ActionResult<Result<BranchT>>> Add(BranchT branch)
        {
            try
            {
                var affectedResult = await branchService.AddAsync(branch);
                if(affectedResult > 0)
                {
                    return Ok(ResultFactory<BranchT>.CreateSuccessResponse(branch));
                }
                else
                {
                    return Ok(ResultFactory<BranchT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<BranchT>(ex));
            }
        }

        [HttpPost("Update")]
        public async Task<ActionResult<Result<BranchT>>> Update(BranchT branch)
        {
            try
            {
                var affectedResult = await branchService.UpdateAsync(branch);
                if (affectedResult > 0)
                {
                    return Ok(ResultFactory<BranchT>.CreateSuccessResponse(branch));
                }
                else
                {
                    return Ok(ResultFactory<BranchT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<BranchT>(ex));
            }
        }

        [HttpGet("Get/{branchId}")]
        public async Task<ActionResult<Result<BranchT>>> Get(int branchId)
        {
            try
            {
                var branch = await branchService.GetAsync(branchId);
                return Ok(ResultFactory<BranchT>.CreateSuccessResponse(branch));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<BranchT>(ex));
            }
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<Result<List<BranchT>>>> GetList()
        {
            try
            {
                var branchList = await branchService.GetListAsync();
                return Ok(ResultFactory<List<BranchT>>.CreateSuccessResponse(branchList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<BranchT>>(ex));
            }
        }
    }
}
