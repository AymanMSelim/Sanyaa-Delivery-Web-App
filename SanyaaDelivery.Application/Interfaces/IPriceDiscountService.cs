using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Domain.OtherModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IPriceDiscountService
    {
        ServicesPriceDiscountSummary GetServicesPriceDiscountSummary(List<CartDetailsT> serviceList, int serviceRatio);
        Task<ServicesPriceDiscountSummary> GetServicesPriceDiscountSummary(int cartId);
    }
}
