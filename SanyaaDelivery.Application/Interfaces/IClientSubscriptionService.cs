using App.Global.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IClientSubscriptionService
    {
        Task<Result<ClientSubscriptionT>> AddAsync(ClientSubscriptionT clientSubscription);
        Task<List<ClientSubscriptionT>> GetListAsync(int? clientId = null, int? departmentId = null,
            bool includeSubscription = false, bool includeDepartment = false, bool includeSubscriptionService = false,
            bool includeService = false, bool includeAddress = false, bool includePhone = false);
        Task<ClientSubscriptionT> GetAsync(int id, bool includeSubscription = false);
        Task<int> DeletetAsync(int id);
        Task<int> UnSubscripe(int id, int systemUserId);
        Task<int> UpdateAsync(ClientSubscriptionT clientSubscription);
        Task<List<SubscriberDto>> GetSubscriberListAsync(int? clientId = null, string clientName = null, string clientPhone = null,
            int? branchId = null, int? departmentId = null, bool? isExpired = null, bool? isActive = null, bool? isCanceled = null, int? subscriptionId = null, bool? isContract = null);
    }
}
