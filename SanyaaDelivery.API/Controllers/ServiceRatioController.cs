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
        public async Task<ActionResult<OpreationResultMessage<ServiceRatioT>>> Get(int serviceRatioId)
        {
            try
            {
                var serviceRatio = await serviceRatioService.GetAsync(serviceRatioId);
                return base.Ok(OpreationResultMessageFactory<ServiceRatioT>.CreateSuccessResponse(serviceRatio, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ServiceRatioT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<ServiceRatioT>>> Add(ServiceRatioT serviceRatio)
        {
            try
            {
                var affectedRows = await serviceRatioService.AddAsync(serviceRatio);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<ServiceRatioT>.CreateSuccessResponse(serviceRatio, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<ServiceRatioT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ServiceRatioT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<ServiceRatioT>>> Update(ServiceRatioT serviceRatio)
        {
            try
            {
                var affectedRows = await serviceRatioService.UpdateAsync(serviceRatio);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<ServiceRatioT>.CreateSuccessResponse(serviceRatio, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<ServiceRatioT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ServiceRatioT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Delete/{serviceRatioId}")]
        public async Task<ActionResult<OpreationResultMessage<ServiceRatioT>>> Delete(int serviceRatioId)
        {
            try
            {
                var affectedRows = await serviceRatioService.DeletetAsync(serviceRatioId);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<ServiceRatioT>.CreateSuccessResponse(null, App.Global.Enums.OpreationResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<ServiceRatioT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ServiceRatioT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<OpreationResultMessage<List<ServiceRatioT>>>> GetList(string description = null)
        {
            try
            {
                var list = await serviceRatioService.GetListAsync(description);
                return Ok(OpreationResultMessageFactory<List<ServiceRatioT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ServiceRatioT>.CreateExceptionResponse(ex));
            }
        }
        #endregion

        #region ServiceRatioDetails
        [HttpGet("GetDetail/{serviceRatioDetailsId}")]
        public async Task<ActionResult<OpreationResultMessage<ServiceRatioDetailsT>>> GetDetail(int serviceRatioDetailsId)
        {
            try
            {
                var serviceRatioDetails = await serviceRatioService.GetDetailsAsync(serviceRatioDetailsId);
                return base.Ok(OpreationResultMessageFactory<ServiceRatioDetailsT>.CreateSuccessResponse(serviceRatioDetails, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ServiceRatioDetailsT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<ServiceRatioDetailsT>>> AddDetail(ServiceRatioDetailsT serviceRatioDetails)
        {
            try
            {
                var affectedRows = await serviceRatioService.AddDetailAsync(serviceRatioDetails);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<ServiceRatioDetailsT>.CreateSuccessResponse(serviceRatioDetails, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<ServiceRatioDetailsT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ServiceRatioDetailsT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<ServiceRatioDetailsT>>> UpdateDetail(ServiceRatioDetailsT serviceRatioDetails)
        {
            try
            {
                var affectedRows = await serviceRatioService.UpdateDetailAsync(serviceRatioDetails);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<ServiceRatioDetailsT>.CreateSuccessResponse(serviceRatioDetails, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<ServiceRatioDetailsT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ServiceRatioDetailsT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteDetail/{serviceRatioDetailsId}")]
        public async Task<ActionResult<OpreationResultMessage<ServiceRatioDetailsT>>> DeleteDetail(int serviceRatioDetailsId)
        {
            try
            {
                var affectedRows = await serviceRatioService.DeletetDetailAsync(serviceRatioDetailsId);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<ServiceRatioDetailsT>.CreateSuccessResponse(null, App.Global.Enums.OpreationResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<ServiceRatioDetailsT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ServiceRatioDetailsT>.CreateExceptionResponse(ex));
            }
        }
        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<ServiceRatioDetailsT>>>> GetDetailsList(int? serviceRatioId = null)
        {
            try
            {
                var list = await serviceRatioService.GetDetailsListAsync(serviceRatioId);
                return Ok(OpreationResultMessageFactory<List<ServiceRatioDetailsT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ServiceRatioDetailsT>.CreateExceptionResponse(ex));
            }
        }
        #endregion
    }
}
