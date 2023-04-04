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
    public class AppLandingScreenService : IAppLandingScreenService
    {
        private readonly IRepository<AppLandingScreenItemT> landingScreenRepository;
        private readonly IRepository<OpeningSoonDepartmentT> openingSoonDepartmentRepository;
        private readonly IClientService clientService;
        private readonly ICityService cityService;
        private readonly IRepository<LandingScreenItemDetailsT> landingScreenDetailsRepository;

        public AppLandingScreenService(IRepository<AppLandingScreenItemT> landingScreenRepository, IRepository<OpeningSoonDepartmentT> openingSoonDepartmentRepository,
            IClientService clientService, ICityService cityService, IRepository<LandingScreenItemDetailsT> landingScreenDetailsRepository)
        {
            this.landingScreenRepository = landingScreenRepository;
            this.openingSoonDepartmentRepository = openingSoonDepartmentRepository;
            this.clientService = clientService;
            this.cityService = cityService;
            this.landingScreenDetailsRepository = landingScreenDetailsRepository;
        }
        public async Task<List<AppLandingScreenItemT>> GetDepartmentListAsync(int? clientId = null)
        {
            var list = await GetListAsync(new List<int> { (int)Domain.Enum.LandingScreenItemType.Department });
            if (clientId.IsNull())
            {
                return list;
            }
            var client = await clientService.GetAsync(clientId.Value);
            if (client.BranchId.IsNull())
            {
                return list;
            }
            var branchCityList = await cityService.GetListByBranchIdAsync(client.BranchId.Value);
            if (branchCityList.IsEmpty())
            {
                return list;
            }
            var cityList = branchCityList.Select(d => d.CityId).ToList();
            var openingSoonDepartmentList = await openingSoonDepartmentRepository.DbSet.Where(d => cityList.Contains(d.CityId.Value)).ToListAsync();
            if (openingSoonDepartmentList.IsEmpty())
            {
                return list;
            }
            var departmentList = openingSoonDepartmentList.Select(d => d.DepartmentId).ToList();
            foreach (var item in list)
            {
                item.LandingScreenItemDetailsT = item.LandingScreenItemDetailsT.Where(d => !departmentList.Contains(d.DepartmentId)).ToList();
            }
            return list;
        }

        public Task<List<AppLandingScreenItemT>> GetListAsync(List<int> typeList)
        {
            return landingScreenRepository
                .Where(d => typeList.Contains(d.ItemType.Value) && d.IsActive && d.IsActive)
                .Include(d => d.LandingScreenItemDetailsT)
                .ToListAsync();
        }

        public Task<List<AppLandingScreenItemT>> GetOfferListAsync()
        {
            return GetListAsync(new List<int> { (int)Domain.Enum.LandingScreenItemType.Offer });
        }

        public Task<List<AppLandingScreenItemT>> GetBannerListAsync()
        {
            return GetListAsync(new List<int> { (int)Domain.Enum.LandingScreenItemType.Banner, (int)Domain.Enum.LandingScreenItemType.Video, (int)Domain.Enum.LandingScreenItemType.Link });
        }

        public Task<List<LandingScreenItemDetailsT>> GetDetailsItemListAsync(int itemId)
        {
            return landingScreenRepository
                .Where(d => d.ItemId == itemId)
                .Include(d => d.LandingScreenItemDetailsT)
                .SelectMany(d => d.LandingScreenItemDetailsT)
                .ToListAsync();
        }

        public Task<AppLandingScreenItemT> GetAsync(int itemId)
        {
            return landingScreenRepository
                .Where(d => d.ItemId == itemId)
                .Include(d => d.LandingScreenItemDetailsT)
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddAsync(AppLandingScreenItemT model)
        {
            await landingScreenRepository.AddAsync(model);
            return await landingScreenRepository.SaveAsync();
        }

        public Task<List<AppLandingScreenItemT>> GetListAsync(string searchValue = null, int? type = null)
        {
            var query = landingScreenRepository.DbSet.AsQueryable();
            if(string.IsNullOrEmpty(searchValue) is false)
            {
                query = query.Where(d => d.Caption.Contains(searchValue));
            }
            if (type.HasValue)
            {
                query = query.Where(d => d.ItemType == type);
            }
            return query.ToListAsync();
        }

        public Task<int> UpdateAsync(AppLandingScreenItemT model)
        {
            landingScreenRepository.Update(model.ItemId, model);
            return landingScreenRepository.SaveAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await landingScreenRepository.DeleteAsync(id);
            return await landingScreenRepository.SaveAsync();
        }

        public Task<LandingScreenItemDetailsT> GetDetailsAsync(int id)
        {
            return landingScreenDetailsRepository
                .Where(d => d.ItemId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddDetailsAsync(LandingScreenItemDetailsT model)
        {
            await landingScreenDetailsRepository.AddAsync(model);
            return await landingScreenDetailsRepository.SaveAsync();
        }

        public Task<List<LandingScreenItemDetailsT>> GetDetailsListAsync(int itemId)
        {
            return landingScreenDetailsRepository.Where(d => d.ItemId == itemId)
                .ToListAsync();
        }

        public Task<int> UpdateDetailsAsync(LandingScreenItemDetailsT model)
        {
            landingScreenDetailsRepository.Update(model.ItemId, model);
            return landingScreenDetailsRepository.SaveAsync();
        }

        public async Task<int> DeleteDetailsAsync(int id)
        {
            await landingScreenDetailsRepository.DeleteAsync(id);
            return await landingScreenDetailsRepository.SaveAsync();
        }
    }
}
