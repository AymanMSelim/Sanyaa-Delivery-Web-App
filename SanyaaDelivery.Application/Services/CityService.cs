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
    public class CityService : ICityService
    {
        private readonly IRepository<CityT> repo;

        public CityService(IRepository<CityT> repo)
        {
            this.repo = repo;
        }

        public async Task<int> AddAsync(CityT city)
        {
            await repo.AddAsync(city);
            return await repo.SaveAsync();
        }

        public async Task<int> DeletetAsync(int id)
        {
            await repo.DeleteAsync(id);
            return await repo.SaveAsync();
        }

        public Task<CityT> GetAsync(int id, bool includeGov = false)
        {
            if (includeGov)
            {
                return repo.DbSet.Include(d => d.Governorate).FirstOrDefaultAsync(d => d.CityId == id);
            }
            else
            {
                return repo.GetAsync(id);
            }
        }

        public Task<CityT> GetAsync(string cityName)
        {
            return repo.Where(d => d.CityName.ToLower() == cityName.ToLower()).FirstOrDefaultAsync();
        }

        public Task<BranchT> GetCityBranchAsync(int cityId)
        {
            return repo.DbSet
                .Include(d => d.Branch)
                .Where(d => d.CityId == cityId)
                .Select(d => d.Branch)
                .FirstOrDefaultAsync();
        }

        public Task<List<CityT>> GetListAsync(int? countryId = null, int? governorateId = null, string cityName = null)
        {
            var query = repo.DbSet.AsQueryable();
            if (countryId.HasValue)
            {
                query = query.Where(d => d.Governorate.CountryId == countryId);
            }
            if (governorateId.HasValue)
            {
                query = query.Where(d => d.GovernorateId == governorateId);
            }
            if (cityName.IsNotNull())
            {
                query = query.Where(d => d.CityName.Contains(cityName));
            }
            return query.ToListAsync();
        }

        public Task<List<CityT>> GetListByBranchIdAsync(int branchId)
        {
            return repo.Where(d => d.BranchId == branchId).ToListAsync();
        }

        public async Task<int> GetMinimumChargeAsync(int? cityId = null, int? branchId = null)
        {
            CityT city = null;
            if (cityId.HasValue)
            {
                city = await GetAsync(cityId.Value);
            }
            else if (branchId.HasValue)
            {
                city = await repo.Where(d => d.BranchId == branchId).FirstOrDefaultAsync();
            }
            if (city.IsNull() || city.MinimumCharge.IsNull())
            {
                return GeneralSetting.MinimumCharge;
            }
            return ((int)city.MinimumCharge);
        }

        public Task<int> UpdateAsync(CityT city)
        {
            repo.Update(city.CityId, city);
            return repo.SaveAsync();
        }
    }
}
