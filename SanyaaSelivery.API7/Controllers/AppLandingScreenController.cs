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

        public AppLandingScreenController(IAppLandingScreenService landingScreenService, IServiceService serviceService, CommonService commonService) : base(commonService)
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
                var detailsList = await landingScreenService.GetDetailsItemListAsync(itemId);
                if(detailsList.IsEmpty())
                {
                    return Ok(ResultFactory<List<ServiceCustom>>.CreateErrorResponse(new List<ServiceCustom>(), App.Global.Enums.ResultStatusCode.NotFound, "No data found"));
                }
                var serviceList = await serviceService.GetOfferListByMainDeparmentAsync(detailsList.Select(d => d.DepartmentId).ToList());
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


        [HttpGet("GetEmployeeAppCompanyInfo")]
        public ActionResult<Result<CompanyInfoDto>> GetEmployeeAppCompanyInfo()
        {
            try
            {
                var data = System.IO.File.ReadAllText(@"companycontact.json");
                var aboutCompany = System.IO.File.ReadAllText(@"AboutCompanyEmployeeApp.txt");
                var privacyPolicy = System.IO.File.ReadAllText(@"PrivacyPolicyEmployeeApp.txt");
                var companyInfo = JsonConvert.DeserializeObject<CompanyInfoDto>(data);
                companyInfo.AboutCompany = aboutCompany;
                companyInfo.PrivacyPolicy = privacyPolicy;
                return Ok(ResultFactory<CompanyInfoDto>.CreateSuccessResponse(companyInfo));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CompanyInfoDto>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Result<AppLandingScreenItemT>>> Add(AppLandingScreenItemT model)
        {
            try
            {
                var affectedRows = await landingScreenService.AddAsync(model);
                return Ok(ResultFactory<AppLandingScreenItemT>.CreateAffectedRowsResult(affectedRows, data: model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppLandingScreenItemT>.CreateExceptionResponse(ex));
            }

        }

        [HttpPost("Update")]
        public async Task<ActionResult<Result<AppLandingScreenItemT>>> Update(AppLandingScreenItemT model)
        {
            try
            {
                var affectedRows = await landingScreenService.UpdateAsync(model);
                return Ok(ResultFactory<AppLandingScreenItemT>.CreateAffectedRowsResult(affectedRows, data: model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppLandingScreenItemT>.CreateExceptionResponse(ex));
            }

        }

        [HttpPost("Delete/{id}")]
        public async Task<ActionResult<Result<object>>> Delete(int id)
        {
            try
            {
                var affectedRows = await landingScreenService.DeleteAsync(id);
                return Ok(ResultFactory<object>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }

        }

        [HttpGet("GetList")]
        public async Task<ActionResult<Result<List<AppLandingScreenItemT>>>> GetList(string? searchValue = null, int? type = null)
        {
            try
            {
                var list = await landingScreenService.GetListAsync(searchValue, type);
                return Ok(ResultFactory<List<AppLandingScreenItemT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<AppLandingScreenItemT>.CreateExceptionResponse(ex));
            }

        }

        [HttpPost("AddDetails")]
        public async Task<ActionResult<Result<LandingScreenItemDetailsT>>> AddDetails(LandingScreenItemDetailsT model)
        {
            try
            {
                var affectedRows = await landingScreenService.AddDetailsAsync(model);
                return Ok(ResultFactory<LandingScreenItemDetailsT>.CreateAffectedRowsResult(affectedRows, data: model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<LandingScreenItemDetailsT>.CreateExceptionResponse(ex));
            }

        }

        [HttpPost("UpdateDetails")]
        public async Task<ActionResult<Result<LandingScreenItemDetailsT>>> UpdateDetails(LandingScreenItemDetailsT model)
        {
            try
            {
                var affectedRows = await landingScreenService.UpdateDetailsAsync(model);
                return Ok(ResultFactory<LandingScreenItemDetailsT>.CreateAffectedRowsResult(affectedRows, data: model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<LandingScreenItemDetailsT>.CreateExceptionResponse(ex));
            }

        }

        [HttpPost("DeleteDetails/{id}")]
        public async Task<ActionResult<Result<object>>> DeleteDetails(int id)
        {
            try
            {
                var affectedRows = await landingScreenService.DeleteDetailsAsync(id);
                return Ok(ResultFactory<object>.CreateAffectedRowsResult(affectedRows));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<object>.CreateExceptionResponse(ex));
            }

        }

        [HttpGet("GetDetailsList/{itemId}")]
        public async Task<ActionResult<Result<List<LandingScreenItemDetailsT>>>> GetDetailsList(int itemId)
        {
            try
            {
                var list = await landingScreenService.GetDetailsListAsync(itemId);
                return Ok(ResultFactory<List<LandingScreenItemDetailsT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<LandingScreenItemDetailsT>.CreateExceptionResponse(ex));
            }

        }
    }
}
