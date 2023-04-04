using App.Global.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IHelperService
    {
        Task<int> GetMinimumCharge(int cityId, int departmentId);
        Task<Result<string>> ValidateClientSubscription(int clientSubscriptionId);
        Result<string> ValidateClientSubscription(ClientSubscriptionT clientSubscription);
    }
}
