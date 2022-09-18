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
    public class PromocodeService : IPromocodeService
    {
        private readonly IRepository<PromocodeT> promocodeRepo;

        public PromocodeService(IRepository<PromocodeT> promocodeRepo)
        {
            this.promocodeRepo = promocodeRepo;
        }
        public Task<int> AddAsync(PromocodeT promocode)
        {
            throw new NotImplementedException();
        }

        public Task<int> DecreaseUsageCountAsync(PromocodeT promocode, int value = 1)
        {
            throw new NotImplementedException();
        }

        public Task<PromocodeT> GetAsync(int promocodeId, bool includeDetails = false, bool getActiveOnly = true)
        {
            var query = promocodeRepo.DbSet.Where(d => d.PromocodeId == promocodeId);
            if (includeDetails)
            {
                query = query.Include(d => d.PromocodeCityT).Include(d => d.PromocodeDepartmentT);
            }
            return query.FirstOrDefaultAsync();
        }

        public Task<PromocodeT> GetAsync(string promocode, bool includeDetails = false, bool getActiveOnly = true)
        {
            var query = promocodeRepo.Where(d => d.Promocode == promocode);
            if (includeDetails)
            {
                query = query.Include(d => d.PromocodeCityT).Include(d => d.PromocodeDepartmentT);
            }
            if (getActiveOnly)
            {
                query = query.Where(d => d.MaxUsageCount > d.UsageCount && d.ExpireTime > DateTime.Now);
            }
            return query.FirstOrDefaultAsync();
        }

        public Task<bool> IsAvailableForClientAsync(int promocodeId, int clientId)
        {
            //var 
            //var promocode = promocodeRepo.Where(d => d.MaxUsageCount > d.UsageCount && d.ExpireTime < DateTime.Now && d.PromocodeCityT.Any(t => t.CityId))
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(PromocodeT promocode)
        {
            throw new NotImplementedException();
        }
    }
}
