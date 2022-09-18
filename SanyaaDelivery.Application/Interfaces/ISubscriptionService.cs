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
        Task<int> AddSequenceAsync(SubscriptionSequenceT subscriptionSequence);
        Task<List<SubscriptionT>> GetListAsync(int? departmentId = null, bool? isActive = null, bool includeDepartment = false);
        Task<List<SubscriptionT>> GetListAsync(List<int> departmentIdList, bool? isActive = null, bool includeDepartment = false);
        Task<List<SubscriptionSequenceT>> GetSequenceListAsync(int subscriptionId);
        Task<SubscriptionT> GetAsync(int id);
        Task<int> DeletetAsync(int id);
        Task<int> DeletetSequenceAsync(int id);
        Task<int> UpdateAsync(SubscriptionT subscription);
        Task<int> UpdateSequenceAsync(SubscriptionSequenceT subscriptionSequence);
    }
}
