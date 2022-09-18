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
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IRepository<SubscriptionT> subscriptionRepo;
        private readonly IRepository<SubscriptionSequenceT> subscriptionSequenceRepo;

        public SubscriptionService(IRepository<SubscriptionT> subscriptionRepo, IRepository<SubscriptionSequenceT> subscriptionSequenceRepo)
        {
            this.subscriptionRepo = subscriptionRepo;
            this.subscriptionSequenceRepo = subscriptionSequenceRepo;
        }

        public async Task<int> AddAsync(SubscriptionT subscription)
        {
            await subscriptionRepo.AddAsync(subscription);
            return await subscriptionRepo.SaveAsync();
        }

        public async Task<int> AddSequenceAsync(SubscriptionSequenceT subscriptionSequence)
        {
            await subscriptionSequenceRepo.AddAsync(subscriptionSequence);
            return await subscriptionSequenceRepo.SaveAsync();
        }

        public async Task<int> DeletetAsync(int id)
        {
            await subscriptionRepo.DeleteAsync(id);
            return await subscriptionRepo.SaveAsync();
        }

        public async Task<int> DeletetSequenceAsync(int id)
        {
            await subscriptionSequenceRepo.DeleteAsync(id);
            return await subscriptionSequenceRepo.SaveAsync();
        }

        public Task<SubscriptionT> GetAsync(int id)
        {
            return subscriptionRepo.GetAsync(id);
        }

        public Task<List<SubscriptionSequenceT>> GetSequenceListAsync(int subscriptionId)
        {
            return subscriptionSequenceRepo.DbSet.Where(d => d.SubscriptionId == subscriptionId).ToListAsync();
        }

        public Task<List<SubscriptionT>> GetListAsync(int? departmentId = null, bool? isActive = null, bool includeDepartment = false)
        {
            var query = subscriptionRepo.DbSet.AsQueryable();
            if (includeDepartment)
            {
                query = query.Include(d => d.Department);
            }
            if (departmentId.HasValue)
            {
                query = query.Where(d => d.DepartmentId == departmentId);
            }
            if (isActive.HasValue)
            {
                query = query.Where(d => d.IsActive.Value == isActive);
            }
            return query.ToListAsync();
        }

        public Task<int> UpdateAsync(SubscriptionT subscription)
        {
            subscriptionRepo.Update(subscription.SubscriptionId, subscription);
            return subscriptionRepo.SaveAsync();
        }

        public Task<int> UpdateSequenceAsync(SubscriptionSequenceT subscriptionSequence)
        {
            subscriptionSequenceRepo.Update(subscriptionSequence.ClientSubscriptionSequenceId, subscriptionSequence);
            return subscriptionSequenceRepo.SaveAsync();
        }

        public Task<List<SubscriptionT>> GetListAsync(List<int> departmentIdList, bool? isActive = null, bool includeDepartment = false)
        {
            var query = subscriptionRepo.DbSet.AsQueryable();
            if (includeDepartment)
            {
                query = query.Include(d => d.Department);
            }
            if (departmentIdList.HasItem())
            {
                query = query.Where(d => departmentIdList.Contains(d.DepartmentId.Value));
            }
            if (isActive.HasValue)
            {
                query = query.Where(d => d.IsActive.Value == isActive);
            }
            return query.ToListAsync();
        }
    }
}
