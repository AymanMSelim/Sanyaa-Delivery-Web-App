using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Enum;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Global.DateTimeHelper;
namespace SanyaaDelivery.Application.Services
{
    public class NotificatonService : INotificatonService
    {
        private readonly IRepository<AppNotificationT> notificationRepository;
        private readonly IRepository<AccountT> accountRepository;

        public NotificatonService(IRepository<AppNotificationT> notificationRepository, IRepository<AccountT> accountRepository)
        {
            this.notificationRepository = notificationRepository;
            this.accountRepository = accountRepository;
        }
        public Task<List<AppNotificationT>> GetListAsync(int accountId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = notificationRepository.Where(d => d.AccountId == accountId);
            if (startDate.HasValue)
            {
                query = query.Where(d => d.CreationTime >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                query = query.Where(d => d.CreationTime <= endDate.Value);
            }
            return query.ToListAsync();
        }

        public async Task<Result<AppNotificationT>> SendFirebaseNotificationAsync(AppNotificationT notification)
        {
            var account = await accountRepository.Where(d => d.AccountId == notification.AccountId)
                .Select(d => new { d.AccountTypeId, d.FcmToken })
                .FirstOrDefaultAsync();
            if (account.IsNull())
            {
                return ResultFactory<AppNotificationT>.CreateNotFoundResponse();
            }
            notification.CreationTime = DateTime.Now.EgyptTimeNow();
            await notificationRepository.AddAsync(notification);
            await notificationRepository.SaveAsync();
            AccountType accountType = (AccountType)account.AccountTypeId;
            if(accountType == AccountType.Client)
            {
                await App.Global.Firebase.FirebaseMessaging.SendToClientAsync(account.FcmToken, notification.Title, notification.Body, notification.Image);
            }
            else if(accountType == AccountType.Employee)
            {
                await App.Global.Firebase.FirebaseMessaging.SendToEmpAsync(account.FcmToken, notification.Title, notification.Body, notification.Image);
            }
            return ResultFactory<AppNotificationT>.CreateSuccessResponse(notification);
        }
    }
}
