﻿using App.Global.DTOs;
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
using FirebaseAdmin.Messaging;

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
            query = query.OrderByDescending(d => d.CreationTime);
            query = query.Take(50);
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
            if (accountType == AccountType.Client)
            {
                await App.Global.Firebase.FirebaseMessaging.SendToClientAsync(account.FcmToken, notification.Title, notification.Body, notification.Image);
            }
            else if (accountType == AccountType.Employee)
            {
                await App.Global.Firebase.FirebaseMessaging.SendToEmpAsync(account.FcmToken, notification.Title, notification.Body, notification.Image);
            }
            return ResultFactory<AppNotificationT>.CreateSuccessResponse(notification);
        }

        public async Task<Result<AppNotificationT>> SendFirebaseNotificationAsync(Domain.Enum.AccountType accountType, string referenceId, string title, string body)
        {
            try
            {
                int accountTypeInt = (int)accountType;
                var accountId = await accountRepository.Where(d => d.AccountTypeId == accountTypeInt && d.AccountReferenceId == referenceId)
                    .Select(d => d.AccountId)
                    .FirstOrDefaultAsync();
                return await SendFirebaseNotificationAsync(new AppNotificationT
                {
                    AccountId = accountId,
                    Body = body,
                    Title = title,
                });
            }
            catch (Exception ex)
            {
                return ResultFactory<AppNotificationT>.CreateExceptionResponse(ex);
            }
        }

        public async Task<Result<BatchResponse>> SendFirebaseMulticastNotificationAsync(Domain.Enum.AccountType accountType, List<string> referenceIdList, string title, string body, string imageUrl = null, string link = null)
        {
            try
            {
                int accountTypeInt = (int)accountType;
                var accountDetailsList = await accountRepository.Where(d => d.AccountTypeId == accountTypeInt && referenceIdList.Contains(d.AccountReferenceId))
                     .Where(d => d.FcmToken != null)
                     .Select(d => new { d.FcmToken, d.AccountId })
                    .ToListAsync();
                List<AppNotificationT> list = accountDetailsList.Select(d => new AppNotificationT
                {
                    AccountId = d.AccountId,
                    Body = body,
                    CreationTime = DateTime.Now.EgyptTimeNow(),
                    Title = title,
                    Image = imageUrl,
                    Link = link
                }).ToList();
                await notificationRepository.DbSet.AddRangeAsync(list);
                await notificationRepository.SaveAsync();
                var result = await App.Global.Firebase.FirebaseMessaging.SendMulticastToEmpAsync(accountDetailsList.Select(d => d.FcmToken).ToList(), title, body, imageUrl);
                return ResultFactory<BatchResponse>.CreateSuccessResponse(result);
            }
            catch (Exception ex)
            {
                return ResultFactory<BatchResponse>.CreateExceptionResponse(ex);
            }
        }
    }
}
