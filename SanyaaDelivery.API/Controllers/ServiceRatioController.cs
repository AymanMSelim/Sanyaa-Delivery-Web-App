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
    public class ServiceRatioController : APIBaseAuthorizeController
    {
        private readonly IServiceRatioService serviceRatioService;

        public ServiceRatioController(IServiceRatioService serviceRatioService)
        {
            this.serviceRatioService = serviceRatioService;
        }
        #region ServiceRatio
        [HttpGet("Get/{serviceRatioId}")]
        public async Task<ActionResult<Result<ServiceRatioT>>> Get(int serviceRatioId)
        {
            try
            {
                var serviceRatio = await serviceRatioService.GetAsync(serviceRatioId);
                return base.Ok(ResultFactory<ServiceRatioT>.CreateSuccessResponse(serviceRatio, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ServiceRatioT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Result<ServiceRatioT>>> Add(ServiceRatioT serviceRatio)
        {
            try
            {
                var affectedRows = await serviceRatioService.AddAsync(serviceRatio);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<ServiceRatioT>.CreateSuccessResponse(serviceRatio, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<ServiceRatioT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ServiceRatioT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Update")]
        public async Task<ActionResult<Result<ServiceRatioT>>> Update(ServiceRatioT serviceRatio)
        {
            try
            {
                var affectedRows = await serviceRatioService.UpdateAsync(serviceRatio);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<ServiceRatioT>.CreateSuccessResponse(serviceRatio, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<ServiceRatioT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ServiceRatioT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Delete/{serviceRatioId}")]
        public async Task<ActionResult<Result<ServiceRatioT>>> Delete(int serviceRatioId)
        {
            try
            {
                var affectedRows = await serviceRatioService.DeletetAsync(serviceRatioId);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<ServiceRatioT>.CreateSuccessResponse(null, App.Global.Enums.ResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<ServiceRatioT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ServiceRatioT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<Result<List<ServiceRatioT>>>> GetList(string description = null)
        {
            try
            {
                var list = await serviceRatioService.GetListAsync(description);
                return Ok(ResultFactory<List<ServiceRatioT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ServiceRatioT>.CreateExceptionResponse(ex));
            }
        }
        #endregion

        #region ServiceRatioDetails
        [HttpGet("GetDetail/{serviceRatioDetailsId}")]
        public async Task<ActionResult<Result<ServiceRatioDetailsT>>> GetDetail(int serviceRatioDetailsId)
        {
            try
            {
                var serviceRatioDetails = await serviceRatioService.GetDetailsAsync(serviceRatioDetailsId);
                return base.Ok(ResultFactory<ServiceRatioDetailsT>.CreateSuccessResponse(serviceRatioDetails, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ServiceRatioDetailsT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddDetail")]
        public async Task<ActionResult<Result<ServiceRatioDetailsT>>> AddDetail(ServiceRatioDetailsT serviceRatioDetails)
        {
            try
            {
                var affectedRows = await serviceRatioService.AddDetailAsync(serviceRatioDetails);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<ServiceRatioDetailsT>.CreateSuccessResponse(serviceRatioDetails, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<ServiceRatioDetailsT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ServiceRatioDetailsT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateDetail")]
        public async Task<ActionResult<Result<ServiceRatioDetailsT>>> UpdateDetail(ServiceRatioDetailsT serviceRatioDetails)
        {
            try
            {
                var affectedRows = await serviceRatioService.UpdateDetailAsync(serviceRatioDetails);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<ServiceRatioDetailsT>.CreateSuccessResponse(serviceRatioDetails, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<ServiceRatioDetailsT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ServiceRatioDetailsT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteDetail/{serviceRatioDetailsId}")]
        public async Task<ActionResult<Result<ServiceRatioDetailsT>>> DeleteDetail(int serviceRatioDetailsId)
        {
            try
            {
                var affectedRows = await serviceRatioService.DeletetDetailAsync(serviceRatioDetailsId);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<ServiceRatioDetailsT>.CreateSuccessResponse(null, App.Global.Enums.ResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<ServiceRatioDetailsT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ServiceRatioDetailsT>.CreateExceptionResponse(ex));
            }
        }
        [HttpGet("GetDetailsList")]
        public async Task<ActionResult<Result<List<ServiceRatioDetailsT>>>> GetDetailsList(int? serviceRatioId = null)
        {
            try
            {
                var list = await serviceRatioService.GetDetailsListAsync(serviceRatioId);
                return Ok(ResultFactory<List<ServiceRatioDetailsT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ServiceRatioDetailsT>.CreateExceptionResponse(ex));
            }
        }
        #endregion
    }
}
