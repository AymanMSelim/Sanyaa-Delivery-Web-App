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
    public class FavouriteServiceService : IFavouriteServiceService
    {
        private readonly IRepository<FavouriteServiceT> repo;

        public FavouriteServiceService(IRepository<FavouriteServiceT> repo)
        {
            this.repo = repo;
        }

        public async Task<int> AddAsync(FavouriteServiceT favouriteService)
        {
            await repo.AddAsync(favouriteService);
            return await repo.SaveAsync();
        }

        public async Task<int> DeletetAsync(int id)
        {
            await repo.DeleteAsync(id);
            return await repo.SaveAsync();
        }

        public async Task<int> FavouriteSwitch(int clientId, int serviceId)
        {
            var serviceFavouriteList = await GetListAsync(clientId);
            if (serviceFavouriteList.HasItem())
            {
                var selectedFavouriteService = serviceFavouriteList.FirstOrDefault(d => d.ServiceId == serviceId);
                if (selectedFavouriteService.IsNotNull())
                {
                    return await DeletetAsync(selectedFavouriteService.FavouriteServiceId);
                }
            }
            var newFavouriteService = new FavouriteServiceT
            {
                ClientId = clientId,
                ServiceId = serviceId,
                CreationTime = DateTime.Now
            };
            return await AddAsync(newFavouriteService);
        }

        public Task<FavouriteServiceT> GetAsync(int id, bool includeService = false)
        {
            var query = repo.DbSet.AsQueryable();
            query = query.Where(d => d.FavouriteServiceId == id);
            if (includeService)
            {
                query = query.Include(d => d.Service);
            }
            return query.FirstOrDefaultAsync();
        }

        public Task<List<FavouriteServiceT>> GetListAsync(int? clientId, bool includeService = false)
        {
            var query = repo.DbSet.AsQueryable();
            if (includeService)
            {
                query = query.Include(d => d.Service);
            }
            if (clientId.HasValue)
            {
                query = query.Where(d => d.ClientId == clientId);
            }
            return query.ToListAsync();
        }

        public Task<int> UpdateAsync(FavouriteServiceT favouriteService)
        {
            repo.Update(favouriteService.FavouriteServiceId, favouriteService);
            return repo.SaveAsync();
        }
    }
}
