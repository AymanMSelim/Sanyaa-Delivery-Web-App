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
        Task<decimal> GetMinimumChargeAsync(int? cityId = null, int? departmentId = null);
        Task<Result<string>> ValidateClientSubscription(int clientSubscriptionId);
        Result<string> ValidateClientSubscription(ClientSubscriptionT clientSubscription);
        Task<decimal> GetDeliveryPriceAsync(int? cityId = null, int? region = null, int? departmentId = null);
        Result<T> ValidateRequest<T>(RequestT request, string employeeId = null);
    }
}
