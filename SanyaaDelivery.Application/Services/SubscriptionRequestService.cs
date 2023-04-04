using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App.Global.ExtensionMethods;
using SanyaaDelivery.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SanyaaDelivery.Application.Services
{
    public class SubscriptionRequestService : ISubscriptionRequestService
    {
        private readonly IRepository<RequestT> requestRepository;
        private readonly IRepository<ClientSubscriptionT> clientSubscriptionRepository;

        //private readonly ISubscriptionService subscriptionService;
        //private readonly IClientSubscriptionService clientSubscriptionService;

        public SubscriptionRequestService(IRepository<RequestT> requestRepository, IRepository<ClientSubscriptionT> clientSubscriptionRepository)
        {
            this.requestRepository = requestRepository;
            this.clientSubscriptionRepository = clientSubscriptionRepository;
            //this.subscriptionService = subscriptionService;
            //this.clientSubscriptionService = clientSubscriptionService;
        }
        public async Task<SubscriptionSequenceT> GetNextSequenceAsync(int clientSubscriptionId, DateTime requestTime)
        {
            SubscriptionSequenceT subscriptionSequence = new SubscriptionSequenceT();
            int requestCount;
            var clientSubscription = await clientSubscriptionRepository.Where(d => d.ClientSubscriptionId == clientSubscriptionId)
                .Include(d => d.SubscriptionService)
                .ThenInclude(d => d.SubscriptionSequenceT)
                .FirstOrDefaultAsync();
            var subscriptionRequestCountQuery = requestRepository.DbSet.Where(d => d.ClientId == clientSubscription.ClientId && d.IsCanceled == false &&
            d.RequestTimestamp >= App.Global.DateTimeHelper.DateTimeHelperService.GetStartDateOfMonthS(requestTime) &&
            d.RequestTimestamp <= App.Global.DateTimeHelper.DateTimeHelperService.GetEndDateOfMonthS(requestTime) &&
            d.ClientSubscriptionId == clientSubscriptionId);
            requestCount = await subscriptionRequestCountQuery.CountAsync();
            var sequenceList = clientSubscription.SubscriptionService.SubscriptionSequenceT.ToList();
            if (sequenceList.IsEmpty())
            {
                return subscriptionSequence;
            }
            var nextRequestIndex = requestCount % sequenceList.Count;
            if (sequenceList.IsEmpty())
            {
                return null;
            }
            subscriptionSequence =  sequenceList[nextRequestIndex];
            return subscriptionSequence;
        }

        public async Task<bool> IsExceedSubscriptionLimitAsync(int clientSubscriptionId, DateTime requestTime)
        {
            int requestCount;
            var clientSubscription = await clientSubscriptionRepository.Where(d => d.ClientSubscriptionId == clientSubscriptionId)
                .Include(d => d.Subscription)
                .FirstOrDefaultAsync();
            var subscriptionRequestCountQuery = requestRepository.DbSet.Where(d => d.ClientId == clientSubscription.ClientId && d.IsCanceled == false &&
            d.RequestTimestamp >= App.Global.DateTimeHelper.DateTimeHelperService.GetStartDateOfMonthS(requestTime) &&
            d.RequestTimestamp <= App.Global.DateTimeHelper.DateTimeHelperService.GetEndDateOfMonthS(requestTime) &&
            d.ClientSubscriptionId == clientSubscriptionId);
            requestCount = await subscriptionRequestCountQuery.CountAsync();
            if(requestCount >= clientSubscription.Subscription.RequestNumberPerMonth)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> IsExceedContractSubscriptionLimitAsync(int clientSubscriptionId, DateTime requestTime)
        {
            int requestCount;
            var clientSubscription = await clientSubscriptionRepository
                .Where(d => d.ClientSubscriptionId == clientSubscriptionId && requestTime >= d.CreationTime  && requestTime <= d.ExpireDate)
                .Include(d => d.Subscription)
                .OrderByDescending(d => d.ExpireDate)
                .FirstOrDefaultAsync();
            if (clientSubscription.IsNull())
            {
                return true;
            }
            var subscriptionRequestCountQuery = requestRepository.DbSet.Where(d => d.ClientId == clientSubscription.ClientId
            && d.IsCanceled == false && d.ClientSubscriptionId == clientSubscriptionId);
            requestCount = await subscriptionRequestCountQuery.CountAsync();
            if (requestCount >= (clientSubscription.Subscription.RequestNumberPerMonth * clientSubscription.Subscription.NumberOfMonth))
            {
                return true;
            }
            return false;
        }
    }
}
