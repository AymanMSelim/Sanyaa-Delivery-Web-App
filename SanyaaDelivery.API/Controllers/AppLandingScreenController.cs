using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class AppLandingScreenController : APIBaseAuthorizeController
    {
        private readonly IAppLandingScreenService landingScreenService;
        private readonly IServiceService serviceService;
        private readonly CommonService commonService;

        public AppLandingScreenController(IAppLandingScreenService landingScreenService, IServiceService serviceService, CommonService commonService)
        {
            this.landingScreenService = landingScreenService;
            this.serviceService = serviceService;
            this.commonService = commonService;
        }

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<AppLandingScreenItemT>>>> GetDepartmentList()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var clientId = App.Global.JWT.TokenHelper.GetReferenceId(identity);
                var list = await landingScreenService.GetDepartmentListAsync(clientId);
                list.ForEach(d => d.IsActive = d.LandingScreenItemDetailsT.Any());
                list.ForEach(d => d.ImagePath = d.ImagePath.Replace("{status}", d.IsActive.Value ? "ava" : "soon"));
                return Ok(OpreationResultMessageFactory<List<AppLandingScreenItemT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<AppLandingScreenItemT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetItemList/{itemId}")]
        public async Task<ActionResult<OpreationResultMessage<List<LandingScreenItemDetailsT>>>> GetItemList(int itemId)
        {
            try
            {
                var list = await landingScreenService.GetDetailsItemListAsync(itemId);
                return Ok(OpreationResultMessageFactory<List<LandingScreenItemDetailsT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<LandingScreenItemDetailsT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<AppLandingScreenItemT>>>> GetOfferList()
        {
            try
            {
                var list = await landingScreenService.GetOfferListAsync();
                return Ok(OpreationResultMessageFactory<List<AppLandingScreenItemT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<AppLandingScreenItemT>.CreateExceptionResponse(ex));
            }
           
        }

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<ServiceCustom>>>> GetOfferServiceList(int itemId, int? clientId = null)
        {
            try
            {
                List<ServiceCustom> customServiceList = null;
                if (commonService.IsViaApp())
                {
                    clientId = commonService.GetClientId();
                }
                if (clientId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<List<ServiceCustom>>.CreateErrorResponse(null, App.Global.Enums.OpreationResultStatusCode.InvalidData, "No client id found"));
                }
                var item = await landingScreenService.GetAsync(itemId);
                if(item.IsNull() || item.DepartmentId.IsNull())
                {
                    return Ok(OpreationResultMessageFactory<List<ServiceCustom>>.CreateErrorResponse(null, App.Global.Enums.OpreationResultStatusCode.NotFound, "No data found"));
                }
                var serviceList = await serviceService.GetOfferListByMainDeparmentAsync(item.DepartmentId.Value);
                if (serviceList.HasItem())
                {
                    customServiceList = await serviceService.ConvertServiceToCustom(serviceList, clientId.Value);
                }
                return Ok(OpreationResultMessageFactory<List<ServiceCustom>>.CreateSuccessResponse(customServiceList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<ServiceCustom>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<AppLandingScreenItemT>>>> GetBannerList()
        {
            try
            {
                var list = await landingScreenService.GetBannerListAsync();
                return Ok(OpreationResultMessageFactory<List<AppLandingScreenItemT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<AppLandingScreenItemT>.CreateExceptionResponse(ex));
            }
           
        }
    }
}
