using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface ISubscriptionService
    {
        Task<int> AddAsync(SubscriptionT subscription);
        Task<int> AddSubscriptionServiceAsync(SubscriptionServiceT subscriptionService);
        Task<int> AddSequenceAsync(SubscriptionSequenceT subscriptionSequence);
        Task<List<SubscriptionT>> GetListAsync(int? departmentId = null, bool? isActive = null, bool includeDepartment = false);
        Task<List<SubscriptionT>> GetListByServiceAsync(int serviceId, bool? isActive = null, bool includeService = false, bool includeDepartment = false);
        Task<List<SubscriptionT>> GetListAsync(List<int> departmentIdList, bool? isActive = null, bool includeDepartment = false);
        Task<List<SubscriptionServiceT>> GetSubscriptionServiceListAsync(int? subscriptionId = null, int? serviceId = null, bool includeService = false);
        Task<List<SubscriptionSequenceT>> GetSequenceListAsync(int subscriptionServiceId);
        Task<SubscriptionT> GetAsync(int id);
        Task<SubscriptionServiceT> GetSubscriptionServiceAsync(int id, bool includeSubscription = false);
        Task<SubscriptionSequenceT> GetSequenceAsync(int id);
        Task<int> DeletetAsync(int id);
        Task<int> DeletetSubscriptionServiceAsync(int id);
        Task<int> DeletetSequenceAsync(int id);
        Task<int> UpdateAsync(SubscriptionT subscription);
        Task<int> UpdateSubscriptionServiceAsync(SubscriptionServiceT subscriptionService);
        Task<int> UpdateSequenceAsync(SubscriptionSequenceT subscriptionSequence);
    }
}
