using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class SettingController : APIBaseAuthorizeController
    {
        private readonly IAppSettingService appSettingService;

        public SettingController(IAppSettingService appSettingService, CommonService commonService) : base(commonService)
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
        [AllowAnonymous]
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

        [HttpGet("GetEmployeeAppRegisterPolicy")]
        public ActionResult<Result<string>> GetEmployeeAppRegisterPolicy()
        {
            try
            {
                return Ok(ResultFactory<string>.CreateSuccessResponse("Test"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<string>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetProcInfo")]
        public ActionResult<Result<object>> GetProcInfo()
        {
            try
            {
                // Get the current process
                Process currentProcess = Process.GetCurrentProcess();

                // Get the working set size of the process (in bytes)
                long workingSetBytes = currentProcess.WorkingSet64;

                // Convert the working set size to megabytes
                double workingSetMegabytes = (double)workingSetBytes / (1024 * 1024);

                float cpuUsage = currentProcess.TotalProcessorTime.Ticks / (float)Stopwatch.Frequency / Environment.ProcessorCount * 100;
                var ret = new
                {
                    Memory = $"Memory = {workingSetMegabytes} MB",
                    CPU = $"{cpuUsage}%",
                    currentProcess.StartTime,
                    currentProcess.TotalProcessorTime,
                    ThreadsCount = currentProcess.Threads.Count,
                    currentProcess.UserProcessorTime
                };
                return Ok(ResultFactory<object>.CreateSuccessResponse(ret, App.Global.Enums.ResultStatusCode.NotAllowed));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }
        }
    }
}
