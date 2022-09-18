using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IClientSubscriptionService
    {
        Task<int> AddAsync(ClientSubscriptionT clientSubscription);
        Task<List<ClientSubscriptionT>> GetListAsync(int? clientId = null, int? departmentId = null, bool includeSubscription = false, bool includeDepartment = false);
        Task<ClientSubscriptionT> GetAsync(int id, bool includeSubscription = false);
        Task<int> DeletetAsync(int id);
        Task<int> UpdateAsync(ClientSubscriptionT clientSubscription);
    }
}
