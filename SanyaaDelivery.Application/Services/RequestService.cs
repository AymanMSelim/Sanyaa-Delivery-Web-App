using App.Global.DateTimeHelper;
using App.Global.DTOs;
using App.Global.ExtensionMethods;
using App.Global.Translation;
using App.Global.WhatsApp;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SanyaaDelivery.Application.Services
{
    public class RequestService : IRequestService
    {
        #region Order Status

        static readonly List<int> CompleteStatus = new List<int>
        {
            14 ,20
        };

        static readonly List<int> WaitingStatus = new List<int>
        {
            11, 12, 1
        };

        static readonly List<int> InExcution = new List<int>
        {
            13
        };

        static readonly List<int> Rejected = new List<int>
        {
            2
        };
        #endregion

        private readonly IRepository<RequestT> requestRepository;
        private readonly IRepository<RequestServicesT> requestServiceRepository;
        private readonly IRepository<AddressT> addressRepository;
        private readonly IRepository<RequestCanceledT> cancelRequestRepository;
        private readonly IRepository<RequestDelayedT> delayRequestRepository;
        private readonly IRepository<RequestComplaintT> complaintRequestRepository;
        private readonly ICartService cartService;
        private readonly IRepository<ClientPhonesT> phoneRepository;
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly IRepository<ClientSubscriptionT> clientSubscriptionRepository;

        private readonly ISubscriptionRequestService subscriptionRequestService;
        private readonly ICityService cityService;
        private readonly WhatsAppService whatsAppService;

        private readonly IClientPointService clientPointService;
        private readonly IEmployeeRequestService employeeRequestService;
        private readonly Translator translationService;
        private readonly IUnitOfWork unitOfWork;

        public RequestService(IRepository<RequestT> requestRepository, IRepository<RequestCanceledT> cancelRequestRepository,
            IRepository<RequestDelayedT> delayRequestRepository, IRepository<RequestComplaintT> complaintRequestRepository,
            IRepository<RequestServicesT> requestServiceRepository, IRepository<AddressT> addressRepository,
            ICartService cartService, IRepository<ClientPhonesT> phoneRepository, IRepository<EmployeeT> employeeRepository,
            IRepository<ClientSubscriptionT> clientSubscriptionRepository, ISubscriptionRequestService subscriptionRequestService,
            ICityService cityService, WhatsAppService whatsAppService,
            IClientPointService clientPointService, IEmployeeRequestService employeeRequestService, Translator translationService, IUnitOfWork unitOfWork)
        {
            this.requestRepository = requestRepository;
            this.cancelRequestRepository = cancelRequestRepository;
            this.delayRequestRepository = delayRequestRepository;
            this.complaintRequestRepository = complaintRequestRepository;
            //this.clientService = clientService;
            this.requestServiceRepository = requestServiceRepository;
            this.addressRepository = addressRepository;
            this.cartService = cartService;
            this.phoneRepository = phoneRepository;
            this.employeeRepository = employeeRepository;
            this.clientSubscriptionRepository = clientSubscriptionRepository;
            //this.clientSubscriptionService = clientSubscriptionService;
            this.subscriptionRequestService = subscriptionRequestService;
            this.cityService = cityService;
            this.whatsAppService = whatsAppService;
            //this.subscriptionService = subscriptionService;
            this.clientPointService = clientPointService;
            this.employeeRequestService = employeeRequestService;
            this.translationService = translationService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<RequestT> GetAsync(int requestId, bool includeService = false, bool includeAddress = false)
        {
            var list = await GetList(requestId: requestId, includeRequestService: includeService, includeAddress: includeAddress);
            if (list.HasItem())
            {
                return list.FirstOrDefault();
            }
            return null;
        }

        public Task<RequestT> GetDetails(int requestId)
        {
            return requestRepository.Where(r => r.RequestId == requestId)
                .Include("RequestServicesT")
                .Include("RequestStagesT").FirstOrDefaultAsync();
        }

        public Task<RequestT> GetInfo(int requestId)
        {
            return requestRepository.Where(r => r.RequestId == requestId)
                .Include(d => d.BillNumberT)
                .Include(d => d.ClientSubscription)
                .Include(d => d.Department)
                .Include(d => d.Employee)
                .Include(d => d.EmployeeReviewT)
                .Include(d => d.FawryChargeRequestT)
                .Include(d => d.FollowUpT)
                .Include(d => d.PartinerPaymentRequestT)
                .Include(d => d.PaymentT)
                .Include(d => d.Promocode)
                .Include(d => d.RejectRequestT)
                .Include(d => d.RequestCanceledT)
                .Include(d => d.RequestDelayedT)
                .Include(d => d.RequestDiscountT)
                .Include(d => d.RequestStagesT)
                .Include(d => d.RequestedAddress)
                .Include(d => d.RequestedPhone)
                .Include(d => d.Subscription)
                .Include(d => d.RequestStatusNavigation)
                .Include(d => d.SystemUser)
                .Include(d => d.Client)
                .Include(d => d.Branch)
                .Include(d => d.RequestServicesT).ThenInclude(d => d.Service)
                .FirstOrDefaultAsync();
        }

        public Task<int> GetCanceledOrdersCountByEmployee(string employeeId, DateTime time)
        {
            return requestRepository
              .Where(o => o.EmployeeId == employeeId
              && o.RequestTimestamp.Value.Year == time.Year
              && o.RequestTimestamp.Value.Month == time.Month
              && o.RequestTimestamp.Value.Day == time.Day
              && o.RequestCanceledT.Count > 0).CountAsync();
        }

        public Task<int> GetComlpeteOrdersCountByEmployee(string employeeId, DateTime time)
        {
            return requestRepository
              .Where(o => o.EmployeeId == employeeId
              && o.RequestTimestamp.Value.Year == time.Year
              && o.RequestTimestamp.Value.Month == time.Month
              && o.RequestTimestamp.Value.Day == time.Day
              && CompleteStatus.Contains(o.RequestStatus)
              && o.RequestCanceledT.Count == 0).CountAsync();
        }

        public Task<List<RequestT>> GetEmployeeOrdersList(string employeeId, DateTime time)
        {
            return requestRepository
               .Where(o => o.EmployeeId == employeeId
               && o.RequestTimestamp.Value.Year == time.Year
               && o.RequestTimestamp.Value.Month == time.Month
               && o.RequestTimestamp.Value.Day == time.Day).ToListAsync();
        }

        public Task<List<RequestT>> GetEmployeeOrdersExceptCanceledList(string employeeId, DateTime time)
        {
            return requestRepository
               .Where(o => o.EmployeeId == employeeId
               && o.RequestTimestamp.Value.Year == time.Year
               && o.RequestTimestamp.Value.Month == time.Month
               && o.RequestTimestamp.Value.Day == time.Day
               && o.RequestCanceledT.Count == 0).ToListAsync();
        }

        public Task<List<RequestT>> GetEmployeeOrdersExceptCanceledList(DateTime day)
        {
            return requestRepository
               .Where(o => o.RequestTimestamp.Value.Year == day.Year
               && o.RequestTimestamp.Value.Month == day.Month
               && o.RequestTimestamp.Value.Day == day.Day
               && o.RequestCanceledT.Count == 0
               ).ToListAsync();
        }

        public Task<List<DayOrderDto>> GetDayOrdersCustom(DateTime day)
        {
            return requestRepository
               .Where(o => o.RequestTimestamp.Value.Year == day.Year
               && o.RequestTimestamp.Value.Month == day.Month
               && o.RequestTimestamp.Value.Day == day.Day
               ).Select(orderDay => new DayOrderDto
               {
                   Day = day,
                   RequestId = orderDay.RequestId,
                   ClientId = orderDay.ClientId,
                   EmployeeId = orderDay.EmployeeId,
                   RequestStatus = orderDay.RequestStatus,
                   ClientName = orderDay.Client.ClientName,
                   EmployeeName = orderDay.Employee.EmployeeName,
                   IsCanceled = orderDay.RequestCanceledT.Count != 0,
                   IsCleaningSubscriber = orderDay.Client.Cleaningsubscribers != null
               }
               ).ToListAsync();
        }

        public Task<List<OrderDto>> GetEmployeeOrdersCustomList(string employeeId, DateTime day)
        {
            var data = requestRepository
               .Where(o => o.EmployeeId == employeeId
               && o.RequestTimestamp.Value.Year == day.Year
               && o.RequestTimestamp.Value.Month == day.Month
               && o.RequestTimestamp.Value.Day == day.Day)
               .Select(d => new OrderDto
               {
                   OrderId = d.RequestId,
                   ClientId = d.ClientId,
                   ClientName = d.Client.ClientName,
                   ClientPhone = d.Client.CurrentPhone,
                   BranchName = d.Branch.BranchName,
                   OrderTime = d.RequestTimestamp,
                   OrderStatus = d.RequestStatus,
                   IsCanceled = d.RequestCanceledT.Count != 0,
                   OrderCost = d.RequestStagesT.Cost.Value
               })
               .ToListAsync();
            return data;
        }

        public Task<int> GetAllOrdersCountByEmployee(string employeeId, DateTime time)
        {
            return requestRepository
                .Where(o => o.EmployeeId == employeeId
                && o.RequestTimestamp.Value.Year == time.Year
                && o.RequestTimestamp.Value.Month == time.Month
                && o.RequestTimestamp.Value.Day == time.Day).CountAsync();
        }

        public Task<int> GetWaitingOrdersCountByEmployee(string employeeId, DateTime time)
        {
            return requestRepository
                 .Where(o => o.EmployeeId == employeeId
                 && o.RequestTimestamp.Value.Year == time.Year
                 && o.RequestTimestamp.Value.Month == time.Month
                 && o.RequestTimestamp.Value.Day == time.Day
                 && WaitingStatus.Contains(o.RequestStatus)).CountAsync();
        }

        public Task<int> GetOrdersExceptCanceledCountByEmployee(string employeeId, DateTime time)
        {
            return requestRepository
               .Where(o => o.EmployeeId == employeeId
               && o.RequestTimestamp.Value.Year == time.Year
               && o.RequestTimestamp.Value.Month == time.Month
               && o.RequestTimestamp.Value.Day == time.Day
               && o.RequestCanceledT.Count == 0).CountAsync();
        }

        public Task<List<RequestT>> GetList(DateTime? startDate = null, DateTime? endDate = null, int? requestId = null, int? siteId = null,
        int? subscriptionId = null, int? clientSubscriptionId = null, int? clientId = null, string employeeId = null, int? systemUserId = null, int? requestStatus = null, int? requestStatusGroupId = null,
        bool? getCanceled = null, int? branchId = null, bool? isPaid = null, int? promocode = null, int? departmentId = null,
        bool? isCompleted = null, bool? isReviewed = null, bool? isFollowUp = null,
        bool includeRequestStage = false, bool includeClient = false, bool includeEmployee = false, bool includeStatus = false,
        bool includeRequestService = false, bool includeService = false, bool includeDiscounts = false, bool includeCancelT = false,
        bool includeDelayedT = false, bool includeFollowUpT = false, bool includeReviewT = false, bool includeSubscription = false,
        bool includePayment = false, bool includeComplaiment = false, bool includeSite = false, bool includeBill = false,
        bool includeFawryCharge = false, bool includeAddress = false, bool includePhone = false, bool includePromocode = false,
        bool includeDepartment = false, bool includeBranch = false, bool includeSystemUser = false, bool includeEmployeeLogin = false)
        {
            var query = requestRepository.DbSet.AsQueryable();
            if (startDate.HasValue)
            {
                query = query.Where(d => d.RequestTimestamp >= startDate);
            }
            if (endDate.HasValue)
            {
                query = query.Where(d => d.RequestTimestamp <= endDate);
            }
            if (requestId.HasValue)
            {
                query = query.Where(d => d.RequestId == requestId);
            }
            if (clientId.HasValue)
            {
                query = query.Where(d => d.ClientId == clientId);
            }
            if (systemUserId.HasValue)
            {
                query = query.Where(d => d.SystemUserId == systemUserId);
            }
            if (requestStatus.HasValue)
            {
                query = query.Where(d => d.RequestStatus == requestStatus);
            }
            if (requestStatusGroupId.HasValue)
            {
                query = query.Where(d => d.RequestStatusNavigation.RequestStatusGroupId == requestStatusGroupId);
            }
            if (getCanceled.HasValue)
            {
                query = query.Where(d => d.RequestCanceledT.Any() == getCanceled);
            }
            if (!string.IsNullOrEmpty(employeeId))
            {
                query = query.Where(d => d.EmployeeId == employeeId);
            }
            if (branchId.HasValue)
            {
                query = query.Where(d => d.BranchId == branchId.Value);
            }
            if (subscriptionId.HasValue)
            {
                query = query.Where(d => d.SubscriptionId == subscriptionId.Value);
            }
            if (clientSubscriptionId.HasValue)
            {
                query = query.Where(d => d.ClientSubscriptionId == clientSubscriptionId.Value);
            }
            if (siteId.HasValue)
            {
                query = query.Where(d => d.SiteId == siteId.Value);
            }
            if (isPaid.HasValue)
            {
                query = query.Where(d => d.RequestStagesT.PaymentFlag == isPaid.Value);
            }
            if (promocode.HasValue)
            {
                query = query.Where(d => d.PromocodeId == promocode.Value);
            }
            if (departmentId.HasValue)
            {
                query = query.Where(d => d.DepartmentId == departmentId);
            }
            if (isCompleted.HasValue)
            {
                query = query.Where(d => d.IsCompleted == isCompleted);
            }
            if (isFollowUp.HasValue)
            {
                query = query.Where(d => d.IsFollowed == isFollowUp);
            }
            if (isReviewed.HasValue)
            {
                query = query.Where(d => d.IsReviewed == isReviewed);
            }
            if (includeRequestService)
            {
                if (includeService)
                {
                    query = query.Include(d => d.RequestServicesT).ThenInclude(d => d.Service);
                }
                else
                {
                    query = query.Include(d => d.RequestServicesT);
                }
            }
            if (includeClient)
            {
                query = query.Include(d => d.Client);
            }
            if (includeEmployee)
            {
                if (includeEmployeeLogin)
                {
                    query = query.Include(d => d.Employee)
                        .ThenInclude(d => d.LoginT);
                }
                else
                {
                    query = query.Include(d => d.Employee);
                }
            }
            if (includeStatus)
            {
                query = query.Include(d => d.RequestStatusNavigation);
            }
            if (includeCancelT)
            {
                query = query.Include(d => d.RequestCanceledT);
            }
            if (includeDelayedT)
            {
                query = query.Include(d => d.RequestDelayedT);
            }
            if (includeComplaiment)
            {
                query = query.Include(d => d.RequestComplaintT);
            }
            if (includeDiscounts)
            {
                query = query.Include(d => d.RequestDiscountT);
            }
            if (includeFollowUpT)
            {
                query = query.Include(d => d.FollowUpT);
            }
            if (includePayment)
            {
                query = query.Include(d => d.PaymentT);
            }
            if (includeSite)
            {
                query = query.Include(d => d.Site);
            }
            if (includeSubscription)
            {
                query = query.Include(d => d.Subscription);
            }
            if (includeReviewT)
            {
                query = query.Include(d => d.EmployeeReviewT);
            }
            if (includeRequestStage)
            {
                query = query.Include(d => d.RequestStagesT);
            }
            if (includeBill)
            {
                query = query.Include(d => d.BillNumberT);
            }
            if (includeFawryCharge)
            {
                query = query.Include(d => d.FawryChargeRequestT);
            }
            if (includeAddress)
            {
                query = query.Include(d => d.RequestedAddress);
            }
            if (includePhone)
            {
                query = query.Include(d => d.RequestedPhone);
            }
            if (includePromocode)
            {
                query = query.Include(d => d.Promocode);
            }
            if (includeDepartment)
            {
                query = query.Include(d => d.Department);
            }
            if (includeBranch)
            {
                query = query.Include(d => d.Branch);
            }
            if (includeSystemUser)
            {
                query = query.Include(d => d.SystemUser);
            }
            return query.ToListAsync();
        }

        public async Task<int> AddAsync(RequestT request)
        {
            await requestRepository.AddAsync(request);
            return await requestRepository.SaveAsync();
        }

        public Task<List<RequestT>> GetUnPaidAsync(string employeeId, bool ignoreRequestWithValidFawryRequest = false)
        {
            var query = requestRepository
                .Where(d => d.EmployeeId == employeeId && d.RequestStagesT.PaymentFlag == false);
            if (ignoreRequestWithValidFawryRequest)
            {
                query = query
                    .Where(d => d.FawryChargeRequestT.Any(t => t.Charge.ChargeStatus == App.Global.Enums.FawryRequestStatus.UNPAID.ToString()));
            }
            query = query.Include(d => d.RequestStagesT);
            return query.ToListAsync();
        }

         
        public async Task<Result<RequestT>> AddAsync(int clientId, bool isViaApp, int addressId, int phoneId, string employeeId,
            int? siteId, int? clientSubscriptionId, DateTime requestTime, int systemUserId)
        {
            CartT cart;
            int? cartId = null;
            int? subscriptionId = null;
            if(requestTime < DateTime.Now.EgyptTimeNow())
            {
                return ResultFactory<RequestT>.CreateErrorResponseMessageFD("You can't place a request in the past, time " + requestTime.ToString(), App.Global.Enums.ResultStatusCode.Failed);
            }
            if (string.IsNullOrEmpty(employeeId) == false)
            {
                var isEmployeeFree = await employeeRequestService.IsThisEmployeeFree(employeeId, requestTime);
                if (isEmployeeFree == false)
                {
                    return ResultFactory<RequestT>.CreateErrorResponseMessageFD("This employee have a request in this time please select another", App.Global.Enums.ResultStatusCode.Failed);
                }
            }
            var address = await addressRepository.GetAsync(addressId);
            if (address.IsNull())
            {
                return ResultFactory<RequestT>.CreateErrorResponseMessage("Address not found", App.Global.Enums.ResultStatusCode.NotFound);
            }
            var branch = await cityService.GetCityBranchAsync(address.CityId.Value);
            if (branch.IsNull())
            {
                return ResultFactory<RequestT>.CreateErrorResponseMessage("Branch not found", App.Global.Enums.ResultStatusCode.NotFound);
            }
            unitOfWork.StartTransaction();
            if (clientSubscriptionId.HasValue)
            {
                var clientSubscription = await clientSubscriptionRepository.Where(d => d.ClientSubscriptionId == clientSubscriptionId.Value)
                    .Include(d => d.Subscription)
                    .Include(d => d.SubscriptionService)
                    .FirstOrDefaultAsync();
                if (clientSubscription.IsNull())
                {
                    return ResultFactory<RequestT>.CreateErrorResponseMessage("Client subscription not found", App.Global.Enums.ResultStatusCode.NotFound);
                }
                if (clientSubscription.IsCanceled)
                {
                    return ResultFactory<RequestT>.CreateErrorResponseMessage("Client subscription is canceled", App.Global.Enums.ResultStatusCode.Failed);
                }
                if (clientSubscription.ExpireDate.HasValue && DateTime.Now.EgyptTimeNow() > clientSubscription.ExpireDate.Value)
                {
                    return ResultFactory<RequestT>.CreateErrorResponseMessage("Client subscription is expired", App.Global.Enums.ResultStatusCode.Failed);
                }
                if (requestTime < clientSubscription.CreationTime.Value)
                {
                    return ResultFactory<RequestT>.CreateErrorResponseMessage($"Please select a date after subscription date {clientSubscription.CreationTime.Value.ToShortDateString()}", App.Global.Enums.ResultStatusCode.Failed);
                }
                if (clientSubscription.ExpireDate.HasValue && requestTime > clientSubscription.ExpireDate.Value )
                {
                    return ResultFactory<RequestT>.CreateErrorResponseMessage($"Please select a date before the subscription end date {clientSubscription.ExpireDate.Value.ToShortDateString()}", App.Global.Enums.ResultStatusCode.Failed);
                }
                if (clientSubscription.Subscription.IsNull())
                {
                    return ResultFactory<RequestT>.CreateErrorResponseMessage("Subscription error found", App.Global.Enums.ResultStatusCode.NotFound);
                }
                var subService = clientSubscription.SubscriptionService;
                if (subService.IsNull())
                {
                    return ResultFactory<RequestT>.CreateErrorResponseMessage("Subscription service not found", App.Global.Enums.ResultStatusCode.NotFound);
                }
                if (clientSubscription.ExpireDate.HasValue)
                {
                    var isExceed = await subscriptionRequestService.IsExceedContractSubscriptionLimitAsync(clientSubscriptionId.Value, requestTime);
                    if (isExceed)
                    {
                        return ResultFactory<RequestT>.CreateErrorResponseMessage("Your have been reach the request count limit for this subscription", App.Global.Enums.ResultStatusCode.Failed);
                    }
                }
                else
                {
                    var isExceed = await subscriptionRequestService.IsExceedSubscriptionLimitAsync(clientSubscriptionId.Value, requestTime);
                    if (isExceed)
                    {
                        return ResultFactory<RequestT>.CreateErrorResponseMessage("Your subscription is ended for this month, id " + clientSubscriptionId, App.Global.Enums.ResultStatusCode.Failed);
                    }
                }
               
                subscriptionId = clientSubscription.SubscriptionId;
                var subscriptionCustomCart = new CartT
                {
                    ClientId = clientId,
                    DepartmentId = clientSubscription.Subscription.DepartmentId,
                    CreationTime = DateTime.Now,
                    IsViaApp = isViaApp,
                    UsePoint = false,
                    CartDetailsT = new List<CartDetailsT>
                    {
                        new CartDetailsT
                        {
                            ServiceQuantity = 1,
                            CreationTime = DateTime.Now,
                            ServiceId = subService.ServiceId,
                        }
                    }
                };
                await cartService.AddAsync(subscriptionCustomCart);
                var affectedRow = await unitOfWork.SaveAsync();
                if (affectedRow <= 0)
                {
                    return ResultFactory<RequestT>.CreateErrorResponseMessage("Can't create cart");
                }
                cart = subscriptionCustomCart;
                cartId = subscriptionCustomCart.CartId;
            }
            else
            {
                cart = await cartService.GetCurrentByClientIdAsync(clientId, isViaApp);
                if (cart.IsNull())
                {
                    return ResultFactory<RequestT>.CreateErrorResponseMessage("Cart not found", App.Global.Enums.ResultStatusCode.NotFound);
                }
                cartId = cart.CartId;
            }
            var customCart = await cartService.GetCartForAppAsync(cartId.Value, address.CityId, clientSubscriptionId, requestTime);
            if (customCart.IsNull())
            {
                return ResultFactory<RequestT>.CreateErrorResponseMessage("Error with custom Cart", App.Global.Enums.ResultStatusCode.NotFound);
            }
            var request = new RequestT
            {
                RequestedAddressId = addressId,
                RequestedPhoneId = phoneId,
                EmployeeId = employeeId,
                SystemUserId = systemUserId,
                RequestTimestamp = requestTime,
                RequestCurrentTimestamp = DateTime.Now.ToEgyptTime(),
                BranchId = branch.BranchId,
                RequestStatus = string.IsNullOrEmpty(employeeId) ? ((sbyte)Domain.Enum.RequestStatus.NotSet) : ((sbyte)Domain.Enum.RequestStatus.Waiting),
                SiteId = siteId,
                ClientSubscriptionId = clientSubscriptionId,
                SubscriptionId = subscriptionId,
                IsPaid = false,
                IsReviewed = false,
                RequestStagesT = new RequestStagesT
                {
                    PaymentFlag = false,
                    SentTimestamp = DateTime.Now
                },
            };
            request = ConvertCustomCartToRequest(customCart, request);
            request.RequestDiscountT.ToList().ForEach(d => d.SystemUserId = systemUserId);
            await AddAsync(request);
            cart.HaveRequest = true;
            await cartService.UpdateAsync(cart);
            if (customCart.UsedPoints > 0)
            {
                await clientPointService.WithdrawAsync(new ClientPointT
                {
                    ClientId = customCart.ClientId,
                    Points = customCart.UsedPoints,
                    Reason = "Request #" + request.RequestId,
                    SystemUserId = systemUserId
                });
            }
            var affectedRows = await unitOfWork.CommitAsync();
            if (affectedRows > 0)
            {
                var phone = await phoneRepository.GetAsync(phoneId);
                var msg = $"Your appointment is coming up on {request.RequestTimestamp.Value.DayOfWeek} at {request.RequestTimestamp.Value.ToString("hh:mm tt")}";
                whatsAppService.SendRequestDetails(phone.ClientPhone, msg);
                return ResultFactory<RequestT>.CreateSuccessResponse(request);
            }
            else
            {
                return ResultFactory<RequestT>.CreateErrorResponse();
            }
        }

        public async Task<List<RequestT>> GetAppList(int clientId, int? status)
        {
            bool getCanceled = false;
            var cancelStatus = GeneralSetting.RequestStatusList.FirstOrDefault(d => d.RequestStatusName.ToLower() == "Canceled".ToLower());
            if(cancelStatus.RequestStatusGroupId == status)
            {
                getCanceled = true;
            }
            var list = await GetList(requestStatusGroupId: status, clientId: clientId, getCanceled: getCanceled, includeStatus: true,
                includeRequestService: true, includeService: true, includeDepartment: true);
            list = list.OrderByDescending(d => d.RequestTimestamp).ToList();
            return list;
        }

        public Task<List<RequestT>> GetAppDetails(int requestId)
        {
            return GetList(requestId: requestId, includeStatus: true,
                includeRequestService: true, includeService: true, includeEmployee: true, includeAddress: true, includePhone: true, includeDepartment: true);
        }

        public async Task<Result<int>> CancelAsync(int requestId, string reason, int systemUserId, bool resetSubscriptionMonthRequest = true)
        {
            try
            {
                unitOfWork.StartTransaction();
                var request = await GetAsync(requestId);
                if (request.IsNull())
                {
                    return ResultFactory<int>.CreateNotFoundResponse("Request not found " + requestId); 
                }
                if (request.IsCanceled)
                {
                    return ResultFactory<int>.CreateErrorResponseMessage("Request is already canceled " + requestId);
                }
                if (request.IsCompleted)
                {
                    return ResultFactory<int>.CreateErrorResponseMessage("This request is completed " + requestId);
                }
                await cancelRequestRepository.AddAsync(new RequestCanceledT
                {
                    CancelRequestReason = reason,
                    CancelRequestTimestamp = DateTime.Now,
                    RequestId = requestId,
                    SystemUserId = systemUserId
                });
                request.IsCanceled = true;
                request.RequestStatus = GeneralSetting.RequestStatusList
                    .FirstOrDefault(d => d.RequestStatusName == "Canceled").RequestStatusId;
                await UpdateAsync(request);
                await clientPointService.AddAsync(new ClientPointT
                {
                    ClientId = request.ClientId,
                    Points = request.UsedPoints,
                    Reason = translationService.Tranlate("Cancel Request") + $" #{requestId}",
                    SystemUserId = systemUserId,
                });
                if (request.ClientSubscriptionId.HasValue && resetSubscriptionMonthRequest)
                {
                    await unitOfWork.SaveAsync();
                    var result = await ResetSubscriptionMonthRequestAsync(request.ClientSubscriptionId.Value, request.RequestTimestamp.Value);
                    if (result.IsFail)
                    {
                        return result;
                    }
                }
                var affectedRows = await unitOfWork.CommitAsync();
                return ResultFactory<int>.CreateAffectedRowsResult(affectedRows);
            }
            finally
            {
                unitOfWork.Dispose();
            }
        }

        public async Task<Result<int>> ResetSubscriptionMonthRequestAsync(int clientSubscriptionId, DateTime dateTime)
        {
            int affectedRows = 0;
            try
            {
                unitOfWork.StartTransaction();
                var requestList = await GetList(clientSubscriptionId: clientSubscriptionId, getCanceled: false, isCompleted: false,
                    startDate: App.Global.DateTimeHelper.DateTimeHelperService.GetStartDateOfMonthS(dateTime),
                    endDate: App.Global.DateTimeHelper.DateTimeHelperService.GetEndDateOfMonthS(dateTime),
                    includeRequestService: true, includeRequestStage: true, includeDiscounts: true);
                requestList = requestList.OrderBy(d => d.RequestTimestamp.Value).ToList();
                if (requestList.IsEmpty())
                {
                    return ResultFactory<int>.CreateSuccessResponse(0);
                }
                foreach (var request in requestList)
                {
                    request.IsCanceled = true;
                }
                affectedRows = await unitOfWork.SaveAsync();
                if (affectedRows <= 0)
                {
                    return ResultFactory<int>.CreateAffectedRowsResult(affectedRows);
                }
                foreach (var request in requestList)
                {
                    request.IsCanceled = false;
                    await UpdateWithCartAsync(request);
                    affectedRows = await unitOfWork.SaveAsync();
                    if (affectedRows <= 0)
                    {
                        return ResultFactory<int>.CreateAffectedRowsResult(affectedRows);
                    }

                }
                affectedRows = await unitOfWork.CommitAsync();
                return ResultFactory<int>.CreateAffectedRowsResult(affectedRows);
            }
            finally
            {
                unitOfWork.Dispose();
            }
          
        }

        public async Task<int> UpdateAsync(RequestT request)
        {
            requestRepository.Update(request.RequestId, request);
            return await requestRepository.SaveAsync();
        }

        public async Task<int> UpdateWithCartAsync(RequestT request)
        {
            AddressT address;
            if (request.RequestedAddress == null)
            {
                address = await addressRepository.GetAsync(request.RequestedAddressId.Value);
            }
            else
            {
                address = request.RequestedAddress;
            }
            if (request.CartId.HasValue)
            {
                var customCart = await cartService.GetCartForAppAsync(request.CartId.Value, address.CityId, request.ClientSubscriptionId, request.RequestTimestamp, request);
                request = ConvertCustomCartToRequest(customCart, request);
            }
            requestRepository.Update(request.RequestId, request);
            return await requestRepository.SaveAsync();
        }

        public async Task<Result<RequestDelayedT>> ChangeTimeAsync(int requestId, DateTime newTime, string reason, int systemUserId)
        {
            unitOfWork.StartTransaction();
            var request = await GetAsync(requestId);
            if (request.IsCompleted)
            {
                return ResultFactory<RequestDelayedT>.CreateErrorResponseMessageFD("This request is already complete");
            }
            if (request.IsCanceled)
            {
                return ResultFactory<RequestDelayedT>.CreateErrorResponseMessageFD("This request is canceled");
            }
            if (string.IsNullOrEmpty(request.EmployeeId) == false)
            {
                var isFree = await employeeRequestService.IsThisEmployeeFree(request.EmployeeId, newTime);
                if (isFree == false)
                {
                    return ResultFactory<RequestDelayedT>.CreateErrorResponseMessage("This employee not avalible at this time");
                }
            }
            var delayRequest = new RequestDelayedT
            {
                DelayRequestTimestamp = request.RequestTimestamp.Value,
                DelayRequestNewTimestamp = newTime,
                DelayRequestReason = reason,
                RequestId = requestId,
                SystemUserId = systemUserId
            };
            await delayRequestRepository.AddAsync(delayRequest);
            request.RequestStatus = GeneralSetting.RequestStatusList
                .FirstOrDefault(d => d.RequestStatusName == "Delayed").RequestStatusId;
            request.RequestTimestamp = newTime;
            await UpdateAsync(request);
            int affectedRows = await unitOfWork.CommitAsync();
            return ResultFactory<RequestDelayedT>.CreateAffectedRowsResult(affectedRows, data: delayRequest);
        }

        public async Task<Result<EmployeeT>> ReAssignEmployeeAsync(int requestId, string employeeId)
        {
            EmployeeT employee = null;
            var request = await GetAsync(requestId);
            if (request.IsCompleted)
            {
                return ResultFactory<EmployeeT>.CreateErrorResponseMessageFD("This request is already complete");
            }
            if (request.IsCanceled)
            {
                return ResultFactory<EmployeeT>.CreateErrorResponseMessageFD("This request is canceled");
            }
            if (string.IsNullOrEmpty(employeeId))
            {
                request.EmployeeId = null;
                request.RequestStatus = GeneralSetting.RequestStatusList
               .FirstOrDefault(d => d.RequestStatusName == "NotSet").RequestStatusId;
            }
            else
            {
                request.EmployeeId = employeeId;
                var isFree = await employeeRequestService.IsThisEmployeeFree(employeeId, request.RequestTimestamp.Value);
                if(isFree == false)
                {
                    return ResultFactory<EmployeeT>.CreateErrorResponseMessage("This employee is not available at this time");
                }
                employee = await employeeRepository.GetAsync(employeeId);
            }
            request.IsReviewed = false;
            var affectedRows = await UpdateAsync(request);
            return ResultFactory<EmployeeT>.CreateAffectedRowsResult(affectedRows, data: employee);
        }

        public async Task<int> AddComplaintAsync(RequestComplaintT requestComplaint)
        {
            var request = await GetAsync(requestComplaint.RequestId);
            if(request.IsCompleted == false)
            {
                return 0;
            }
            requestComplaint.ComplaintTimestamp = DateTime.Now.EgyptTimeNow();
            await complaintRequestRepository.AddAsync(requestComplaint);
            return await complaintRequestRepository.SaveAsync();
        }

        private RequestT ConvertCustomCartToRequest(CartDto customCart, RequestT request)
        {
            decimal netPrice = customCart.NetPrice - customCart.MaterialCost - customCart.DeliveryPrice;
            request.ClientId = customCart.ClientId;
            request.DepartmentId = customCart.DepartmentId;
            request.RequestNote = customCart.Note;
            request.CustomerPrice = customCart.NetPrice;
            request.MaterialCost = customCart.MaterialCost;
            request.RequestPoints = customCart.GainPoints;
            request.UsedPoints = customCart.UsedPoints;
            request.TotalPrice = customCart.TotalPrice;
            request.TotalDiscount = customCart.ApplliedDiscount;
            request.NetPrice = netPrice;
            request.CompanyPercentageAmount = customCart.CompanyePercentageAmount;
            request.EmployeePercentageAmount = customCart.EmployeePercentageAmount;
            request.PromocodeId = customCart.PromoCodeId;
            request.DeliveryPrice = customCart.DeliveryPrice;
            request.CartId = customCart.CartId;
            if (request.RequestStagesT.IsNull())
            {
                request.RequestStagesT = new RequestStagesT();
            }
            request.RequestStagesT.Cost = netPrice;
            ConvertCartDiscountToRequestDiscount(customCart, request);
            ConvertCartServiceToRequestService(customCart, request);
            return request;
        }

        private void ConvertCartServiceToRequestService(CartDto cart, RequestT request)
        {
            if (request.RequestServicesT.IsNull())
            {
                request.RequestServicesT = new List<RequestServicesT>();
            }
            foreach (var item in cart.CartServiceDetails)
            {
                var requestService = request.RequestServicesT.FirstOrDefault(d => d.ServiceId == item.ServiceId);
                if(requestService != null)
                {
                    requestService.ServicePrice = item.Price;
                    requestService.ServiceDiscount = item.Discount;
                    requestService.RequestServicesQuantity = item.ServiceQuantity.Value;
                    requestService.ServiceMaterialCost = item.MaterialCost;
                    requestService.ServiceId = item.ServiceId;
                    requestService.ServicePoint = item.Points;
                }
                else
                {
                    request.RequestServicesT.Add(new RequestServicesT
                    {
                        AddTimestamp = DateTime.Now,
                        ServicePrice = item.Price,
                        ServiceDiscount = item.Discount,
                        RequestServicesQuantity = item.ServiceQuantity.Value,
                        ServiceMaterialCost = item.MaterialCost,
                        ServiceId = item.ServiceId,
                        ServicePoint = item.Points,
                    });
                }
            }
        }

        private void ConvertCartDiscountToRequestDiscount(CartDto cart, RequestT request)
        {
            if (request.RequestDiscountT.IsNull())
            {
                request.RequestDiscountT = new List<RequestDiscountT>();
            }
            foreach (var item in cart.RequestDiscounts)
            {
                var discount = request.RequestDiscountT.FirstOrDefault(d => d.DiscountTypeId == item.DiscountTypeId);
                if (discount != null)
                {
                    discount.DiscountValue = item.DiscountValue;
                }
                else
                {
                    request.RequestDiscountT.Add(new RequestDiscountT
                    {
                        CreationTime = DateTime.Now,
                        RequestId = item.RequestId,
                        CompanyPercentage = item.CompanyPercentage,
                        DiscountValue = item.DiscountValue,
                        DiscountTypeId = item.DiscountTypeId,
                        Description = item.Description,
                        SystemUserId = request.SystemUserId,
                    });
                }
            }
        }

        public async Task<Result<RequestServicesT>> AddUpdateServiceAsync(RequestServicesT requestService)
        {
            unitOfWork.StartTransaction();
            var requestServiceList = await GetServiceListAsync(requestService.RequestId);
            if (requestServiceList.Count <= 1 && requestService.RequestServicesQuantity <= 0)
            {
                return ResultFactory<RequestServicesT>.CreateErrorResponseMessageFD("Can't delete this service, the request must have at least one service");
            }
            var cartId = await GetCartIdAsync(requestService.RequestId);
            if (cartId.IsNull())
            {
                return ResultFactory<RequestServicesT>.CreateErrorResponseMessageFD("This request has no cart please contact with us");
            }
            var cartResult = await cartService.AddUpdateServiceAsync(cartId.Value, requestService.ServiceId, requestService.RequestServicesQuantity);
            if (cartResult.IsFail)
            {
                return cartResult.Convert(requestService);
            }
            requestService.AddTimestamp = DateTime.Now;
            var requestServiceFound = requestServiceList.FirstOrDefault(d => d.ServiceId == requestService.ServiceId);
            if (requestServiceFound.IsNotNull())
            {
                if (requestService.RequestServicesQuantity <= 0)
                {
                    await requestServiceRepository.DeleteAsync(requestServiceFound.RequestServiceId);
                }
                else
                {
                    requestServiceFound.RequestServicesQuantity = requestService.RequestServicesQuantity;
                    requestServiceRepository.Update(requestServiceFound.RequestServiceId, requestServiceFound);
                }
            }
            else
            {
                await requestServiceRepository.AddAsync(requestService);
            }
            await unitOfWork.SaveAsync();
            var requestList = await GetList(requestId: requestService.RequestId, includeRequestService: true, includeRequestStage: true, includeDiscounts: true);
            var request = requestList.FirstOrDefault();
            if (request.IsCanceled)
            {
                return ResultFactory<RequestServicesT>.CreateErrorResponseMessage("This request is canceled");
            }
            if (request.IsCompleted)
            {
                return ResultFactory<RequestServicesT>.CreateErrorResponseMessage("This request is complete");
            }
            await UpdateWithCartAsync(request);
            var affectedRows = await unitOfWork.CommitAsync();
            return ResultFactory<RequestServicesT>.CreateAffectedRowsResult(affectedRows);
        }

        public Task<RequestServicesT> GetServiceAsync(int requestServiceId)
        {
            return requestServiceRepository.GetAsync(requestServiceId);
        }

        public Task<int?> GetCartIdAsync(int requestId)
        {
            return requestRepository.Where(d => d.RequestId == requestId)
                .Select(d => d.CartId).FirstOrDefaultAsync();
        }

        public Task<List<RequestServicesT>> GetServiceListAsync(int requestId)
        {
            return requestServiceRepository.Where(d => d.RequestId == requestId)
                .ToListAsync();
        }

        public async Task<Result<RequestT>> AddAsync(int clientSubscriptionId, DateTime requestTime, int systemUserId, bool isViaApp)
        {
            var clientSubscription = await clientSubscriptionRepository.GetAsync(clientSubscriptionId);
            if (clientSubscription.IsNull())
            {
                return ResultFactory<RequestT>.CreateErrorResponseMessage("Client subscription not found");
            }
            if (clientSubscription.IsCanceled)
            {
                return ResultFactory<RequestT>.CreateErrorResponseMessage("Client subscription is canceled");
            }
            var time = new DateTime(requestTime.Year, requestTime.Month, requestTime.Day, clientSubscription.VisitTime.Value.Hour, 0, 0);
            return await AddAsync(clientSubscription.ClientId, isViaApp, clientSubscription.AddressId.Value, clientSubscription.PhoneId.Value, clientSubscription.EmployeeId,
                null, clientSubscriptionId, time, systemUserId);
        }
    }
}
