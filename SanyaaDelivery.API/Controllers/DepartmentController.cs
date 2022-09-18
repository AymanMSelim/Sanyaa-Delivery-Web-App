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
    public class DepartmentController : APIBaseAuthorizeController
    {
        private readonly IDepartmentService departmentService;
        private readonly IDeparmentSub0Service deparmentSub0Service;
        private readonly IDeparmentSub1Service deparmentSub1Service;

        public DepartmentController(IDepartmentService departmentService, IDeparmentSub0Service deparmentSub0Service, IDeparmentSub1Service deparmentSub1Service)
        {
            this.departmentService = departmentService;
            this.deparmentSub0Service = deparmentSub0Service;
            this.deparmentSub1Service = deparmentSub1Service;
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<DepartmentT>>> AddMainDepartment(DepartmentT department)
        {
            try
            {
                var affectedRow = await departmentService.AddAsync(department);
                if(affectedRow > 0)
                {
                    return Ok(OpreationResultMessageFactory<DepartmentT>.CreateSuccessResponse(department));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<DepartmentT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<DepartmentT>(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<DepartmentSub0T>>> AddDepartmentSub0(DepartmentSub0T department)
        {
            try
            {
                var affectedRow = await deparmentSub0Service.AddAsync(department);
                if (affectedRow > 0)
                {
                    return Ok(OpreationResultMessageFactory<DepartmentSub0T>.CreateSuccessResponse(department));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<DepartmentSub0T>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<DepartmentSub0T>(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<DepartmentSub1T>>> AddDepartmentSub1(DepartmentSub1T department)
        {
            try
            {
                var affectedRow = await deparmentSub1Service.AddAsync(department);
                if (affectedRow > 0)
                {
                    return Ok(OpreationResultMessageFactory<DepartmentSub1T>.CreateSuccessResponse(department));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<DepartmentSub1T>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<DepartmentSub1T>(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<DepartmentT>>> UpdateMainDepartment(DepartmentT department)
        {
            try
            {
                var affectedRow = await departmentService.UpdateAsync(department);
                if (affectedRow > 0)
                {
                    return Ok(OpreationResultMessageFactory<DepartmentT>.CreateSuccessResponse(department));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<DepartmentT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<DepartmentT>(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<DepartmentSub0T>>> UpdateDepartmentSub0(DepartmentSub0T department)
        {
            try
            {
                var affectedRow = await deparmentSub0Service.UpdateAsync(department);
                if (affectedRow > 0)
                {
                    return Ok(OpreationResultMessageFactory<DepartmentSub0T>.CreateSuccessResponse(department));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<DepartmentSub0T>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<DepartmentSub0T>(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<DepartmentSub1T>>> UpdateDepartmentSub1(DepartmentSub1T department)
        {
            try
            {
                var affectedRow = await deparmentSub1Service.UpdateAsync(department);
                if (affectedRow > 0)
                {
                    return Ok(OpreationResultMessageFactory<DepartmentSub1T>.CreateSuccessResponse(department));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<DepartmentSub1T>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<DepartmentSub1T>(ex));
            }
        }

        [HttpGet("GetMainDepartmentList/{departmentName?}")]
        public async Task<ActionResult<OpreationResultMessage<List<DepartmentT>>>> GetMainDepartmentList(string departmentName = null)
        {
            try
            {
                var list = await departmentService.GetListAsync();
                if (departmentName.IsNotNull())
                {
                    list = list.Where(d => d.DepartmentName.Contains(departmentName)).ToList();
                }
                return Ok(OpreationResultMessageFactory<List<DepartmentT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<DepartmentT>>(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<DepartmentSub0T>>>> GetDepartmentSub0List(int? departmentId = null, string departmentSub0Name = null)
        {
            try
            {
                var list = await deparmentSub0Service.GetListAsync();
                if (departmentId.HasValue)
                {
                    list = list.Where(d => d.DepartmentId == departmentId).ToList();
                }
                if (departmentSub0Name.IsNotNull())
                {
                    list = list.Where(d => d.DepartmentSub0.Contains(departmentSub0Name)).ToList();
                }
                return Ok(OpreationResultMessageFactory<List<DepartmentSub0T>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<DepartmentSub0T>>(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<DepartmentSub1T>>>> GetDepartmentSub1List(int? mainDepartmentId = null, int? departmentSub0Id = null, string departmentSub1Name = null)
        {
            try
            {
                var list = await deparmentSub1Service.GetListAsync(mainDepartmentId, departmentSub0Id, departmentSub1Name);
                return Ok(OpreationResultMessageFactory<List<DepartmentSub1T>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, App.Global.Logging.LogHandler.PublishExceptionReturnResponse<List<DepartmentSub1T>>(ex));
            }
        }
    }
}