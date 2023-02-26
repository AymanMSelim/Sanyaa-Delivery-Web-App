using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
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

        [HttpGet("GetDepartmentList")]
        public async Task<ActionResult<Result<List<AppLandingScreenItemT>>>> GetDepartmentList()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var clientId = App.Global.JWT.TokenHelper.GetReferenceId(identity);
                var list = await landingScreenService.GetDepartmentListAsync(clientId);
                list.ForEach(d => d.IsActive = d.LandingScreenItemDetailsT.Any());
                list.ForEach(d => d.ImagePath = d.ImagePath.Replace("{status}", d.IsActive ? "ava" : "soon"));
                return Ok(ResultFactory<List<AppLandingScreenItemT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppLandingScreenItemT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetItemList/{itemId}")]
        public async Task<ActionResult<Result<List<LandingScreenItemDetailsT>>>> GetItemList(int itemId)
        {
            try
            {
                var list = await landingScreenService.GetDetailsItemListAsync(itemId);
                return Ok(ResultFactory<List<LandingScreenItemDetailsT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<LandingScreenItemDetailsT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetOfferList")]
        public async Task<ActionResult<Result<List<AppLandingScreenItemT>>>> GetOfferList()
        {
            try
            {
                var list = await landingScreenService.GetOfferListAsync();
                return Ok(ResultFactory<List<AppLandingScreenItemT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppLandingScreenItemT>.CreateExceptionResponse(ex));
            }
           
        }

        [HttpGet("GetOfferServiceList")]
        public async Task<ActionResult<Result<List<ServiceCustom>>>> GetOfferServiceList(int itemId, int? clientId = null)
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
                    return Ok(ResultFactory<List<ServiceCustom>>.CreateErrorResponse(null, App.Global.Enums.ResultStatusCode.InvalidData, "No client id found"));
                }
                var item = await landingScreenService.GetAsync(itemId);
                if(item.IsNull() || item.DepartmentId.IsNull())
                {
                    return Ok(ResultFactory<List<ServiceCustom>>.CreateErrorResponse(null, App.Global.Enums.ResultStatusCode.NotFound, "No data found"));
                }
                var serviceList = await serviceService.GetOfferListByMainDeparmentAsync(item.DepartmentId.Value);
                if (serviceList.HasItem())
                {
                    customServiceList = await serviceService.ConvertServiceToCustom(serviceList, clientId.Value);
                }
                return Ok(ResultFactory<List<ServiceCustom>>.CreateSuccessResponse(customServiceList));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<ServiceCustom>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetBannerList")]
        public async Task<ActionResult<Result<List<AppLandingScreenItemT>>>> GetBannerList()
        {
            try
            {
                var list = await landingScreenService.GetBannerListAsync();
                return Ok(ResultFactory<List<AppLandingScreenItemT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppLandingScreenItemT>.CreateExceptionResponse(ex));
            }
           
        }

        [HttpGet("GetCompanyInfo")]
        public ActionResult<Result<CompanyInfoDto>> GetCompanyInfo()
        {
            try
            {
                var data = System.IO.File.ReadAllText(@"companycontact.json");
                var aboutCompany = System.IO.File.ReadAllText(@"AboutCompany.txt");
                var termsAndConditions = System.IO.File.ReadAllText(@"TermsAndConditions.txt");
                var privacyPolicy = System.IO.File.ReadAllText(@"PrivacyPolicy.txt");
                var companyInfo = JsonConvert.DeserializeObject<CompanyInfoDto>(data);
                companyInfo.AboutCompany = aboutCompany;
                companyInfo.AgreementConditionTerms = termsAndConditions;
                companyInfo.PrivacyPolicy = privacyPolicy;
                return Ok(ResultFactory<CompanyInfoDto>.CreateSuccessResponse(companyInfo));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CompanyInfoDto>.CreateExceptionResponse(ex));
            }
        }
    }
}
