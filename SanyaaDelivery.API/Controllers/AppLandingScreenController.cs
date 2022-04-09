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
    public class AppLandingScreenController : APIBaseAuthorizeController
    {
        private readonly IAppLandingScreenService landingScreenService;

        public AppLandingScreenController(IAppLandingScreenService landingScreenService)
        {
            this.landingScreenService = landingScreenService;
        }
        [HttpGet("GetDepartmentList")]
        public async Task<ActionResult<HttpResponseDto<List<AppLandingScreenItemT>>>> GetDepartmentList()
        {
            var list = await landingScreenService.GetDepartmentListAsync();
            return Ok(HttpResponseDtoFactory<List<AppLandingScreenItemT>>.CreateSuccessResponse(list));
        }

        [HttpGet("GetOfferList")]
        public async Task<ActionResult<HttpResponseDto<List<AppLandingScreenItemT>>>> GetOfferList()
        {
            var list = await landingScreenService.GetOfferListAsync();
            return Ok(HttpResponseDtoFactory<List<AppLandingScreenItemT>>.CreateSuccessResponse(list));
        }

        [HttpGet("GetBannerList")]
        public async Task<ActionResult<HttpResponseDto<List<AppLandingScreenItemT>>>> GetBannerList()
        {
            var list = await landingScreenService.GetBannerListAsync();
            return Ok(HttpResponseDtoFactory<List<AppLandingScreenItemT>>.CreateSuccessResponse(list));
        }
    }
}
