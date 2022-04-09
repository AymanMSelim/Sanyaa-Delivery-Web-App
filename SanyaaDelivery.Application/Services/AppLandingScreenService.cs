using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class AppLandingScreenService : IAppLandingScreenService
    {
        private readonly IRepository<AppLandingScreenItemT> landingScreenRepository;

        public AppLandingScreenService(IRepository<AppLandingScreenItemT> landingScreenRepository)
        {
            this.landingScreenRepository = landingScreenRepository;
        }
        public Task<List<AppLandingScreenItemT>> GetDepartmentListAsync()
        {
            return GetListAsync((int)Domain.Enum.LandingScreenItemType.Department);
        }

        public Task<List<AppLandingScreenItemT>> GetListAsync(int type)
        {
            return landingScreenRepository
                .Where(d => d.ItemType == type)
                .ToListAsync();
        }

        public Task<List<AppLandingScreenItemT>> GetOfferListAsync()
        {
            return GetListAsync((int)Domain.Enum.LandingScreenItemType.Offer);
        }

        public Task<List<AppLandingScreenItemT>> GetBannerListAsync()
        {
            return GetListAsync((int)Domain.Enum.LandingScreenItemType.Banner);
        }
    }
}
