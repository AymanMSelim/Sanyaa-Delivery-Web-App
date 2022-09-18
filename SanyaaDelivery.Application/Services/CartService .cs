using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Global.Enums;

namespace SanyaaDelivery.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<CartT> cartRepo;
        private readonly IRepository<CartDetailsT> cartDetailsRepo;
        private readonly IPromocodeService promocodeService;
        private readonly ICityService cityService;
        private readonly IClientService clientService;
        private readonly IServiceRatioService serviceRatioService;
        private readonly IClientSubscriptionService clientSubscriptionService;
        private readonly IAttachmentService attachmentService;

        public CartService(IRepository<CartT> cartRepo, IRepository<CartDetailsT> cartDetailsRepo, IPromocodeService promocodeService,
            ICityService cityService, IClientService clientService, IServiceRatioService serviceRatioService,
            IClientSubscriptionService clientSubscriptionService, IAttachmentService attachmentService)
        {
            this.cartRepo = cartRepo;
            this.cartDetailsRepo = cartDetailsRepo;
            this.promocodeService = promocodeService;
            this.cityService = cityService;
            this.clientService = clientService;
            this.serviceRatioService = serviceRatioService;
            this.clientSubscriptionService = clientSubscriptionService;
            this.attachmentService = attachmentService;
        }

        public async Task<int> AddAsync(CartT cart)
        {
            await cartRepo.AddAsync(cart);
            return await cartRepo.SaveAsync();
        }

        public async Task<int> AddDetailsAsync(CartDetailsT cartDetails)
        {
            await cartDetailsRepo.AddAsync(cartDetails);
            return await cartDetailsRepo.SaveAsync();
        }

        public async Task<CartT> AddOrReturnExistAsync(int clientId, int departmentId, bool isViaApp)
        {
            var cart = await cartRepo.DbSet.Where(d => d.ClientId == clientId && d.IsViaApp == isViaApp)
                .Include(d => d.CartDetailsT)
                .FirstOrDefaultAsync();
            if (cart.IsNotNull())
            {
                return cart;
            }
            cart = new CartT
            {
                ClientId = clientId,
                DepartmentId = departmentId,
                IsViaApp = isViaApp,
                CreationTime = DateTime.Now
            };
            await cartRepo.AddAsync(cart);
            await cartRepo.SaveAsync();
            return cart;
        }

        public async Task<CartDetailsT> AddUpdateServiceAsync(int cartId, int serviceId, int quantity)
        {
            var cart = await GetAsync(cartId, true);
            CartDetailsT cartService = cart.CartDetailsT.FirstOrDefault(d => d.ServiceId == serviceId);
            if (cartService.IsNull())
            {
                cartService = new CartDetailsT
                {
                    CartId = cartId,
                    ServiceId = serviceId,
                    ServiceQuantity = quantity > 0 ? quantity : 1,
                    CreationTime = DateTime.Now
                };
                await AddDetailsAsync(cartService);
                return cartService;
            }
            if (quantity == 0)
            {
                await DeletetDetailsAsync(cartService.CartDetailsId);
                return null;
            }
            else
            {
                cartService.ServiceQuantity = quantity;
                cartService.CreationTime = DateTime.Now;
                await UpdateDetailsAsync(cartService);
                return cartService;
            }
        }

        public async Task<OpreationResultMessage<PromocodeT>> ApplyPromocodeAsync(int cartId, string code)
        {
            var promocode = await promocodeService.GetAsync(code, true, false);
            if (promocode.IsNull())
            {
                return OpreationResultMessageFactory<PromocodeT>.CreateErrorResponse(null, OpreationResultStatusCode.NotFound, "Promocode not found");
            }
            var cart = await GetAsync(cartId, true, true, true);
            var defaultAddress = await clientService.GetDefaultAddressAsync(cart.ClientId);
            if (defaultAddress.IsNull() || defaultAddress.CityId.IsNull())
            {
                return OpreationResultMessageFactory<PromocodeT>.CreateErrorResponse(null, OpreationResultStatusCode.IncompleteClientData, "Client address not complete");
            }
            if (promocode.PromocodeDepartmentT.Any(d => d.DepartmentId == cart.DepartmentId) && promocode.PromocodeCityT.Any(d => d.CityId == defaultAddress.CityId))
            {
                if(promocode.MinimumCharge > cart.CartDetailsT.Sum(d => d.ServiceQuantity * d.Service.ServiceCost))
                {
                    return OpreationResultMessageFactory<PromocodeT>.CreateErrorResponse(null, OpreationResultStatusCode.NotAvailable, $"Request amount must be more than {promocode.MinimumCharge} to use this promocode");
                }
                cart.PromocodeId = promocode.PromocodeId;
                await UpdateAsync(cart);
                return OpreationResultMessageFactory<PromocodeT>.CreateSuccessResponse();
            }
            else
            {
                return OpreationResultMessageFactory<PromocodeT>.CreateErrorResponse(null, OpreationResultStatusCode.NotAvailable, "Promocode not avaliable for this request");
            }
        }

        public async Task<int> CancelPromocodeAsync(int cartId)
        {
            var cart = await GetAsync(cartId);
            cart.PromocodeId = null;
            return await UpdateAsync(cart);
        }

        public async Task<int> ChangeUsePointStatusAsync(int cartId)
        {
            var cart = await GetAsync(cartId);
            if (cart.UsePoint.IsNull())
            {
                cart.UsePoint = false;
            }
            cart.UsePoint = !cart.UsePoint;
            return await UpdateAsync(cart);
        }

        public async Task<int> DeletetAsync(int id)
        {
            await cartRepo.DeleteAsync(id);
            return await cartRepo.SaveAsync();
        }

        public async Task<int> DeletetByServiceIdAsync(int cartId, int serviceId)
        {
            var cartDetails = await cartDetailsRepo.Where(d => d.CartId == cartId && d.ServiceId == serviceId).FirstOrDefaultAsync();
            if (cartDetails.IsNotNull())
            {
                return await DeletetDetailsAsync(cartDetails.CartDetailsId);
            }
            return 1;
        }

        public async Task<int> DeletetDetailsAsync(int id)
        {
            await cartDetailsRepo.DeleteAsync(id);
            return await cartDetailsRepo.SaveAsync();
        }

        public Task<CartT> GetAsync(int id, bool includeDetails = false, bool includeService = false, bool includeDepartment = false)
        {
            var query = cartRepo.DbSet.Where(d => d.CartId == id);
            if (includeService)
            {
                query = query.Include(d => d.CartDetailsT).ThenInclude(d => d.Service);
            }
            if (includeDetails && !includeService)
            {
                query = query.Include(d => d.CartDetailsT);
            }
            if (includeDepartment)
            {
                query = query.Include(d => d.Department);
            }
            return query.FirstOrDefaultAsync();
        }

        public Task<CartT> GetByClientIdAsync(int clientId, bool? isViaApp = false, bool includeDetails = false, bool includeService = false)
        {
            var query = cartRepo.DbSet.Where(d => d.ClientId == clientId);
            if (isViaApp.HasValue)
            {
                query = query.Where(d => d.IsViaApp == isViaApp);
            }
            if (includeDetails)
            {
                if (includeService)
                {
                    query = query.Include(d => d.CartDetailsT)
                        .ThenInclude(d => d.Service);
                }
                else
                {
                    query = query.Include(d => d.CartDetailsT);
                }
            }
            return query.FirstOrDefaultAsync();
        }

        public async Task<CartDto> GetCartForAppAsync(int cartId, int? cityId = null)
        {

            CartDto cartDto = new CartDto();
            var cart = await GetAsync(cartId, true, true, true);
            if (cart.IsNull())
            {
                return null;
            }
            var client = await clientService.GetAsync(cart.ClientId);
            cartDto.CartId = cartId;
            cartDto.ClientId = cart.ClientId;
            cartDto.UsePoints = cart.UsePoint;
            cartDto.Note = cart.Note;
            cartDto.DepartmentId = cart.DepartmentId;
            var clientSubscriptionList = await clientSubscriptionService.GetListAsync(cartDto.ClientId, cartDto.DepartmentId, true);
            if (clientSubscriptionList.HasItem())
            {
                var firstSubscription = clientSubscriptionList.FirstOrDefault();
                cartDto.SubscriptionId = firstSubscription.SubscriptionId;
                if (firstSubscription.Subscription.IsNotNull() && firstSubscription.Subscription.SubscriptionSequenceT.HasItem())
                {
                    int requestNo = 1;
                    var sequence = requestNo % firstSubscription.Subscription.SubscriptionSequenceT.Count();
                    cartDto.SubscriptionDiscountPercentage = firstSubscription.Subscription.SubscriptionSequenceT.ToList()[sequence].DiscountPercentage;
                }
            }
            if (cityId.IsNull())
            {
                cityId = await clientService.GetDefaultCityIdAsync(cart.ClientId);
            }
            cartDto.CityId = cityId.Value;
            var city = await cityService.GetAsync(cityId.Value);
            if (city.IsNotNull())
            {
                if (city.MinimumCharge.HasValue)
                {
                    cartDto.MinimumCharge = city.MinimumCharge.Value;
                }
                else
                {
                    cartDto.MinimumCharge = GeneralSetting.MinimumCharge;
                }
                if (city.DeliveryPrice.HasValue)
                {
                    cartDto.DeliveryPrice = GeneralSetting.DeliveryPrice;
                }
                else
                {
                    cartDto.DeliveryPrice = GeneralSetting.DeliveryPrice;
                }
            }
            else
            {
                cartDto.MinimumCharge = GeneralSetting.MinimumCharge;
                cartDto.DeliveryPrice = GeneralSetting.DeliveryPrice;
            }
            cartDto.MaxDiscountPercentage = cart.Department.MaximumDiscountPercentage.Value;
            cartDto.TotalPoints = client.ClientPoints.HasValue ? client.ClientPoints.Value : 0;
            cartDto.PointsPerEGP = GeneralSetting.PointsPerEGP;
            cartDto.PromocodeCompanyDiscountPercentage = GeneralSetting.PromocodeCompanyDiscountPercentage;
            var serviceRatioList = await serviceRatioService.GetListAsync(cartDto.CityId, cartDto.DepartmentId, true);
            if (serviceRatioList.IsEmpty())
            {
                cartDto.ServiceRatio = 1;
            }
            else
            {
                var ratio = serviceRatioList.LastOrDefault().Ratio;
                if (ratio.IsNull() || ratio == 0)
                {
                    cartDto.ServiceRatio = 1;
                }
                else
                {
                    cartDto.ServiceRatio = 1 + (ratio.Value / 100);
                }
            }
            if (cart.CartDetailsT.HasItem())
            {
                cartDto.SetServiceDetails(cart.CartDetailsT.OrderBy(d => d.CartDetailsId).ToList());
                if (cart.PromocodeId.HasValue)
                {
                    var promocode = await promocodeService.GetAsync(cart.PromocodeId.Value, true, true);
                    if (promocode.PromocodeDepartmentT.Any(d => d.DepartmentId == cartDto.DepartmentId) && promocode.PromocodeCityT.Any(d => d.CityId == cartDto.CityId))
                    {
                        cartDto.CalculatePromocodeDiscount(promocode);
                    }
                }
                cartDto.SetTax(0);
                cartDto.CalculateDepartmentDiscount(0);
                cartDto.CalculateSubscriptionDiscount(0);
                cartDto.CalculateAppliedDiscount();
                cartDto.CalculateUsedPoints();
                cartDto.CheckMinimumCharge();
                cartDto.SetDeliveryPrice();
                cartDto.CalculateCompanyEmployeeDiscount();
            }
            cartDto.RoundDecimal();
            cartDto.SetDescription();
            cartDto.CalcualteDiscountDetails();
            cartDto.AttachmentList = await attachmentService.GetListAsync(1, cart.CartId.ToString());
            return cartDto;
        }

        public Task<CartDetailsT> GetDetailsAsync(int id)
        {
            return cartDetailsRepo.GetAsync(id);
        }

        public Task<List<CartDetailsT>> GetDetailsListAsync(int cartId)
        {
            return cartDetailsRepo.DbSet.Where(d => d.CartId == cartId).ToListAsync();
        }

        public Task<List<CartDetailsT>> GetDetailsListByClientIdAsync(int clientId)
        {
            return cartDetailsRepo.DbSet.Where(d => d.Cart.ClientId == clientId).ToListAsync();
        }

        public Task<List<CartT>> GetListAsync(DateTime? startDate = null, DateTime? endDate = null, bool? isViaApp = false)
        {
            var query = cartRepo.DbSet.AsQueryable();
            if (startDate.HasValue)
            {
                query = query.Where(d => d.CreationTime.Value >= startDate);
            }
            if (endDate.HasValue)
            {
                query = query.Where(d => d.CreationTime.Value <= endDate);
            }
            if (isViaApp.HasValue)
            {
                query = query.Where(d => d.IsViaApp == isViaApp);
            }
            return query.ToListAsync();
        }

        public Task<int> UpdateAsync(CartT cart)
        {
            cartRepo.Update(cart.CartId, cart);
            return cartRepo.SaveAsync();
        }

        public Task<int> UpdateDetailsAsync(CartDetailsT cartDetails)
        {
            cartDetailsRepo.Update(cartDetails.CartDetailsId, cartDetails);
            return cartDetailsRepo.SaveAsync();
        }

        public async Task<int> UpdateNoteAsync(int cartId, string note)
        {
            var cart = await GetAsync(cartId);
            if (cart.IsNull())
            {
                return 0;
            }
            cart.Note = note;
            return await UpdateAsync(cart);
        }
    }
}
