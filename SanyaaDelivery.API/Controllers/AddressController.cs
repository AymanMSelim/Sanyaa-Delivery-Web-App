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
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("GetCountryList")]
        public async Task<ActionResult<HttpResponseDto<List<CountryT>>>> GetCountryList()
        {
            try
            {
                var list = await countryService.GetList();
                if (list != null)
                {
                    return Ok(HttpResponseDtoFactory<List<CountryT>>.CreateSuccessResponse(list));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return BadRequest(HttpResponseDtoFactory<List<CountryT>>.CreateErrorResponse());
            }
        }

        [HttpGet("GetGovernorateList/{countryId}")]
        public async Task<ActionResult<HttpResponseDto<List<GovernorateT>>>> GetGovernorateList(int? countryId)
        {
            try
            {
                var list = await governorateService.GetList(countryId);
                if (list != null)
                {
                    return Ok(HttpResponseDtoFactory<List<GovernorateT>>.CreateSuccessResponse(list));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return BadRequest(HttpResponseDtoFactory<List<GovernorateT>>.CreateErrorResponse());
            }
        }


        [HttpGet("GetCityList/{governorateId}")]
        public async Task<ActionResult<HttpResponseDto<List<CityT>>>> GetCityListList(int? governorateId)
        {
            try
            {
                var list = await cityService.GetList(governorateId);
                if (list != null)
                {
                    return Ok(HttpResponseDtoFactory<List<CityT>>.CreateSuccessResponse(list));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return BadRequest(HttpResponseDtoFactory<List<CityT>>.CreateErrorResponse());
            }
        }


        [HttpGet("GetRegionList/{cityId}")]
        public async Task<ActionResult<HttpResponseDto<List<RegionT>>>> GetRegionList(int? cityId)
        {
            try
            {
                var list = await regionService.GetList(cityId);
                if (list != null)
                {
                    return Ok(HttpResponseDtoFactory<List<RegionT>>.CreateSuccessResponse(list));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return BadRequest(HttpResponseDtoFactory<List<RegionT>>.CreateErrorResponse());
            }
        }
    }
}
