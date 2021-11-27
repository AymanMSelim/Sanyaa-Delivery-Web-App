using App.Global.DTOs;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class WorkingAreaService : IWorkingAreaService
    {
        private readonly IRepository<WorkingAreaT> workingAreaRepository;

        public WorkingAreaService(IRepository<WorkingAreaT> workingAreaRepository)
        {
            this.workingAreaRepository = workingAreaRepository;
        }

        public Task<int> Add(WorkingAreaT workingArea)
        {
            workingAreaRepository.Add(workingArea);
            return workingAreaRepository.Save();
        }

        public Task<WorkingAreaT> Get(int id)
        {
            return workingAreaRepository.Get(id);
        }

        public Task<List<WorkingAreaT>> GetList(string govName, string cityName, string regionName)
        {
            var workingAreaList = workingAreaRepository.DbSet.AsQueryable();
            if (!string.IsNullOrEmpty(govName))
            {
                workingAreaList = workingAreaList.Where(d => d.WorkingAreaGov.Contains(govName));
            }
            if (!string.IsNullOrEmpty(cityName))
            {
                workingAreaList = workingAreaList.Where(d => d.WorkingAreaCity.Contains(cityName));
            }
            if (!string.IsNullOrEmpty(regionName))
            {
                workingAreaList = workingAreaList.Where(d => d.WorkingAreaRegion.Contains(regionName));
            }
            return workingAreaList.ToListAsync();
        }
        public Task<List<ValueWithIdDto>> GetCityList(string searchValue)
        {
            var govList = workingAreaRepository.DbSet.AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
            {
                govList = govList.Where(d => d.WorkingAreaCity.Contains(searchValue));
            }
            return govList.GroupBy(d => d.WorkingAreaCity)
                          .Select(d => new ValueWithIdDto
                          {
                              Value = d.FirstOrDefault().WorkingAreaCity
                          }).ToListAsync();
        }

        public Task<List<ValueWithIdDto>> GetRegionList(string searchValue)
        {
            var govList = workingAreaRepository.DbSet.AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
            {
                govList = govList.Where(d => d.WorkingAreaRegion.Contains(searchValue));
            }
            return govList.GroupBy(d => d.WorkingAreaRegion)
                          .Select(d => new ValueWithIdDto
                          {
                              Id = d.FirstOrDefault().WorkingAreaId.ToString(),
                              Value = d.FirstOrDefault().WorkingAreaRegion
                          }).ToListAsync();
        }


        public Task<List<ValueWithIdDto>> GetCityByGovList(string govName, string searchValue)
        {
            var cityList = workingAreaRepository.DbSet.AsQueryable();
            if (!string.IsNullOrEmpty(govName))
            {
                cityList = cityList.Where(d => d.WorkingAreaGov == govName);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                cityList = cityList.Where(d => d.WorkingAreaCity.Contains(searchValue));
            }
            return cityList.GroupBy(d => d.WorkingAreaCity)
                .Select(d => new ValueWithIdDto
                {
                    Value = d.FirstOrDefault().WorkingAreaCity
                }).ToListAsync();
        }

        public Task<List<ValueWithIdDto>> GetGovList(string searchValue)
        {
            var govList = workingAreaRepository.DbSet.AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
            {
                govList = govList.Where(d => d.WorkingAreaGov.Contains(searchValue));
            }
            return govList.GroupBy(d => d.WorkingAreaGov)
                          .Select(d => new ValueWithIdDto
                          {
                              Value = d.FirstOrDefault().WorkingAreaGov
                          }).ToListAsync();
        }

        public Task<List<ValueWithIdDto>> GetRegionByCityList(string cityName, string searchValue)
        {
            var regionList = workingAreaRepository.DbSet.AsQueryable();
            if (!string.IsNullOrEmpty(cityName))
            {
                regionList = regionList.Where(d => d.WorkingAreaCity == cityName);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                regionList = regionList.Where(d => d.WorkingAreaRegion.Contains(searchValue));
            }
            return regionList.GroupBy(d => d.WorkingAreaRegion)
                          .Select(d => new ValueWithIdDto
                          {
                              Id = d.FirstOrDefault().WorkingAreaId.ToString(),
                              Value = d.FirstOrDefault().WorkingAreaRegion
                          }).ToListAsync();
        }
    }
}
