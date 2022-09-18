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
    public class GovernorateService : IGovernorateService
    {
        private readonly IRepository<GovernorateT> repo;

        public GovernorateService(IRepository<GovernorateT> repo)
        {
            this.repo = repo;
        }

        public async Task<int> AddAsync(GovernorateT governorate)
        {
            await repo.AddAsync(governorate);
            return await repo.SaveAsync();
        }

        public async Task<int> DeletetAsync(int governorateId)
        {
            await repo.DeleteAsync(governorateId);
            return await repo.SaveAsync();
        }

        public Task<GovernorateT> GetAsync(int id)
        {
            return repo.GetAsync(id);
        }

        public Task<GovernorateT> GetAsync(string governorateName)
        {
            return repo.Where(d => d.GovernorateName.ToLower() == governorateName.ToLower())
                .FirstOrDefaultAsync();
        }

        public Task<List<GovernorateT>> GetListAsync(int? countryId, string governorateName = null)
        {
            var query = repo.DbSet.AsQueryable();
            if (countryId.HasValue)
            {
                query = query.Where(d => d.CountryId == countryId);
            }
            if (governorateName.IsNotNull())
            {
                query = query.Where(d => d.GovernorateName.Contains(governorateName));
            }
            return query.ToListAsync();
        }

        public Task<int> UpdateAsync(GovernorateT governorate)
        {
            repo.Update(governorate.GovernorateId, governorate);
            return repo.SaveAsync();
        }
    }
}
