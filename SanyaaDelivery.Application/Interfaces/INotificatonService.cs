using App.Global.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface INotificatonService
    {
        Task<Result<AppNotificationT>> SendFirebaseNotificationAsync(AppNotificationT notification);
        Task<List<AppNotificationT>> GetListAsync(int accountId, DateTime? startDate = null, DateTime? endDate = null);
    }
}
