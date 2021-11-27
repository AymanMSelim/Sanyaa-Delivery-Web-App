using App.Global.DTOs;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class WorkingAreaController : APIBaseController
    {
        private readonly IWorkingAreaService workingAreaService;

        public WorkingAreaController(IWorkingAreaService workingAreaService)
        {
            this.workingAreaService = workingAreaService;
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<HttpResponseDto<WorkingAreaT>>> Get(int id)
        {
            try
            {
                var result = await workingAreaService.Get(id);
                if (result == null)
                {
                    return NotFound(HttpResponseDtoFactory<WorkingAreaT>.CreateNotFoundResponse());
                }
                return Ok(HttpResponseDtoFactory<WorkingAreaT>.CreateSuccessResponse(result));

            }
            catch (Exception ex)
            {
                return BadRequest(HttpResponseDtoFactory<WorkingAreaT>.CreateExceptionResponse(ex));
            }

        }

        [HttpGet("GetList")]
        public async Task<ActionResult<HttpResponseDto<List<WorkingAreaT>>>> GetList(string govName, string cityName, string regionName)
        {
            try
            {
                var result = await workingAreaService.GetList(govName, cityName, regionName);
                if (result == null)
                {
                    return BadRequest(HttpResponseDtoFactory<List<WorkingAreaT>>.CreateErrorResponse());
                }
                if(result.Count == 0)
                {
                    return NotFound(HttpResponseDtoFactory<List<WorkingAreaT>>.CreateNotFoundResponse());
                }
                return Ok(HttpResponseDtoFactory<List<WorkingAreaT>>.CreateSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(HttpResponseDtoFactory<WorkingAreaT>.CreateExceptionResponse(ex));
            }

        }

        [HttpGet("GetGovList/{searchValue}")]
        public async Task<ActionResult<HttpResponseDto<List<ValueWithIdDto>>>> GetGovList(string searchValue)
        {
            try
            {
                var result = await workingAreaService.GetGovList(searchValue);
                if (result == null)
                {
                    return BadRequest(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateErrorResponse());
                }
                if(result.Count == 0)
                {
                    return NotFound(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateNotFoundResponse());
                }
                return Ok(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetCityList/{searchValue}")]
        public async Task<ActionResult<HttpResponseDto<List<ValueWithIdDto>>>> GetCityList(string searchValue)
        {
            try
            {
                var result = await workingAreaService.GetCityList(searchValue);
                if (result == null)
                {
                    return BadRequest(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateErrorResponse());
                }
                if (result.Count == 0)
                {
                    return NotFound(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateNotFoundResponse());
                }
                return Ok(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetRegionList/{searchValue}")]
        public async Task<ActionResult<HttpResponseDto<List<ValueWithIdDto>>>> GetRegionList(string searchValue)
        {
            try
            {
                var result = await workingAreaService.GetRegionList(searchValue);
                if (result == null)
                {
                    return BadRequest(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateErrorResponse());
                }
                if (result.Count == 0)
                {
                    return NotFound(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateNotFoundResponse());
                }
                return Ok(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetCityByGovList")]
        public async Task<ActionResult<HttpResponseDto<List<ValueWithIdDto>>>> GetCityByGovList(string govName, string searchValue)
        {
            try
            {
                var result = await workingAreaService.GetCityByGovList(govName, searchValue);
                if (result == null)
                {
                    return BadRequest(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateErrorResponse());
                }
                if (result.Count == 0)
                {
                    return NotFound(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateNotFoundResponse());
                }
                return Ok(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetRegionByCityList")]
        public async Task<ActionResult<HttpResponseDto<List<ValueWithIdDto>>>> GetRegionByCityList(string cityName, string searchValue)
        {
            try
            {
                var result = await workingAreaService.GetRegionByCityList(cityName, searchValue);
                if (result == null)
                {
                    return BadRequest(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateErrorResponse());
                }
                if (result.Count == 0)
                {
                    return NotFound(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateNotFoundResponse());
                }
                return Ok(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<HttpResponseDto<WorkingAreaT>>> Add(WorkingAreaT workingArea)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(HttpResponseDtoFactory<WorkingAreaT>.CreateErrorResponseMessage("InvalidData", App.Global.Eumns.ResponseStatusCode.InvalidData));
                }

                var result = await workingAreaService.Add(workingArea);
                if (result > 0)
                {
                    return Created($"https://{HttpContext.Request.Host}/api/address/get/{workingArea.WorkingAreaId}",
                        HttpResponseDtoFactory<WorkingAreaT>.CreateSuccessResponse(workingArea, App.Global.Eumns.ResponseStatusCode.RecordAddedSuccessfully)
                        );
                }
                else
                {
                    return BadRequest(HttpResponseDtoFactory<WorkingAreaT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(HttpResponseDtoFactory<List<ValueWithIdDto>>.CreateExceptionResponse(ex));
            }

        }
    
    }
}
