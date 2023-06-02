using App.Global.DTOs;
using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data;
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
        private readonly IAttachmentService attachmentService;
        private readonly IEmployeeService employeeService;
        private readonly ISubscriptionRequestService subscriptionRequestService;
        private readonly IRepository<ServiceT> serviceRepository;
        private readonly IRepository<DepartmentT> departmentRepository;
        private readonly IHelperService helperService;
        private readonly IRepository<SubscriptionServiceT> subscriptionServiceRepository;
        private readonly IUnitOfWork unitOfWork;

        public CartService(IRepository<CartT> cartRepo, IRepository<CartDetailsT> cartDetailsRepo, IPromocodeService promocodeService,
            ICityService cityService, IClientService clientService, IServiceRatioService serviceRatioService, IAttachmentService attachmentService,
            IEmployeeService employeeService, ISubscriptionRequestService subscriptionRequestService, IRepository<ServiceT> serviceRepository,
            IRepository<DepartmentT> departmentRepository, IHelperService helperService,
            IRepository<SubscriptionServiceT> subscriptionServiceRepository, IUnitOfWork unitOfWork)
        {
            this.cartRepo = cartRepo;
            this.cartDetailsRepo = cartDetailsRepo;
            this.promocodeService = promocodeService;
            this.cityService = cityService;
            this.clientService = clientService;
            this.serviceRatioService = serviceRatioService;
            this.attachmentService = attachmentService;
            this.employeeService = employeeService;
            this.subscriptionRequestService = subscriptionRequestService;
            this.serviceRepository = serviceRepository;
            this.departmentRepository = departmentRepository;
            this.helperService = helperService;
            this.subscriptionServiceRepository = subscriptionServiceRepository;
            this.unitOfWork = unitOfWork;
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
            var cart = await cartRepo.DbSet.Where(d => d.ClientId == clientId && d.IsViaApp == isViaApp && d.HaveRequest == false)
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

        public async Task<Result<CartDetailsT>> AddUpdateServiceAsync(int clientId, bool isViaApp, int serviceId, int quantity)
        {
            var service = await serviceRepository.Where(d => d.ServiceId == serviceId)
                .Include(d => d.Department.DepartmentSub0Navigation.Department).FirstOrDefaultAsync();
            if (service.IsNull())
            {
                return ResultFactory<CartDetailsT>.CreateNotFoundResponse("Service not found");
            }
            var cart = await AddOrReturnExistAsync(clientId, service.Department.DepartmentSub0Navigation.Department.DepartmentId, isViaApp);
            if (cart.IsNull())
            {
                return ResultFactory<CartDetailsT>.CreateErrorResponseMessage("No cart found");
            }
            return await AddUpdateServiceAsync(cart.CartId, serviceId, quantity);
        }

        public async Task<Result<CartDetailsT>> AddUpdateServiceAsync(int cartId, int serviceId, int quantity)
        {
            var service = await serviceRepository.Where(d => d.ServiceId == serviceId)
                .Include(d => d.Department.DepartmentSub0Navigation.Department).FirstOrDefaultAsync();
            if (service.IsNull())
            {
                return ResultFactory<CartDetailsT>.CreateNotFoundResponse("Service not found");
            }
            var cart = await GetAsync(cartId, true);
            if (cart.IsNull())
            {
                return ResultFactory<CartDetailsT>.CreateErrorResponseMessage("No cart found");
            }
            if (service.Department.DepartmentSub0Navigation.Department.DepartmentId != cart.DepartmentId)
            {
                if (cart.CartDetailsT.HasItem())
                {
                    return ResultFactory<CartDetailsT>.CreateErrorResponseMessage("Department mismatched", App.Global.Enums.ResultStatusCode.Mismatched, App.Global.Enums.ResultAleartType.FailedDialog);
                }
                else
                {
                    cart.DepartmentId = service.Department.DepartmentSub0Navigation.Department.DepartmentId;
                    await UpdateAsync(cart);
                }
            }
            if (cart.DepartmentId == GeneralSetting.CleaningDepartmentId)
            {
                if (cart.CartDetailsT.HasItem() && cart.CartDetailsT.FirstOrDefault().ServiceQuantity >= 1 && quantity > 0)
                {
                    return ResultFactory<CartDetailsT>.CreateErrorResponseMessage("Can't add more than cleaning service to the cart", App.Global.Enums.ResultStatusCode.Failed, App.Global.Enums.ResultAleartType.FailedDialog);
                }
            }
            CartDetailsT cartService = cart.CartDetailsT.FirstOrDefault(d => d.ServiceId == serviceId);
            if (cartService.IsNull())
            {
                cartService = new CartDetailsT
                {
                    CartId = cart.CartId,
                    ServiceId = serviceId,
                    ServiceQuantity = quantity > 0 ? quantity : 1,
                    CreationTime = DateTime.Now
                };
                await AddDetailsAsync(cartService);
                return ResultFactory<CartDetailsT>.CreateSuccessResponse(cartService, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully, "Added to cart", App.Global.Enums.ResultAleartType.SuccessToast);
            }
            if (quantity == 0)
            {
                await DeletetDetailsAsync(cartService.CartDetailsId);
                return ResultFactory<CartDetailsT>.CreateSuccessResponse(cartService, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully, "Removed from cart", App.Global.Enums.ResultAleartType.SuccessToast);
            }
            else
            {
                cartService.ServiceQuantity = quantity;
                cartService.CreationTime = DateTime.Now;
                await UpdateDetailsAsync(cartService);
                return ResultFactory<CartDetailsT>.CreateSuccessResponse(cartService, App.Global.Enums.ResultStatusCode.RecordUpdatedSuccessfully, "Cart updated", App.Global.Enums.ResultAleartType.SuccessToast);
            }
        }


        public async Task<Result<PromocodeT>> ApplyPromocodeAsync(int cartId, string code)
        {
            bool isHaveServiceWithNoPromocodeDiscount = await cartDetailsRepo.DbSet.AnyAsync(d => d.CartId == cartId && d.Service.NoPromocodeDiscount);
            if (isHaveServiceWithNoPromocodeDiscount)
            {
                return ResultFactory<PromocodeT>.CreateErrorResponseMessageFD("Promocode discount not avalible for this service");
            }
            var promocode = await promocodeService.GetAsync(code, true);
            if (promocode.IsNull())
            {
                return ResultFactory<PromocodeT>.CreateErrorResponse(null, ResultStatusCode.NotFound, "Promocode not found");
            }
            var cart = await GetAsync(cartId, true, true, true);
            var defaultCityId = await clientService.GetDefaultAddressCityIdAsync(cart.ClientId);
            if (defaultCityId.IsNull())
            {
                return ResultFactory<PromocodeT>.CreateErrorResponse(null, ResultStatusCode.IncompleteClientData, "Client address not complete");
            }
            if (promocode.PromocodeDepartmentT.Any(d => d.DepartmentId == cart.DepartmentId) && promocode.PromocodeCityT.Any(d => d.CityId == defaultCityId.Value))
            {
                if(promocode.MinimumCharge > cart.CartDetailsT.Sum(d => d.ServiceQuantity * d.Service.ServiceCost))
                {
                    return ResultFactory<PromocodeT>.CreateErrorResponse(null, ResultStatusCode.NotAvailable, $"Request amount must be more than {promocode.MinimumCharge} to use this promocode");
                }
                cart.PromocodeId = promocode.PromocodeId;
                cart.UsePoint = false;
                await UpdateAsync(cart);
                return ResultFactory<PromocodeT>.CreateSuccessResponse();
            }
            else
            {
                return ResultFactory<PromocodeT>.CreateErrorResponse(null, ResultStatusCode.NotAvailable, "Promocode not avaliable for this request");
            }
        }

        public async Task<int> CancelPromocodeAsync(int cartId)
        {
            var cart = await GetAsync(cartId);
            cart.PromocodeId = null;
            return await UpdateAsync(cart);
        }

        public async Task<Result<object>> ChangeUsePointStatusAsync(int cartId)
        {
            var cart = await GetAsync(cartId);
            if (cart.UsePoint.IsNull())
            {
                cart.UsePoint = false;
            }
            cart.UsePoint = !cart.UsePoint;
            if (cart.UsePoint)
            {
                bool isHaveServiceWithNoPointDiscount = await cartDetailsRepo.DbSet.AnyAsync(d => d.CartId == cartId && d.Service.NoPointDiscount);
                if (isHaveServiceWithNoPointDiscount)
                {
                    return ResultFactory<object>.CreateErrorResponseMessageFD("Point discount not avalible for this service");
                }
            }
            if (cart.UsePoint)
            {
                var customCart = await GetCartForAppAsync(cartId);
                if(customCart.MinimumCharge > customCart.TotalPrice)
                {
                    cart.UsePoint = false;
                }
                cart.PromocodeId = null;
            }
            var affectedRows = await UpdateAsync(cart);
            return ResultFactory<object>.CreateAffectedRowsResult(affectedRows);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var cartDelails = await GetDetailsListAsync(id);
            foreach (var item in cartDelails)
            {
                await cartDetailsRepo.DeleteAsync(item.CartDetailsId);
            }
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

        public Task<CartT> GetCurrentByClientIdAsync(int clientId, bool? isViaApp = false, bool includeDetails = false, bool includeService = false)
        {
            var query = cartRepo.DbSet.Where(d => d.ClientId == clientId && d.HaveRequest == false);
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

        public async Task<CartDto> GetCartForAppAsync(int cartId, int? cityId = null, int? clientSubscriptionId = null, DateTime? requestTime = null, RequestT request = null)
        {
            AddressT address;
            CartDto cartDto = new CartDto();
            var cart = await GetAsync(cartId, true, true);
            if (cart.IsNull())
            {
                return null;
            }
            cartDto.CartId = cartId;
            cartDto.ClientId = cart.ClientId;
            cartDto.UsePoints = cart.UsePoint;
            cartDto.Note = cart.Note;
            cartDto.DepartmentId = cart.DepartmentId;
            var departmentDetails = await departmentRepository.Where(d => d.DepartmentId == cart.DepartmentId)
                .Select(d => new { d.DepartmentName, d.Terms, d.IncludeDeliveryPrice, d.MaximumDiscountPercentage }).FirstOrDefaultAsync();
            cartDto.DepartmentName = departmentDetails.DepartmentName;
            cartDto.DepartmentTerms = departmentDetails.Terms;
            if(request.IsNotNull())
            {
                cartDto.HaveRequest = true;
                cartDto.Request = request;
            }
            cartDto.EmployeeDepartmentPercentage = await employeeService.GetEmployeePrecentageAsync(cartDto.EmployeeId, cartDto.DepartmentId);
            if (cityId.IsNull())
            {
                address = await clientService.GetDefaultAddressAsync(cart.ClientId);
                //address = await clientService.getaddress(cart.ClientId);
            }
            else
            {
                address = await clientService.GetDefaultAddressAsync(cart.ClientId);    
            }
            var defaultAddress = await clientService.GetDefaultAddressAsync(cart.ClientId);
            if (departmentDetails.IncludeDeliveryPrice)
            {
                cartDto.DeliveryPrice = await helperService.GetDeliveryPriceAsync(address.CityId, address.RegionId, cart.DepartmentId);
            }
            if (departmentDetails.MaximumDiscountPercentage.HasValue)
            {
                cartDto.MaxDiscountPercentage = departmentDetails.MaximumDiscountPercentage.Value;
            }
            if (cart.CartDetailsT.Any(d => d.Service.NoMinimumCharge == false))
            {
                cartDto.MinimumCharge = await helperService.GetMinimumChargeAsync(address.CityId, cart.DepartmentId);
            }
            cartDto.TotalPoints =  await clientService.GetPointAsync(cart.ClientId);
            cartDto.PointsPerEGP = GeneralSetting.PointsPerEGP;
            cartDto.ServiceRatio = await serviceRatioService.GetRatioAsync(cartDto.CityId, cartDto.DepartmentId);
            if (clientSubscriptionId.HasValue)
            {
                cartDto.IgnoreServiceDiscount = await subscriptionServiceRepository.Where(d => d.SubscriptionServiceId == clientSubscriptionId)
                    .Select(d => d.Subscription.IgnoreServiceDiscount).FirstOrDefaultAsync();
                cartDto.ClientSubscriptionId = clientSubscriptionId.Value;
                if(requestTime == null)
                {
                    requestTime = DateTime.Now;
                }
                var sequence = await subscriptionRequestService.GetNextSequenceAsync(clientSubscriptionId.Value, requestTime.Value);
                cartDto.SubscriptionDiscountPercentage = sequence.DiscountPercentage;
                cartDto.SubscriptionDiscountCompanyPercentage = sequence.CompanyDiscountPercentage;
            }
            cartDto.PointsCompanyDiscountPercentage = GeneralSetting.PointsCompanyDiscountPercentage;
            if (cart.CartDetailsT.HasItem())
            {
                cartDto.SetServiceDetails(cart.CartDetailsT.OrderBy(d => d.CartDetailsId).ToList());
                cartDto.SetTax(0);
                cartDto.CalculateDepartmentDiscount(0);
                cartDto.CalculateSubscriptionDiscount();
                cartDto.CalculateAppliedDiscount();
                cartDto.CalculateUsedPoints();
                cartDto.CheckMinimumCharge();
                cartDto.SetDeliveryPrice();
                cartDto.CalculateCompanyEmployeeDiscount();
                if (cart.PromocodeId.HasValue)
                {
                    var promocode = await promocodeService.GetAsync(cart.PromocodeId.Value, true);
                    if (promocode.IsNotNull())
                    {
                        cartDto.PromocodeCompanyDiscountPercentage = promocode.CompanyDiscountPercentage;
                        cartDto.CalculatePromocodeDiscount(promocode);
                    }
                }
            }
            cartDto.RoundDecimal();
            cartDto.SetDescription();
            cartDto.CalcualteDiscountDetails();
            cartDto.CalcualteInvoicetDetails();
            cartDto.CalculateAmountPercentage();
            cartDto.AttachmentList = await attachmentService.GetListAsync((int)Domain.Enum.AttachmentType.CartImage, cart.CartId.ToString());
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

        public Task<List<CartDetailsT>> GetDetailsListByClientIdAsync(int clientId, bool haveRequest = false)
        {
            return cartDetailsRepo.DbSet.Where(d => d.Cart.ClientId == clientId && d.Cart.HaveRequest == false).ToListAsync();
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
