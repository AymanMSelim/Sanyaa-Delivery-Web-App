using App.Global.ExtensionMethods;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Domain.OtherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class PriceDiscountService : IPriceDiscountService
    {
        private readonly ICartService cartService;
        private readonly IServiceRatioService serviceRatioService;
        private readonly IClientService clientService;
        private readonly IClientSubscriptionService clientSubscriptionService;

        public PriceDiscountService(ICartService cartService, IServiceRatioService serviceRatioService, IClientService clientService,
            IClientSubscriptionService clientSubscriptionService)
        {
            this.cartService = cartService;
            this.serviceRatioService = serviceRatioService;
            this.clientService = clientService;
            this.clientSubscriptionService = clientSubscriptionService;
        }
        public ServicesPriceDiscountSummary GetServicesPriceDiscountSummary(List<CartDetailsT> serviceList, int serviceRatio)
        {
            var summary = new ServicesPriceDiscountSummary();
            int discount = 0;
            decimal totalPrice;
            foreach (var item in serviceList)
            {
                discount += Convert.ToInt32(item.Service.NoDiscount) * (Convert.ToInt32(item.Service.ServiceDiscount) / 100) * Convert.ToInt32(item.ServiceQuantity / item.Service.DiscountServiceCount) * Convert.ToInt32(item.ServiceQuantity);
            }
            totalPrice = serviceList.Sum(d => d.Service.ServiceCost * d.ServiceQuantity);
            summary.TotalPrice = totalPrice;
            summary.ServiceRatio = serviceRatio;
            summary.TotalRatioPrice = totalPrice + (totalPrice * serviceRatio);
            summary.TotalDiscount = discount;
            summary.TotalPoints = serviceList.Sum(d => d.Service.ServicePoints.Value * d.ServiceQuantity);
            summary.NetPrice = summary.TotalRatioPrice - summary.TotalDiscount;
            return summary;
        }

        public async Task<ServicesPriceDiscountSummary> GetServicesPriceDiscountSummary(int cartId)
        {
            int ratio = 0;
            var cart = await cartService.GetAsync(cartId, true, true);
            if (cart.IsNull())
            {
                return null;
            }
            var serviceRatioList = serviceRatioService.GetListByClientIdAsync(cart.ClientId);
            return GetServicesPriceDiscountSummary(cart.CartDetailsT.ToList(), ratio);
        }
    }
}
