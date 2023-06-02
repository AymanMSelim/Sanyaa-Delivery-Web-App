using App.Global.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.DTOs.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
  
    public class LookupController : APIBaseAuthorizeController
    {
        private readonly ILookupService lookupService;

        public LookupController(ILookupService lookupService)
        {
            this.lookupService = lookupService;
        }


        [HttpGet("Department/{searchValue?}")]
        public async Task<ActionResult<Result<List<DepartmentLookupDto>>>> Department(string searchValue = null)
        {
            try
            {
                var list = await lookupService.Department();
                return Ok(ResultFactory<List<DepartmentLookupDto>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<DepartmentLookupDto>>(ex));
            }
        }


        [HttpGet("Governorate/{searchValue?}")]
        public async Task<ActionResult<Result<List<LookupDto>>>> Governorate(string searchValue = null)
        {
            try
            {
                var list = await lookupService.Governorate();
                return Ok(ResultFactory<List<LookupDto>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<LookupDto>>(ex));
            }
        }


        [HttpGet("City/{governorateId}")]
        public async Task<ActionResult<Result<List<LookupDto>>>> City(int governorateId)
        {
            try
            {
                var list = await lookupService.City(governorateId);
                return Ok(ResultFactory<List<LookupDto>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<LookupDto>>(ex));
            }
        }


        [HttpGet("DepartmentSub0/{departmentId}")]
        public async Task<ActionResult<Result<List<LookupDto>>>> DepartmentSub0(int departmentId)
        {
            try
            {
                var list = await lookupService.DepatmentSub0(departmentId);
                return Ok(ResultFactory<List<LookupDto>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<LookupDto>>(ex));
            }
        }


        [HttpGet("DepartmentSub1/{departmentSub0Id}")]
        public async Task<ActionResult<Result<List<LookupDto>>>> DepartmentSub1(int departmentSub0Id)
        {
            try
            {
                var list = await lookupService.DepatmentSub1(departmentSub0Id);
                return Ok(ResultFactory<List<LookupDto>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<LookupDto>>(ex));
            }
        }

        [HttpGet("Service/{departmentSub1Id}")]
        public async Task<ActionResult<Result<List<LookupDto>>>> Service(int departmentSub1Id)
        {
            try
            {
                var list = await lookupService.Service(departmentSub1Id);
                return Ok(ResultFactory<List<LookupDto>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<LookupDto>>(ex));
            }
        }
    }
}
