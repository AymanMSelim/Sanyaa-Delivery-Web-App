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
using App.Global.DateTimeHelper;
using App.Global.DTOs;
using SanyaaDelivery.Infra.Data;
using SanyaaDelivery.Infra.Data.Context;

namespace SanyaaDelivery.Application.Services
{
    public class ClientSubscriptionService : IClientSubscriptionService
    {
        private readonly IRepository<ClientSubscriptionT> repo;
        private readonly IRepository<RequestT> requestRepository;
        private readonly IRequestService requestService;
        private readonly IUnitOfWork unitOfWork;
        private readonly SanyaaDatabaseContext dbContext;

        public ClientSubscriptionService(IRepository<ClientSubscriptionT> repo, IRepository<RequestT> requestRepository, 
            IRequestService requestService, IUnitOfWork unitOfWork, SanyaaDatabaseContext dbContext)
        {
            this.repo = repo;
            this.requestRepository = requestRepository;
            this.requestService = requestService;
            this.unitOfWork = unitOfWork;
            this.dbContext = dbContext;
        }

        public async Task<Result<ClientSubscriptionT>> AddAsync(ClientSubscriptionT clientSubscription)
        {
            clientSubscription.CreationTime = DateTime.Now.ToEgyptTime();
            var subscriptionList = await GetListAsync(clientSubscription.ClientId);
            if(subscriptionList.HasItem() && subscriptionList.Any(d => d.AddressId == clientSubscription.AddressId.Value && d.IsCanceled == false))
            {
                return ResultFactory<ClientSubscriptionT>.CreateErrorResponseMessageFD("You can't add two subscriptions on the same address!");
            }
            var subscription = await dbContext.SubscriptionT.FirstOrDefaultAsync(d => d.SubscriptionId == clientSubscription.SubscriptionId);
            if (subscription.IsContract)
            {
                clientSubscription.ExpireDate = clientSubscription.CreationTime.Value.AddMonths(subscription.NumberOfMonth);
            }
            else
            {
                clientSubscription.AutoRenew = true;
            }
            await repo.AddAsync(clientSubscription);
            var affectedRows = await repo.SaveAsync();
            return ResultFactory<ClientSubscriptionT>.CreateAffectedRowsResult(affectedRows);
        }

        public async Task<int> DeletetAsync(int id)
        {
            await repo.DeleteAsync(id);
            return await repo.SaveAsync();
        }

        public Task<ClientSubscriptionT> GetAsync(int id, bool includeSubscription = false)
        {
            var query = repo.DbSet.AsQueryable();
            query = query.Where(d => d.ClientSubscriptionId == id);
            if (includeSubscription)
            {
                query = query.Include(d => d.Subscription);
            }
            return query.FirstOrDefaultAsync();
        }

        public Task<List<ClientSubscriptionT>> GetListAsync(int? clientId = null, int? departmentId = null, bool includeSubscription = false,
            bool includeDepartment = false, bool includeSubscriptionService = false, bool includeService = false,
            bool includeAddress = false, bool includePhone = false)
        {
            var query = repo.Where(d => d.IsCanceled == false && (d.ExpireDate == null || d.ExpireDate.Value > DateTime.Now.ToEgyptTime()));
            if (includeSubscription)
            {
                if (includeDepartment)
                {
                    query = query.Include(d => d.Subscription)
                        .ThenInclude(d => d.Department);
                }
                else
                {
                    query = query.Include(d => d.Subscription);
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
            if (includeService)
            {
                query = query.Include(d => d.SubscriptionService.Service);
            }
            if(includeSubscriptionService)
            {
                query = query.Include(d => d.SubscriptionService);
            }
            if (includeAddress)
            {
                query = query.Include(d => d.Address);
            }
            if (includePhone)
            {
                query = query.Include(d => d.Phone);
            }
            return query.ToListAsync();
        }

        public async Task<int> UnSubscripe(int id, int systemUserId)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var clientSubscription = await GetAsync(id);
                if (clientSubscription.IsCanceled)
                {
                    return (int)App.Global.Enums.ResultStatusCode.Success;
                }
                clientSubscription.IsCanceled = true;
                await UpdateAsync(clientSubscription);
                var activeRequestList = await requestRepository
                    .Where(d => d.ClientSubscriptionId == id && d.IsCanceled == false && d.IsCompleted == false)
                    .ToListAsync();
                if (activeRequestList.HasItem())
                {
                    foreach (var item in activeRequestList)
                    {
                        var cancelResult = await requestService.CancelAsync(item.RequestId, "Cancel subscription", systemUserId, false);
                        if (cancelResult.IsFail)
                        {
                            return (int)App.Global.Enums.ResultStatusCode.Failed;
                        }
                    }
                }
                if (isRootTransaction)
                {
                    return await unitOfWork.CommitAsync(false);
                }
                return (int)App.Global.Enums.ResultStatusCode.Success;
            }
            catch(Exception ex)
            {
                unitOfWork.RollBack();
                App.Global.Logging.LogHandler.PublishException(ex);
                return (int)App.Global.Enums.ResultStatusCode.Exception;
            }
            finally
            {
                unitOfWork.DisposeTransaction(false);
            }
        }

        public Task<int> UpdateAsync(ClientSubscriptionT clientSubscription)
        {
            repo.Update(clientSubscription.ClientSubscriptionId, clientSubscription);
            return repo.SaveAsync();
        }
    }
}
