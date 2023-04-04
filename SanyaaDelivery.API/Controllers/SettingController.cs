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
    public class SettingController : APIBaseAuthorizeController
    {
        private readonly IAppSettingService appSettingService;

        public SettingController(IAppSettingService appSettingService)
        {
            this.appSettingService = appSettingService;
        }

 
        [HttpGet("GetAppSettingList")]
        public async Task<ActionResult<Result<List<AppSettingT>>>> GetAppSettingList()
        {
            try
            {
                var list = await appSettingService.GetListAsync();
                if (list.IsNotNull())
                {
                    var appList = list.Where(d => d.IsAppSetting == null || (d.IsAppSetting.HasValue && d.IsAppSetting.Value)).ToList();
                    return Ok(ResultFactory<List<AppSettingT>>.CreateSuccessResponse(appList));
                }
                else
                {
                    return Ok(ResultFactory<List<AppSettingT>>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppSettingT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("IsTechnicalSelectionAllowed")]
        public async Task<ActionResult<Result<bool>>> IsTechnicalSelectionAllowed()
        {
            try
            {
                var setting = await appSettingService.Get("TechnicianSelectionMethod");
                if (setting.IsNotNull() && setting.SettingValue.ToLower() == Domain.Enum.TechnicianSelectionType.App.ToString().ToLower())
                {
                    return Ok(ResultFactory<bool>.CreateSuccessResponse(true, App.Global.Enums.ResultStatusCode.Allowed));
                }
                else
                {
                    return Ok(ResultFactory<bool>.CreateSuccessResponse(false, App.Global.Enums.ResultStatusCode.NotAllowed));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<bool>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("IsTestMode")]
        public async Task<ActionResult<Result<bool>>> IsTestMode()
        {
            try
            {
                var setting = await appSettingService.Get("ApplicationMode");
                if (setting.IsNotNull() && setting.SettingValue.ToLower() == Domain.Enum.TechnicianSelectionType.App.ToString().ToLower())
                {
                    return Ok(ResultFactory<bool>.CreateSuccessResponse(true, App.Global.Enums.ResultStatusCode.Allowed));
                }
                else
                {
                    return Ok(ResultFactory<bool>.CreateSuccessResponse(false, App.Global.Enums.ResultStatusCode.NotAllowed));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<bool>.CreateExceptionResponse(ex));
            }
        }


        [HttpGet("GetMem")]
        public ActionResult<Result<long>> GetMem()
        {
            try
            {
                long memoryUsed = GC.GetTotalMemory(false);
                memoryUsed = memoryUsed / 1024;
                memoryUsed = memoryUsed / 1024;
                return Ok(ResultFactory<long>.CreateSuccessResponse(memoryUsed, App.Global.Enums.ResultStatusCode.NotAllowed));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<bool>.CreateExceptionResponse(ex));
            }
        }
    }
}
