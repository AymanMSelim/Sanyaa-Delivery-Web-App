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
    public class ServiceController : APIBaseAuthorizeController
    {
        private readonly IServiceService serviceService;

        public ServiceController(IServiceService serviceService)
        {
            this.serviceService = serviceService;
        }

        [HttpGet("GetById/{serviceId}")]
        public async Task<ActionResult<HttpResponseDto<ServiceT>>> GetById(int serviceId)
        {
            try
            {
                var service = await serviceService.GetAsync(serviceId);
                return Ok(HttpResponseDtoFactory<ServiceT>.CreateSuccessResponse(service));

            }
            catch (Exception ex)
            {
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ServiceT>(ex);
            }
        }

        [HttpGet("GetByName/{serviceName}")]
        public async Task<ActionResult<HttpResponseDto<List<ServiceT>>>> GetByName(string serviceName)
        {
            try
            {
                var serviceList = await serviceService.GetAsync(serviceName);
                return Ok(HttpResponseDtoFactory<List<ServiceT>>.CreateSuccessResponse(serviceList));

            }
            catch (Exception ex)
            {
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<ServiceT>>(ex);
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<HttpResponseDto<ServiceT>>> Add(ServiceT service)
        {
            try
            {
                var result = await serviceService.AddAsync(service);
                var newService = await serviceService.GetAsync(service.ServiceId);
                return Ok(HttpResponseDtoFactory<ServiceT>.CreateSuccessResponse(service, App.Global.Eumns.ResponseStatusCode.RecordAddedSuccessfully));

            }
            catch (Exception ex)
            {
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ServiceT>(ex);
            }
        }

        [HttpPost("Update")]
        public async Task<ActionResult<HttpResponseDto<ServiceT>>> Update(ServiceT service)
        {
            try
            {
                var result = await serviceService.UpdateAsync(service);
                var updatedService = await serviceService.GetAsync(service.ServiceId);
                return Ok(HttpResponseDtoFactory<ServiceT>.CreateSuccessResponse(updatedService, App.Global.Eumns.ResponseStatusCode.RecordUpdatedSuccessfully));

            }
            catch (Exception ex)
            {
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<ServiceT>(ex);
            }
        }

        [HttpPost("Delete")]
        public async Task<ActionResult<HttpResponseDto<string>>> Delete(int serviceId)
        {
            try
            {
                var updatedService = await serviceService.DeleteAsync(serviceId);
                return Ok(HttpResponseDtoFactory<string>.CreateSuccessResponse("Success", App.Global.Eumns.ResponseStatusCode.RecordDeletedSuccessfully));

            }
            catch (Exception ex)
            {
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<string>(ex);
            }
        }

        [HttpGet("GetListByDepartmentSub1Id/{departmentSub1Id}")]
        public async Task<ActionResult<HttpResponseDto<List<ServiceT>>>> GetListByDepartmentSub1Id(int departmentSub1Id)
        {
            try
            {
                var list = await serviceService.GetListByDepartmentSub1Async(departmentSub1Id);
                return Ok(HttpResponseDtoFactory<List<ServiceT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<ServiceT>>(ex);
            }
          
        }

        [HttpGet("GetListByDepartmentSub0Id/{departmentSub0Id}")]
        public async Task<ActionResult<HttpResponseDto<List<ServiceT>>>> GetListByDepartmentSub0Id(int departmentSub0Id)
        {
            try
            {
                var list = await serviceService.GetListByDeparmentSub0Async(departmentSub0Id);
                return Ok(HttpResponseDtoFactory<List<ServiceT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<ServiceT>>(ex);
            }
        }

        [HttpGet("GetListByMainDepartmentId/{mainDepartmentId}")]
        public async Task<ActionResult<HttpResponseDto<List<ServiceT>>>> GetListByMainDepartmentId(int mainDepartmentId)
        {
            try
            {
                var list = await serviceService.GetListByMainDeparmentAsync(mainDepartmentId);
                return Ok(HttpResponseDtoFactory<List<ServiceT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<ServiceT>>(ex);
            }
        }

        [HttpGet("GetOfferListByDepartmentSub1Id/{departmentSub1Id}")]
        public async Task<ActionResult<HttpResponseDto<List<ServiceT>>>> GetOfferListByDepartmentSub1Id(int departmentSub1Id)
        {
            try
            {
                var list = await serviceService.GetOfferListByDepartmentSub1Async(departmentSub1Id);
                return Ok(HttpResponseDtoFactory<List<ServiceT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<ServiceT>>(ex);
            }
        }

        [HttpGet("GetOfferListByDepartmentSub0Id/{departmentSub0Id}")]
        public async Task<ActionResult<HttpResponseDto<List<ServiceT>>>> GetOfferListByDepartmentSub0Id(int departmentSub0Id)
        {
            try
            {
                var list = await serviceService.GetOfferListByDeparmentSub0Async(departmentSub0Id);
                return Ok(HttpResponseDtoFactory<List<ServiceT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<ServiceT>>(ex);
            }
          
        }

        [HttpGet("GetOfferListByMainDeparment/{mainDepartmentId}")]
        public async Task<ActionResult<HttpResponseDto<List<ServiceT>>>> GetOfferListByMainDeparment(int mainDepartmentId)
        {
            try
            {
                var list = await serviceService.GetOfferListByMainDeparmentAsync(mainDepartmentId);
                return Ok(HttpResponseDtoFactory<List<ServiceT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<ServiceT>>(ex);
            }
        }
    }
}
