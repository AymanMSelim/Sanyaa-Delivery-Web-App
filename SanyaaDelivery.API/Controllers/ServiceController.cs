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

        [HttpGet]
        public async Task<ActionResult<HttpResponseDto<List<ServiceT>>>> GetList(int departmentId)
        {
            var list = await serviceService.GetListAsync(departmentId);
            return Ok(HttpResponseDtoFactory<List<ServiceT>>.CreateSuccessResponse(list));
        }

        [HttpGet]
        public async Task<ActionResult<HttpResponseDto<List<ServiceT>>>> GetList(string departmentName, string sub0DepartmentName, string sub1DeparmetnName, string serviceName)
        {
            var list = await serviceService.GetListAsync(departmentName, sub0DepartmentName, sub1DeparmetnName, serviceName);
            return Ok(HttpResponseDtoFactory<List<ServiceT>>.CreateSuccessResponse(list));
        }

        [HttpGet]
        public async Task<ActionResult<HttpResponseDto<List<ServiceT>>>> GetOfferList(int departmentId)
        {
            var list = await serviceService.GetOfferListAsync(departmentId);
            return Ok(HttpResponseDtoFactory<List<ServiceT>>.CreateSuccessResponse(list));
        }

        [HttpGet]
        public async Task<ActionResult<HttpResponseDto<List<ServiceT>>>> GetOfferList(string departmentName, string sub0DepartmentName, string sub1DeparmetnName, string serviceName)
        {
            var list = await serviceService.GetListAsync(departmentName, sub0DepartmentName, sub1DeparmetnName, serviceName, true);
            return Ok(HttpResponseDtoFactory<List<ServiceT>>.CreateSuccessResponse(list));
        }
    }
}
