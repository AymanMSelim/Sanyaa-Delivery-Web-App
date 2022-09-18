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
    public class CountryService : ICountryService
    {
        private readonly IRepository<CountryT> repo;

        public CountryService(IRepository<CountryT> repo)
        {
            this.repo = repo;
        }

        public async Task<int> AddAsync(CountryT country)
        {
            await repo.AddAsync(country);
            return await repo.SaveAsync();
        }

        public async Task<int> DeletetAsync(int countryId)
        {
            await repo.DeleteAsync(countryId);
            return await repo.SaveAsync();
        }

        public Task<CountryT> GetAsync(int countryId)
        {
            return repo.GetAsync(countryId);
        }

        public Task<List<CountryT>> GetListAsync(string countryName = null)
        {
            var query = repo.DbSet.AsQueryable();
            if (countryName.IsNotNull())
            {
                query = query.Where(d => d.CountryName.Contains(countryName));
            }
            return query.ToListAsync();
        }

        public Task<int> UpdateAsync(CountryT country)
        {
            repo.Update(country.CountryId, country);
            return repo.SaveAsync();
        }
    }
}
