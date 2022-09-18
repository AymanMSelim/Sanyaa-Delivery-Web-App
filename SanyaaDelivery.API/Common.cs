using Microsoft.AspNetCore.Http;
using SanyaaDelivery.Application;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Application.Services;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SanyaaDelivery.API
{
    public class CommonService
    {
        private readonly IHttpContextAccessor context;
        private readonly ICartService cartService;
        private readonly IClientService clientService;
        private readonly ICityService cityService;

        public CommonService(IHttpContextAccessor context, ICartService cartService, IClientService clientService, ICityService cityService)
        {
            this.context = context;
            this.cartService = cartService;
            this.clientService = clientService;
            this.cityService = cityService;
        }
        public int? GetClientId(int? clientId = null)
        {
            if (IsViaApp())
            {
                var identity = context.HttpContext.User.Identity as ClaimsIdentity;
                clientId = App.Global.JWT.TokenHelper.GetReferenceId(identity);
                return clientId;
            }
            else
            {
                return clientId;
            }
        }

        public async Task<ClientT> GetClient(int? clientId = null, bool includeAddress = false, bool includePhone = false, bool includePoints = false)
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (IsViaApp())
            {
                clientId = App.Global.JWT.TokenHelper.GetReferenceId(identity);
            }
            if (clientId.HasValue)
            {
                return await clientService.GetAsync(clientId.Value);
            }
            return null;
        }

        public async Task<AddressT> GetDefaultAddress(int? clientId = null)
        {
            clientId = GetClientId(clientId);
            if (clientId.HasValue)
            {
                return await clientService.GetDefaultAddressAsync(clientId.Value);
            }
            return null;
        }

        public async Task<BranchT> GetCurrentAddressBranch(int? clientId = null)
        {
            clientId = GetClientId(clientId);
            if (clientId.HasValue)
            {
                var defaultAddress = await clientService.GetDefaultAddressAsync(clientId.Value);
                var branch = await cityService.GetCityBranchAsync(defaultAddress.CityId.Value);
                return branch;
            }
            return null;
        }

        public Task<Domain.Models.CartT> GetClientCartAsync(int? clientId = null, bool includeDetails = false)
        {
            bool isViaApp = IsViaApp();
            if (isViaApp)
            {
                clientId = GetClientId();
            }
            if (clientId.HasValue)
            {
                return cartService.GetByClientIdAsync(clientId.Value, isViaApp, includeDetails);
            }
            else
            {
                return null;
            }
        }

        public int? GetSystemUserId()
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            var claimValue = App.Global.JWT.TokenHelper.GetClaimValue(identity, "SystemUserId");
            if (string.IsNullOrEmpty(claimValue))
            {
                return null;
            }
            return int.Parse(claimValue);
        }

        public bool IsViaApp()
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            int? accountType = App.Global.JWT.TokenHelper.GetAccountType(identity);
            if (accountType.HasValue && GeneralSetting.CustomerAccountTypeId == accountType.Value)
            {
                return true;
            }
            return false;
        }
    }
}
