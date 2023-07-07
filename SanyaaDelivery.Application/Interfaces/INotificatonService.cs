using App.Global.DTOs;
using FirebaseAdmin.Messaging;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface INotificatonService
    {
        Task<Result<AppNotificationT>> SendFirebaseNotificationAsync(Domain.Enum.AccountType accountType, string referenceId, string title, string body);
        Task<Result<AppNotificationT>> SendFirebaseNotificationAsync(AppNotificationT notification);
        Task<List<AppNotificationT>> GetListAsync(int accountId, DateTime? startDate = null, DateTime? endDate = null);
        Task<Result<BatchResponse>> SendFirebaseMulticastNotificationAsync(Domain.Enum.AccountType accountType, List<string> referenceIdList, string title, string body, string imageUrl = null, string link = null);
    }
}
