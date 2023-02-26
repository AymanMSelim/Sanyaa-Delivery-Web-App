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
        private readonly IRepository<SubscriptionServiceT> subscriptionServiceRepo;
        private readonly IRepository<SubscriptionSequenceT> subscriptionSequenceRepo;

        public SubscriptionService(IRepository<SubscriptionT> subscriptionRepo, 
            IRepository<SubscriptionServiceT> subscriptionServiceRepo, IRepository<SubscriptionSequenceT> subscriptionSequenceRepo)
        {
            this.subscriptionRepo = subscriptionRepo;
            this.subscriptionServiceRepo = subscriptionServiceRepo;
            this.subscriptionSequenceRepo = subscriptionSequenceRepo;
        }

        public async Task<int> AddAsync(SubscriptionT subscription)
        {
            await subscriptionRepo.AddAsync(subscription);
            return await subscriptionRepo.SaveAsync();
        }

        public async Task<int> AddSubscriptionServiceAsync(SubscriptionServiceT subscriptionService)
        {
            await subscriptionServiceRepo.AddAsync(subscriptionService);
            return await subscriptionServiceRepo.SaveAsync();
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


        public async Task<int> DeletetSubscriptionServiceAsync(int id)
        {
            await subscriptionServiceRepo.DeleteAsync(id);
            return await subscriptionServiceRepo.SaveAsync();
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

        public Task<SubscriptionServiceT> GetSubscriptionServiceAsync(int id, bool includeSubscription = false)
        {
            var query = subscriptionServiceRepo.Where(d => d.SubscriptionServiceId == id);
            if (includeSubscription)
            {
                query = query.Include(d => d.Subscription);
            }
            return query.FirstOrDefaultAsync();
        }

        public Task<SubscriptionSequenceT> GetSequenceAsync(int id)
        {
            return subscriptionSequenceRepo.GetAsync(id);
        }

        public Task<List<SubscriptionSequenceT>> GetSequenceListAsync(int subscriptionId)
        {
            return subscriptionSequenceRepo.DbSet.Where(d => d.SubscriptionService.SubscriptionId == subscriptionId).ToListAsync();
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

        public Task<int> UpdateSubscriptionServiceAsync(SubscriptionServiceT subscriptionService)
        {
            subscriptionServiceRepo.Update(subscriptionService.SubscriptionServiceId, subscriptionService);
            return subscriptionServiceRepo.SaveAsync();
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
                query = query.Where(d => departmentIdList.Contains(d.DepartmentId));
            }
            if (isActive.HasValue)
            {
                query = query.Where(d => d.IsActive.Value == isActive);
            }
            return query.ToListAsync();
        }

       

        public Task<List<SubscriptionServiceT>> GetSubscriptionServiceListAsync(int? subscriptionId = null, int? serviceId = null, bool includeService = false)
        {
            var query = subscriptionServiceRepo.DbSet.AsQueryable();
            if (subscriptionId.HasValue)
            {
                query = query.Where(d => d.SubscriptionId == subscriptionId.Value);
            }
            if (serviceId.HasValue)
            {
                query = query.Where(d => d.ServiceId == serviceId.Value);
            }
            if (includeService)
            {
                query = query.Include(d => d.Service);
            }
            return query.ToListAsync();
        }

        public async Task<List<SubscriptionT>> GetListByServiceAsync(int serviceId, bool? isActive = null, bool includeService = false, bool includeDepartment = false)
        {
            var query = subscriptionServiceRepo.Where(d => d.ServiceId == serviceId);
            if (isActive.HasValue)
            {
                query = query.Where(d => d.Subscription.IsActive == isActive.Value);
            }
            if (includeService)
            {
                query = query.Include(d => d.Service);
            }
            if (includeDepartment)
            {
                query = query.Include(d => d.Subscription).ThenInclude(d => d.Department);
            }
            var list = await query.ToListAsync();
            return list.Select(d => d.Subscription).ToList();
        }
    }
}
