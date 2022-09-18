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

        public AppLandingScreenService(IRepository<AppLandingScreenItemT> landingScreenRepository, IRepository<OpeningSoonDepartmentT> openingSoonDepartmentRepository, IClientService clientService, ICityService cityService)
        {
            this.landingScreenRepository = landingScreenRepository;
            this.openingSoonDepartmentRepository = openingSoonDepartmentRepository;
            this.clientService = clientService;
            this.cityService = cityService;
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
                .Where(d => typeList.Contains(d.ItemType.Value) && d.IsActive.HasValue && d.IsActive.Value)
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
    }
}
