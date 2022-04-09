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

        public Task<List<CityT>> GetList(int? governorateId)
        {
            var query = repo.DbSet.AsQueryable();
            if (governorateId.HasValue)
            {
                query = query.Where(d => d.GovernorateId == governorateId);
            }
            return query.ToListAsync();
        }
    }
}
