using App.Global.ExtensionMethods;
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
    public class RegionService : IRegionService
    {
        private readonly IRepository<RegionT> repo;

        public RegionService(IRepository<RegionT> repo)
        {
            this.repo = repo;
        }

        public async Task<int> AddAsync(RegionT region)
        {
            await repo.AddAsync(region);
            return await repo.SaveAsync();
        }

        public async Task<int> DeletetAsync(int id)
        {
            await repo.DeleteAsync(id);
            return await repo.SaveAsync();
        }

        public Task<RegionT> GetAsync(int id)
        {
            return repo.GetAsync(id);
        }

        public Task<RegionT> GetAsync(string regionName)
        {
            return repo.Where(d => d.RegionName.ToLower() == regionName.ToLower())
                .FirstOrDefaultAsync();
        }

        public Task<List<RegionT>> GetListAsync(int? countryId = null, int? governorateId = null, int? cityId = null, string regionName = null)
        {
            var query = repo.DbSet.AsQueryable();
            if (countryId.HasValue)
            {
                query = query.Where(d => d.City.Governorate.CountryId == countryId);
            }
            if (governorateId.HasValue)
            {
                query = query.Where(d => d.City.GovernorateId == governorateId);
            }
            if (cityId.HasValue)
            {
                query = query.Where(d => d.CityId == cityId);
            }
            if (regionName.IsNotNull())
            {
                query = query.Where(d => d.RegionName.Contains(regionName));
            }
            return query.ToListAsync();
        }

        public Task<int> UpdateAsync(RegionT region)
        {
            repo.Update(region.RegionId, region);
            return repo.SaveAsync();
        }
    }
}
