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

        public Task<List<RegionT>> GetList(int? cityId)
        {
            var query = repo.DbSet.AsQueryable();
            if (cityId.HasValue)
            {
                query = query.Where(d => d.CityId == cityId);
            }
            return query.ToListAsync();
        }
    }
}
