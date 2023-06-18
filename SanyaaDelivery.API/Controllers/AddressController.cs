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
    public class AddressController : APIBaseAuthorizeController
    {
        private readonly ICountryService countryService;
        private readonly IGovernorateService governorateService;
        private readonly ICityService cityService;
        private readonly IRegionService regionService;

        public AddressController(ICountryService countryService, IGovernorateService governorateService, 
            ICityService cityService, IRegionService regionService, CommonService commonService) : base(commonService)
        {
            this.countryService = countryService;
            this.governorateService = governorateService;
            this.cityService = cityService;
            this.regionService = regionService;
        }

        #region Country
        [HttpGet("GetCountry/{countryId}")]
        public async Task<ActionResult<Result<CountryT>>> GetCountry(int countryId)
        {
            try
            {
                var country = await countryService.GetAsync(countryId);
                return Ok(ResultFactory<CountryT>.CreateSuccessResponse(country, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CountryT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddCountry")]
        public async Task<ActionResult<Result<CountryT>>> AddCountry(CountryT country)
        {
            try
            {
                var affectedRows = await countryService.AddAsync(country);
                if(affectedRows > 0)
                {
                    return Ok(ResultFactory<CountryT>.CreateSuccessResponse(country, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<CountryT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CountryT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateCountry")]
        public async Task<ActionResult<Result<CountryT>>> UpdateCountry(CountryT country)
        {
            try
            {
                var affectedRows = await countryService.UpdateAsync(country);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<CountryT>.CreateSuccessResponse(country, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<CountryT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CountryT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteCountry/{countryId}")]
        public async Task<ActionResult<Result<CountryT>>> DeleteCountry(int countryId)
        {
            try
            {
                var affectedRows = await countryService.DeletetAsync(countryId);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<CountryT>.CreateSuccessResponse(null, App.Global.Enums.ResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<CountryT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CountryT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetCountryList/{countryName?}")]
        public async Task<ActionResult<Result<List<CountryT>>>> GetCountryList(string countryName = null)
        {
            try
            {
                var list = await countryService.GetListAsync(countryName);
                if (list != null)
                {
                    return Ok(ResultFactory<List<CountryT>>.CreateSuccessResponse(list));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return Ok(ResultFactory<List<CountryT>>.CreateErrorResponse());
            }
        }
        #endregion

        #region Governorate
        [HttpGet("GetGovernorate/{governorateId}")]
        public async Task<ActionResult<Result<GovernorateT>>> GetGovernorate(int governorateId)
        {
            try
            {
                var Governorate = await governorateService.GetAsync(governorateId);
                return Ok(ResultFactory<GovernorateT>.CreateSuccessResponse(Governorate, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<GovernorateT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddGovernorate")]
        public async Task<ActionResult<Result<GovernorateT>>> AddGovernorate(GovernorateT governorate)
        {
            try
            {
                var affectedRows = await governorateService.AddAsync(governorate);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<GovernorateT>.CreateSuccessResponse(governorate, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<GovernorateT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<GovernorateT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateGovernorate")]
        public async Task<ActionResult<Result<GovernorateT>>> UpdateGovernorate(GovernorateT governorate)
        {
            try
            {
                var affectedRows = await governorateService.UpdateAsync(governorate);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<GovernorateT>.CreateSuccessResponse(governorate, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<GovernorateT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<GovernorateT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteGovernorate/{governorateId}")]
        public async Task<ActionResult<Result<GovernorateT>>> DeleteGovernorate(int governorateId)
        {
            try
            {
                var affectedRows = await governorateService.DeletetAsync(governorateId);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<GovernorateT>.CreateSuccessResponse(null, App.Global.Enums.ResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<GovernorateT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<GovernorateT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetGovernorateList/{countryId}")]
        public async Task<ActionResult<Result<List<GovernorateT>>>> GetGovernorateList(int? countryId)
        {
            try
            {
                var list = await governorateService.GetListAsync(countryId, null);
                return Ok(ResultFactory<List<GovernorateT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<GovernorateT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetGovernorateList")]
        public async Task<ActionResult<Result<List<GovernorateT>>>> GetGovernorateList(int? countryId = null, string governorateName = null)
        {
            try
            {
                var list = await governorateService.GetListAsync(countryId, governorateName);
                return Ok(ResultFactory<List<GovernorateT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<GovernorateT>.CreateExceptionResponse(ex));
            }
        }
        #endregion

        #region City
        [HttpGet("GetCity/{cityId}")]
        public async Task<ActionResult<Result<CityT>>> GetCity(int cityId)
        {
            try
            {
                var city = await cityService.GetAsync(cityId);
                return base.Ok(ResultFactory<CityT>.CreateSuccessResponse((CityT)city, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CityT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddCity")]
        public async Task<ActionResult<Result<CityT>>> AddCity(CityT city)
        {
            try
            {
                var affectedRows = await cityService.AddAsync(city);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<CityT>.CreateSuccessResponse(city, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<CityT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CityT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateCity")]
        public async Task<ActionResult<Result<CityT>>> UpdateCity(CityT City)
        {
            try
            {
                var affectedRows = await cityService.UpdateAsync(City);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<CityT>.CreateSuccessResponse(City, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<CityT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CityT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteCity/{cityId}")]
        public async Task<ActionResult<Result<CityT>>> DeleteCity(int cityId)
        {
            try
            {
                var affectedRows = await cityService.DeletetAsync(cityId);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<CityT>.CreateSuccessResponse(null, App.Global.Enums.ResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<CityT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CityT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetCityList/{governorateId}")]
        public async Task<ActionResult<Result<List<CityT>>>> GetCityList(int? governorateId = null)
        {
            try
            {
                var list = await cityService.GetListAsync(null, governorateId, null);
                return Ok(ResultFactory<List<CityT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CityT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetCityList")]
        public async Task<ActionResult<Result<List<CityT>>>> GetCityList(int? countryId = null, int? governorateId = null, string cityName = null)
        {
            try
            {
                var list = await cityService.GetListAsync(countryId, governorateId, cityName);
                return Ok(ResultFactory<List<CityT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<CityT>.CreateExceptionResponse(ex));
            }
        }
        #endregion

        #region Region
        [HttpGet("GetRegion/{regionId}")]
        public async Task<ActionResult<Result<RegionT>>> GetRegion(int regionId)
        {
            try
            {
                var region = await regionService.GetAsync(regionId);
                return base.Ok(ResultFactory<RegionT>.CreateSuccessResponse(region, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RegionT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("AddRegion")]
        public async Task<ActionResult<Result<RegionT>>> AddRegion(RegionT region)
        {
            try
            {
                var affectedRows = await regionService.AddAsync(region);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<RegionT>.CreateSuccessResponse(region, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<RegionT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RegionT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("UpdateRegion")]
        public async Task<ActionResult<Result<RegionT>>> UpdateRegion(RegionT region)
        {
            try
            {
                var affectedRows = await regionService.UpdateAsync(region);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<RegionT>.CreateSuccessResponse(region, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<RegionT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RegionT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteRegion/{regionId}")]
        public async Task<ActionResult<Result<RegionT>>> DeleteRegion(int regionId)
        {
            try
            {
                var affectedRows = await regionService.DeletetAsync(regionId);
                if (affectedRows > 0)
                {
                    return Ok(ResultFactory<RegionT>.CreateSuccessResponse(null, App.Global.Enums.ResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<RegionT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RegionT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetRegionList/{cityId}")]
        public async Task<ActionResult<Result<List<RegionT>>>> GetRegionList(int? cityId)
        {
            try
            {
                var list = await regionService.GetListAsync(null, null, cityId, null);
                return Ok(ResultFactory<List<RegionT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RegionT>.CreateExceptionResponse(ex));
            }
        }


        [HttpGet("GetRegionList")]
        public async Task<ActionResult<Result<List<RegionT>>>> GetRegionList(int? countryId = null, int? governorateId = null, int? cityId = null, string regionName = null)
        {
            try
            {
                var list = await regionService.GetListAsync(countryId, governorateId, cityId, regionName);
                return Ok(ResultFactory<List<RegionT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultFactory<RegionT>.CreateExceptionResponse(ex));
            }
        }
        #endregion
    }
}
