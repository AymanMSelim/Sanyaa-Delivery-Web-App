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

        public AddressController(ICountryService countryService, IGovernorateService governorateService, ICityService cityService, IRegionService regionService)
        {
            this.countryService = countryService;
            this.governorateService = governorateService;
            this.cityService = cityService;
            this.regionService = regionService;
        }

        #region Country
        [HttpGet("GetCountry/{countryId}")]
        public async Task<ActionResult<OpreationResultMessage<CountryT>>> GetCountry(int countryId)
        {
            try
            {
                var country = await countryService.GetAsync(countryId);
                return Ok(OpreationResultMessageFactory<CountryT>.CreateSuccessResponse(country, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CountryT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<CountryT>>> AddCountry(CountryT country)
        {
            try
            {
                var affectedRows = await countryService.AddAsync(country);
                if(affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<CountryT>.CreateSuccessResponse(country, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CountryT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CountryT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<CountryT>>> UpdateCountry(CountryT country)
        {
            try
            {
                var affectedRows = await countryService.UpdateAsync(country);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<CountryT>.CreateSuccessResponse(country, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CountryT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CountryT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteCountry/{countryId}")]
        public async Task<ActionResult<OpreationResultMessage<CountryT>>> DeleteCountry(int countryId)
        {
            try
            {
                var affectedRows = await countryService.DeletetAsync(countryId);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<CountryT>.CreateSuccessResponse(null, App.Global.Enums.OpreationResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CountryT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CountryT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetCountryList/{countryName?}")]
        public async Task<ActionResult<OpreationResultMessage<List<CountryT>>>> GetCountryList(string countryName = null)
        {
            try
            {
                var list = await countryService.GetListAsync(countryName);
                if (list != null)
                {
                    return Ok(OpreationResultMessageFactory<List<CountryT>>.CreateSuccessResponse(list));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return Ok(OpreationResultMessageFactory<List<CountryT>>.CreateErrorResponse());
            }
        }
        #endregion

        #region Governorate
        [HttpGet("GetGovernorate/{governorateId}")]
        public async Task<ActionResult<OpreationResultMessage<GovernorateT>>> GetGovernorate(int governorateId)
        {
            try
            {
                var Governorate = await governorateService.GetAsync(governorateId);
                return Ok(OpreationResultMessageFactory<GovernorateT>.CreateSuccessResponse(Governorate, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<GovernorateT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<GovernorateT>>> AddGovernorate(GovernorateT governorate)
        {
            try
            {
                var affectedRows = await governorateService.AddAsync(governorate);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<GovernorateT>.CreateSuccessResponse(governorate, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<GovernorateT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<GovernorateT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<GovernorateT>>> UpdateGovernorate(GovernorateT governorate)
        {
            try
            {
                var affectedRows = await governorateService.UpdateAsync(governorate);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<GovernorateT>.CreateSuccessResponse(governorate, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<GovernorateT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<GovernorateT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteGovernorate/{governorateId}")]
        public async Task<ActionResult<OpreationResultMessage<GovernorateT>>> DeleteGovernorate(int governorateId)
        {
            try
            {
                var affectedRows = await governorateService.DeletetAsync(governorateId);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<GovernorateT>.CreateSuccessResponse(null, App.Global.Enums.OpreationResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<GovernorateT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<GovernorateT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetGovernorateList/{countryId}")]
        public async Task<ActionResult<OpreationResultMessage<List<GovernorateT>>>> GetGovernorateList(int? countryId)
        {
            try
            {
                var list = await governorateService.GetListAsync(countryId, null);
                return Ok(OpreationResultMessageFactory<List<GovernorateT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<GovernorateT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<GovernorateT>>>> GetGovernorateList(int? countryId = null, string governorateName = null)
        {
            try
            {
                var list = await governorateService.GetListAsync(countryId, governorateName);
                return Ok(OpreationResultMessageFactory<List<GovernorateT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<GovernorateT>.CreateExceptionResponse(ex));
            }
        }
        #endregion

        #region City
        [HttpGet("GetCity/{cityId}")]
        public async Task<ActionResult<OpreationResultMessage<CityT>>> GetCity(int cityId)
        {
            try
            {
                var city = await cityService.GetAsync(cityId);
                return base.Ok(OpreationResultMessageFactory<CityT>.CreateSuccessResponse((CityT)city, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CityT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<CityT>>> AddCity(CityT city)
        {
            try
            {
                var affectedRows = await cityService.AddAsync(city);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<CityT>.CreateSuccessResponse(city, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CityT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CityT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<CityT>>> UpdateCity(CityT City)
        {
            try
            {
                var affectedRows = await cityService.UpdateAsync(City);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<CityT>.CreateSuccessResponse(City, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CityT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CityT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteCity/{cityId}")]
        public async Task<ActionResult<OpreationResultMessage<CityT>>> DeleteCity(int cityId)
        {
            try
            {
                var affectedRows = await cityService.DeletetAsync(cityId);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<CityT>.CreateSuccessResponse(null, App.Global.Enums.OpreationResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<CityT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CityT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetCityList/{governorateId}")]
        public async Task<ActionResult<OpreationResultMessage<List<CityT>>>> GetCityList(int? governorateId = null)
        {
            try
            {
                var list = await cityService.GetListAsync(null, governorateId, null);
                return Ok(OpreationResultMessageFactory<List<CityT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CityT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<CityT>>>> GetCityList(int? countryId = null, int? governorateId = null, string cityName = null)
        {
            try
            {
                var list = await cityService.GetListAsync(countryId, governorateId, cityName);
                return Ok(OpreationResultMessageFactory<List<CityT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<CityT>.CreateExceptionResponse(ex));
            }
        }
        #endregion

        #region Region
        [HttpGet("GetRegion/{regionId}")]
        public async Task<ActionResult<OpreationResultMessage<RegionT>>> GetRegion(int regionId)
        {
            try
            {
                var region = await regionService.GetAsync(regionId);
                return base.Ok(OpreationResultMessageFactory<RegionT>.CreateSuccessResponse(region, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<RegionT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<RegionT>>> AddRegion(RegionT region)
        {
            try
            {
                var affectedRows = await regionService.AddAsync(region);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<RegionT>.CreateSuccessResponse(region, App.Global.Enums.OpreationResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<RegionT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<RegionT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult<OpreationResultMessage<RegionT>>> UpdateRegion(RegionT region)
        {
            try
            {
                var affectedRows = await regionService.UpdateAsync(region);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<RegionT>.CreateSuccessResponse(region, App.Global.Enums.OpreationResultStatusCode.RecordUpdatedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<RegionT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<RegionT>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("DeleteRegion/{regionId}")]
        public async Task<ActionResult<OpreationResultMessage<RegionT>>> DeleteRegion(int regionId)
        {
            try
            {
                var affectedRows = await regionService.DeletetAsync(regionId);
                if (affectedRows > 0)
                {
                    return Ok(OpreationResultMessageFactory<RegionT>.CreateSuccessResponse(null, App.Global.Enums.OpreationResultStatusCode.RecordDeletedSuccessfully));
                }
                else
                {
                    return Ok(OpreationResultMessageFactory<RegionT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<RegionT>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetRegionList/{cityId}")]
        public async Task<ActionResult<OpreationResultMessage<List<RegionT>>>> GetRegionList(int? cityId)
        {
            try
            {
                var list = await regionService.GetListAsync(null, null, cityId, null);
                return Ok(OpreationResultMessageFactory<List<RegionT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<RegionT>.CreateExceptionResponse(ex));
            }
        }


        [HttpGet]
        public async Task<ActionResult<OpreationResultMessage<List<RegionT>>>> GetRegionList(int? countryId = null, int? governorateId = null, int? cityId = null, string regionName = null)
        {
            try
            {
                var list = await regionService.GetListAsync(countryId, governorateId, cityId, regionName);
                return Ok(OpreationResultMessageFactory<List<RegionT>>.CreateSuccessResponse(list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, OpreationResultMessageFactory<RegionT>.CreateExceptionResponse(ex));
            }
        }
        #endregion
    }
}
