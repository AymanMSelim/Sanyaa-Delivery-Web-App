using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Domain.OtherModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface ISubscriptionRequestService
    {
        Task<bool> IsContract(int clientSubscriptionId);
        Task<bool> IsExpiredAsync(int clientSubscriptionId, DateTime requestTime);
        Task<SubscriptionSequenceT> GetNextSequenceAsync(int clientSubscriptionId, DateTime requestTime);
        Task<bool> IsExceedSubscriptionLimitAsync(int clientSubscriptionId, DateTime requestTime);
        Task<bool> IsExceedContractSubscriptionLimitAsync(int clientSubscriptionId, DateTime requestTime);
        Task<SubscriptionDateModel> GetSubscriptionDates(int clientSubscriptionId, DateTime requestTime);
    }
}
