using App.Global.DTOs;
using App.Global.ExtensionMethods;
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
    public class ServiceController : APIBaseAuthorizeController
    {
        private readonly IServiceService serviceService;
        private readonly CommonService commonService;
        private readonly IFavouriteServiceService favouriteService;

        public ServiceController(IServiceService serviceService, CommonService commonService, IFavouriteServiceService favouriteService)
        {
            this.serviceService = serviceService;
            this.commonService = commonService;
            this.favouriteService = favouriteService;
        }

        [HttpGet("GetById/{serviceId}")]
        public async Task<ActionResult<OpreationResultMessage<ServiceT>>> GetById(int serviceId)
        {
            try
            {
                var service = await serviceService.GetAsync(serviceId);
                return Ok(OpreationResultMessageFactory<ServiceT>.CreateSuccessResponse(service));

            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ServiceT>(ex));
            }
        }

        [HttpGet("GetByName/{serviceName}")]
        public async Task<ActionResult<OpreationResultMessage<List<ServiceT>>>> GetByName(string serviceName)
        {
            try
            {
                var serviceList = await serviceService.GetAsync(serviceName);
                return Ok(OpreationResultMessageFactory<List<ServiceT>>.CreateSuccessResponse(serviceList));

            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ServiceT>(ex));
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<OpreationResultMessage<ServiceT>>> Add(ServiceT service)
        {
            try
            {
                int affectedRecord = await serviceService.AddAsync(service);
                if (affectedRecord > 0)
                {
                    return Ok(OpreationResultMessageFactory<ServiceT>.CreateSuccessResponse(service, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return BadRequest(OpreationResultMessageFactory<ServiceT>.CreateErrorResponse());
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ServiceT>(ex));
            }
        }

        [HttpPost("Update")]
        public async Task<ActionResult<OpreationResultMessage<ServiceT>>> Update(ServiceT service)
        {
            try
            {
                int affectedRecord = await serviceService.UpdateAsync(service);
                if (affectedRecord > 0)
                {
                    return Ok(OpreationResultMessageFactory<ServiceT>.CreateSuccessResponse(service, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return BadRequest(OpreationResultMessageFactory<ServiceT>.CreateErrorResponse());
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ServiceT>(ex));
            }
        }

        [HttpPost("Delete")]
        public async Task<ActionResult<OpreationResultMessage<ServiceT>>> Delete(int serviceId)
        {
            try
            {
                var affectedRecord = await serviceService.DeleteAsync(serviceId);
                if (affectedRecord > 0)
                {
                    return Ok(OpreationResultMessageFactory<ServiceT>.CreateSuccessResponse(null, App.Global.Enums.OpreationResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return BadRequest(OpreationResultMessageFactory<ServiceT>.CreateErrorResponse());
                }

            }
            catch (Exception ex)
            {
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ServiceT>(ex);
            }
        }

        [HttpGet("GetListByDepartmentSub1Id/{departmentSub1Id}")]
        public async Task<ActionResult<OpreationResultMessage<List<ServiceT>>>> GetListByDepartmentSub1Id(int departmentSub1Id)
        {
            try
            {
                var list = await serviceService.GetListByDepartmentSub1Async(departmentSub1Id);
                return Ok(OpreationResultMessageFactory<List<ServiceT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ServiceT>(ex));
            }
          
        }

        [HttpGet("GetListByDepartmentSub0Id/{departmentSub0Id}")]
        public async Task<ActionResult<OpreationResultMessage<List<ServiceT>>>> GetListByDepartmentSub0Id(int departmentSub0Id)
        {
            try
            {
                var list = await serviceService.GetListByDeparmentSub0Async(departmentSub0Id);
                return Ok(OpreationResultMessageFactory<List<ServiceT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ServiceT>(ex));
            }
        }

        [HttpGet("GetListByMainDepartmentId/{mainDepartmentId}")]
        public async Task<ActionResult<OpreationResultMessage<List<ServiceT>>>> GetListByMainDepartmentId(int mainDepartmentId)
        {
            try
            {
                var list = await serviceService.GetListByMainDeparmentAsync(mainDepartmentId);
                return Ok(OpreationResultMessageFactory<List<ServiceT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ServiceT>(ex));
            }
        }

        [HttpGet("GetOfferListByDepartmentSub1Id/{departmentSub1Id}")]
        public async Task<ActionResult<OpreationResultMessage<List<ServiceT>>>> GetOfferListByDepartmentSub1Id(int departmentSub1Id)
        {
            try
            {
                var list = await serviceService.GetOfferListByDepartmentSub1Async(departmentSub1Id);
                return Ok(OpreationResultMessageFactory<List<ServiceT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ServiceT>(ex));
            }
        }

        [HttpGet("GetOfferListByDepartmentSub0Id/{departmentSub0Id}")]
        public async Task<ActionResult<OpreationResultMessage<List<ServiceT>>>> GetOfferListByDepartmentSub0Id(int departmentSub0Id)
        {
            try
            {
                var list = await serviceService.GetOfferListByDeparmentSub0Async(departmentSub0Id);
                return Ok(OpreationResultMessageFactory<List<ServiceT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ServiceT>(ex));
            }
          
        }

        [HttpGet("GetOfferListByMainDeparment/{mainDepartmentId}")]
        public async Task<ActionResult<OpreationResultMessage<List<ServiceT>>>> GetOfferListByMainDeparment(int mainDepartmentId)
        {
            try
            {
                var list = await serviceService.GetOfferListByMainDeparmentAsync(mainDepartmentId);
                return Ok(OpreationResultMessageFactory<List<ServiceT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ServiceT>(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<FavouriteServiceT>>> FavouriteSwitch(int serviceId, int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId();
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<FavouriteServiceT>.CreateErrorResponseMessage("Client id is null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                var service = await serviceService.GetAsync(serviceId);
                if (service.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<FavouriteServiceT>.CreateNotFoundResponse("Service is not found"));
                }
                var affectedRow = await favouriteService.FavouriteSwitch(clientId.Value, serviceId);
                if(affectedRow > 0)
                {
                    return Ok(OpreationResultMessageFactory<FavouriteServiceT>.CreateSuccessResponse());
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<FavouriteServiceT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<FavouriteServiceT>(ex));     
            }
        }

        [HttpGet("GetFavouriteList/{clientId?}")]
        public async Task<ActionResult<OpreationResultMessage<List<ServiceCustom>>>> GetFavouriteList(int? clientId = null)
        {
            try
            {
                clientId = commonService.GetClientId();
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<List<ServiceCustom>>.CreateErrorResponseMessage("Client id is null", App.Global.Enums.OpreationResultStatusCode.NullableObject));
                }
                var list = await favouriteService.GetListAsync(clientId.Value, true);
                if (list.HasItem())
                {
                    var serviceList = list.Select(d => d.Service).ToList();
                    serviceList.ForEach(d => d.FavouriteServiceT = null);
                    var favCustomServiceList = await serviceService.ConvertServiceToCustomMultiDepartment(serviceList, clientId.Value);
                    return Ok(OpreationResultMessageFactory<List<ServiceCustom>>.CreateSuccessResponse(favCustomServiceList));
                }
                return Ok(OpreationResultMessageFactory<List<ServiceCustom>>.CreateSuccessResponse(null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<ServiceCustom>>(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<ServiceCustom>>>> GetServiceList(int? clientId = null, int? mainDepartmentId = null, int? departmentSub0Id = null, int? departmentSub1Id = null, bool? getOffer = null, string source = null)
        {
            try
            {
                if (commonService.IsViaApp())
                {
                    clientId = commonService.GetClientId();
                }
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<List<ServiceCustom>>.CreateErrorResponse(null, App.Global.Enums.OpreationResultStatusCode.InvalidData, "Client id is null"));
                }
                if (mainDepartmentId.IsNull() && departmentSub0Id.IsNull() && departmentSub1Id.IsNull())
                {
                    return  Ok(OpreationResultMessageFactory<List<ServiceCustom>>.CreateErrorResponse(null, App.Global.Enums.OpreationResultStatusCode.InvalidData, "All parameters is null"));
                }
                var list = await serviceService.GetCustomServiceList(clientId.Value, mainDepartmentId, departmentSub0Id, departmentSub1Id, getOffer);
                return Ok(OpreationResultMessageFactory<List<ServiceCustom>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ServiceCustom>(ex));
            }

        }
    }
}
