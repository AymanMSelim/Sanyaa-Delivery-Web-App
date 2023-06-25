using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanyaaDelivery.API.Controllers
{
    public class WorkingAreaController : APIBaseController
    {
        private readonly IWorkingAreaService workingAreaService;
        private readonly ICityService cityService;
        private readonly IGovernorateService governorateService;
        private readonly IRegionService regionService;

        public WorkingAreaController(IWorkingAreaService workingAreaService, ICityService cityService, 
            IGovernorateService governorateService, IRegionService regionService, CommonService commonService) : base(commonService)
        {
            this.workingAreaService = workingAreaService;
            this.cityService = cityService;
            this.governorateService = governorateService;
            this.regionService = regionService;
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<Result<WorkingAreaT>>> Get(int id)
        {
            try
            {
                var result = await workingAreaService.Get(id);
                if (result == null)
                {
                    return Ok(ResultFactory<WorkingAreaT>.CreateNotFoundResponse());
                }
                return Ok(ResultFactory<WorkingAreaT>.CreateSuccessResponse(result));

            }
            catch (Exception ex)
            {
                return Ok(ResultFactory<WorkingAreaT>.CreateExceptionResponse(ex));
            }

        }

        [HttpGet("GetList")]
        public async Task<ActionResult<Result<List<WorkingAreaT>>>> GetList(string govName, string cityName, string regionName)
        {
            try
            {
                var result = await workingAreaService.GetList(govName, cityName, regionName);
                if (result == null)
                {
                    return Ok(ResultFactory<List<WorkingAreaT>>.CreateErrorResponse());
                }
                if(result.Count == 0)
                {
                    return Ok(ResultFactory<List<WorkingAreaT>>.CreateNotFoundResponse());
                }
                return Ok(ResultFactory<List<WorkingAreaT>>.CreateSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResultFactory<WorkingAreaT>.CreateExceptionResponse(ex));
            }

        }

        [HttpGet("GetGovList/{searchValue}")]
        public async Task<ActionResult<Result<List<ValueWithIdDto>>>> GetGovList(string searchValue)
        {
            try
            {
                var result = await workingAreaService.GetGovList(searchValue);
                if (result == null)
                {
                    return Ok(ResultFactory<List<ValueWithIdDto>>.CreateErrorResponse());
                }
                if(result.Count == 0)
                {
                    return Ok(ResultFactory<List<ValueWithIdDto>>.CreateNotFoundResponse());
                }
                return Ok(ResultFactory<List<ValueWithIdDto>>.CreateSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResultFactory<List<ValueWithIdDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetCityList/{searchValue}")]
        public async Task<ActionResult<Result<List<ValueWithIdDto>>>> GetCityList(string searchValue)
        {
            try
            {
                var result = await workingAreaService.GetCityList(searchValue);
                if (result == null)
                {
                    return Ok(ResultFactory<List<ValueWithIdDto>>.CreateErrorResponse());
                }
                if (result.Count == 0)
                {
                    return Ok(ResultFactory<List<ValueWithIdDto>>.CreateNotFoundResponse());
                }
                return Ok(ResultFactory<List<ValueWithIdDto>>.CreateSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResultFactory<List<ValueWithIdDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetRegionList/{searchValue}")]
        public async Task<ActionResult<Result<List<ValueWithIdDto>>>> GetRegionList(string searchValue)
        {
            try
            {
                var result = await workingAreaService.GetRegionList(searchValue);
                if (result == null)
                {
                    return Ok(ResultFactory<List<ValueWithIdDto>>.CreateErrorResponse());
                }
                if (result.Count == 0)
                {
                    return Ok(ResultFactory<List<ValueWithIdDto>>.CreateNotFoundResponse());
                }
                return Ok(ResultFactory<List<ValueWithIdDto>>.CreateSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResultFactory<List<ValueWithIdDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetCityByGovList")]
        public async Task<ActionResult<Result<List<ValueWithIdDto>>>> GetCityByGovList(string govName, string searchValue)
        {
            try
            {
                var result = await workingAreaService.GetCityByGovList(govName, searchValue);
                if (result == null)
                {
                    return Ok(ResultFactory<List<ValueWithIdDto>>.CreateErrorResponse());
                }
                if (result.Count == 0)
                {
                    return Ok(ResultFactory<List<ValueWithIdDto>>.CreateNotFoundResponse());
                }
                return Ok(ResultFactory<List<ValueWithIdDto>>.CreateSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResultFactory<List<ValueWithIdDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpGet("GetRegionByCityList")]
        public async Task<ActionResult<Result<List<ValueWithIdDto>>>> GetRegionByCityList(string cityName, string searchValue)
        {
            try
            {
                var result = await workingAreaService.GetRegionByCityList(cityName, searchValue);
                if (result == null)
                {
                    return Ok(ResultFactory<List<ValueWithIdDto>>.CreateErrorResponse());
                }
                if (result.Count == 0)
                {
                    return Ok(ResultFactory<List<ValueWithIdDto>>.CreateNotFoundResponse());
                }
                return Ok(ResultFactory<List<ValueWithIdDto>>.CreateSuccessResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(ResultFactory<List<ValueWithIdDto>>.CreateExceptionResponse(ex));
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Result<WorkingAreaT>>> Add(WorkingAreaT workingArea)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(ResultFactory<WorkingAreaT>.CreateErrorResponseMessage("InvalidData", App.Global.Enums.ResultStatusCode.InvalidData));
                }
                GovernorateT gov = await governorateService.GetAsync(workingArea.WorkingAreaGov);
                CityT city = await cityService.GetAsync(workingArea.WorkingAreaCity);
                RegionT region = await regionService.GetAsync(workingArea.WorkingAreaRegion);
                if (gov.IsNull())
                {
                    gov = new GovernorateT
                    {
                        CountryId = 1,
                        GovernorateName = workingArea.WorkingAreaGov
                    };
                    await governorateService.AddAsync(gov);
                };

                if (city.IsNull())
                {
                    city = new CityT
                    {
                        CityName = workingArea.WorkingAreaCity,
                        GovernorateId = gov.GovernorateId

                    };
                    await cityService.AddAsync(city);
                };

                if (region.IsNull())
                {
                    region = new RegionT
                    {
                        RegionName = workingArea.WorkingAreaRegion,
                        CityId = city.CityId
                    };
                    await regionService.AddAsync(region);
                };
                var workingAreaList = await workingAreaService.GetList(workingArea.WorkingAreaGov, workingArea.WorkingAreaCity, workingArea.WorkingAreaRegion);
                if (workingAreaList.HasItem())
                {
                    return Ok(ResultFactory<WorkingAreaT>.CreateSuccessResponse(workingAreaList.FirstOrDefault(), App.Global.Enums.ResultStatusCode.AlreadyExist));
                }
                var result = await workingAreaService.Add(workingArea);
                if (result > 0)
                {
                    return Ok(ResultFactory<WorkingAreaT>.CreateSuccessResponse(workingArea, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<WorkingAreaT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return Ok(ResultFactory<List<ValueWithIdDto>>.CreateExceptionResponse(ex));
            }

        }

        [HttpPost("Update")]
        public async Task<ActionResult<Result<WorkingAreaT>>> Update(WorkingAreaT workingArea)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(ResultFactory<WorkingAreaT>.CreateErrorResponseMessage("InvalidData", App.Global.Enums.ResultStatusCode.InvalidData));
                }
                GovernorateT gov = await governorateService.GetAsync(workingArea.WorkingAreaGov);
                CityT city = await cityService.GetAsync(workingArea.WorkingAreaCity);
                RegionT region = await regionService.GetAsync(workingArea.WorkingAreaRegion);
                if (gov.IsNull())
                {
                    gov = new GovernorateT
                    {
                        CountryId = 1,
                        GovernorateName = workingArea.WorkingAreaGov
                    };
                    await governorateService.AddAsync(gov);
                };

                if (city.IsNull())
                {
                    city = new CityT
                    {
                        CityName = workingArea.WorkingAreaCity,
                        GovernorateId = gov.GovernorateId

                    };
                    await cityService.AddAsync(city);
                };

                if (region.IsNull())
                {
                    region = new RegionT
                    {
                        RegionName = workingArea.WorkingAreaRegion,
                        CityId = city.CityId
                    };
                    await regionService.AddAsync(region);
                };
                var result = await workingAreaService.Add(workingArea);
                if (result > 0)
                {
                    return Ok(ResultFactory<WorkingAreaT>.CreateSuccessResponse(workingArea, App.Global.Enums.ResultStatusCode.RecordAddedSuccessfully));
                }
                else
                {
                    return Ok(ResultFactory<WorkingAreaT>.CreateErrorResponse());
                }
            }
            catch (Exception ex)
            {
                return Ok(ResultFactory<List<ValueWithIdDto>>.CreateExceptionResponse(ex));
            }

        }

    }
}
