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

        public Task<List<GovernorateT>> GetList(int? countryId)
        {
            var query = repo.DbSet.AsQueryable();
            if (countryId.HasValue)
            {
                query = query.Where(d => d.CountryId == countryId);
            }
            return query.ToListAsync();
        }
    }
}
