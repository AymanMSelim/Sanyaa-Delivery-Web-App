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

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<AppSettingT>>>> GetAppSettingList()
        {
            try
            {
                var list = await appSettingService.GetListAsync();
                if (list.IsNotNull())
                {
                    var appList = list.Where(d => d.IsAppSetting == null || (d.IsAppSetting.HasValue && d.IsAppSetting.Value)).ToList();
                    return Ok(OpreationResultMessageFactory<List<AppSettingT>>.CreateSuccessResponse(appList));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<List<AppSettingT>>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<AppSettingT>.CreateExceptionResponse(ex));
            }
        }
    }
}
