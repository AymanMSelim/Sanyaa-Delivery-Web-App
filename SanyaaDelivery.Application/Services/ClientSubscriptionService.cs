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
    public class ClientSubscriptionService : IClientSubscriptionService
    {
        private readonly IRepository<ClientSubscriptionT> repo;

        public ClientSubscriptionService(IRepository<ClientSubscriptionT> repo)
        {
            this.repo = repo;
        }

        public async Task<int> AddAsync(ClientSubscriptionT clientSubscription)
        {
            await repo.AddAsync(clientSubscription);
            return await repo.SaveAsync();
        }

        public async Task<int> DeletetAsync(int id)
        {
            await repo.DeleteAsync(id);
            return await repo.SaveAsync();
        }

        public Task<ClientSubscriptionT> GetAsync(int id, bool includeSubscription = false)
        {
            var query = repo.DbSet.AsQueryable();
            query = query.Where(d => d.SubscriptionId == id);
            if (includeSubscription)
            {
                query = query.Include(d => d.Subscription);
            }
            return query.FirstOrDefaultAsync();
        }

        public Task<List<ClientSubscriptionT>> GetListAsync(int? clientId = null, int? departmentId = null, bool includeSubscription = false, bool includeDepartment = false)
        {
            var query = repo.DbSet.AsQueryable();
            if (includeSubscription)
            {
                if (includeDepartment)
                {
                    query = query.Include(d => d.Subscription)
                        .ThenInclude(d => new { d.Department, d.SubscriptionSequenceT });
                }
                else
                {
                    query = query.Include(d => d.Subscription).ThenInclude(d => d.SubscriptionSequenceT);
                }
            }
            if (clientId.HasValue)
            {
                query = query.Where(d => d.ClientId == clientId);
            }
            if (departmentId.HasValue)
            {
                query = query.Where(d => d.Subscription.DepartmentId == departmentId);
            }
            return query.ToListAsync();
        }

        public Task<int> UpdateAsync(ClientSubscriptionT clientSubscription)
        {
            repo.Update(clientSubscription.ClientSubscriptionId, clientSubscription);
            return repo.SaveAsync();
        }
    }
}
