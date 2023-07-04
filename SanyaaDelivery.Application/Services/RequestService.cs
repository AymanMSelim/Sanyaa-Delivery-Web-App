using App.Global.DateTimeHelper;
using App.Global.DTOs;
using App.Global.ExtensionMethods;
using App.Global.Translation;
using App.Global.WhatsApp;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Enum;
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
        private readonly INotificatonService notificatonService;
        private readonly IRepository<ServiceT> serviceRepository;
        private readonly IRepository<AttachmentT> attachmentRepository;
        private readonly IOperationService operationService;
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
        private readonly IRepository<MessagesT> messageRepository;
        private readonly IHelperService helperService;
        private readonly IClientPointService clientPointService;
        private readonly IEmployeeRequestService employeeRequestService;
        private readonly Translator translationService;
        private readonly IUnitOfWork unitOfWork;

        public RequestService(IRepository<RequestT> requestRepository, IRepository<RequestCanceledT> cancelRequestRepository,
            IRepository<RequestDelayedT> delayRequestRepository, IRepository<RequestComplaintT> complaintRequestRepository,
            IRepository<RequestServicesT> requestServiceRepository, IRepository<AddressT> addressRepository, INotificatonService notificatonService,
            IRepository<ServiceT> serviceRepository, IRepository<AttachmentT> attachmentRepository, IOperationService operationService,
            ICartService cartService, IRepository<ClientPhonesT> phoneRepository, IRepository<EmployeeT> employeeRepository,
            IRepository<ClientSubscriptionT> clientSubscriptionRepository, ISubscriptionRequestService subscriptionRequestService,
            ICityService cityService, WhatsAppService whatsAppService, IRepository<MessagesT> messageRepository, IHelperService helperService,
            IClientPointService clientPointService, IEmployeeRequestService employeeRequestService, Translator translationService, IUnitOfWork unitOfWork)
        {
            this.requestRepository = requestRepository;
            this.cancelRequestRepository = cancelRequestRepository;
            this.delayRequestRepository = delayRequestRepository;
            this.complaintRequestRepository = complaintRequestRepository;
            //this.clientService = clientService;
            this.requestServiceRepository = requestServiceRepository;
            this.addressRepository = addressRepository;
            this.notificatonService = notificatonService;
            this.serviceRepository = serviceRepository;
            this.attachmentRepository = attachmentRepository;
            this.operationService = operationService;
            this.cartService = cartService;
            this.phoneRepository = phoneRepository;
            this.employeeRepository = employeeRepository;
            this.clientSubscriptionRepository = clientSubscriptionRepository;
            //this.clientSubscriptionService = clientSubscriptionService;
            this.subscriptionRequestService = subscriptionRequestService;
            this.cityService = cityService;
            this.whatsAppService = whatsAppService;
            this.messageRepository = messageRepository;
            this.helperService = helperService;
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
                .Include(d => d.FawryChargeRequestT).ThenInclude(d => d.Charge)
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
                .Include(d => d.BroadcastRequestT)
                .Include(d => d.RequestServicesT).ThenInclude(d => d.Service)
                .AsNoTracking().FirstOrDefaultAsync();
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
                query = query.Where(d => d.IsCanceled);
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


       public Task<List<RequestDto>> GetCustomListAsync(DateTime? startDate = null, DateTime? endDate = null, int? requestId = null, int? siteId = null,
       int? subscriptionId = null, int? clientSubscriptionId = null, int? clientId = null, string employeeId = null, int? systemUserId = null, int? requestStatus = null, int? requestStatusGroupId = null,
       bool? isCanceled = null, int? branchId = null, bool? isPaid = null, int? promocode = null, int? departmentId = null,
       bool? isCompleted = null, bool? isReviewed = null, bool? isFollowUp = null)
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
            if (isCanceled.HasValue)
            {
                query = query.Where(d => d.IsCanceled == isCanceled);
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
            return query.Select(d => new RequestDto
            {
                BranchId = d.BranchId,
                BranchName = d.Branch.BranchName,
                CityId = d.RequestedAddress.CityId,
                ClientAddress = d.RequestedAddress.City.CityName + ", " + d.RequestedAddress.Region.RegionName,
                ClientId = d.ClientId,
                ClientName = d.Client.ClientName,
                ClientPhone = d.RequestedPhone.ClientPhone,
                CompanyPercentageAmount = d.CompanyPercentageAmount,
                CustomerPrice = d.CustomerPrice,
                DeparmentName = d.Department.DepartmentName,
                DepartmentId = d.DepartmentId.Value,
                EmployeeId = d.EmployeeId,
                EmployeeName = d.EmployeeId == null ? "" : d.Employee.EmployeeName,
                IsCanceled = d.IsCanceled,
                NetPrice = d.NetPrice,
                RequestId = d.RequestId,
                RequestNote = d.RequestNote,
                RequestStatus = d.RequestStatus,
                RequestStatusName = translationService.Tranlate(d.RequestStatusNavigation.RequestStatusName),
                RequestTimestamp = d.RequestTimestamp.Value,
                SubscriptionId = d.SubscriptionId,
                SubscriptionName = d.SubscriptionId.HasValue ? d.Subscription.SubscriptionName : "",
                SystemUserId = d.SystemUserId,
                SystemUserName = d.SystemUser.SystemUserUsername,
                ServicesNames = string.Join(", ", d.RequestServicesT.Select(s => s.Service.ServiceName)),
                IsCompleted = d.IsCompleted,
                PhoneId = d.RequestedPhoneId,
                AddressId = d.RequestedAddressId,
                ClientSubscriptionId = d.ClientSubscriptionId
            }).ToListAsync();
        }

        public async Task<int> AddAsync(RequestT request)
        {
            await requestRepository.AddAsync(request);
            return await requestRepository.SaveAsync();
        }

        public Task<List<RequestT>> GetUnPaidAsync(string employeeId, bool ignoreRequestWithValidFawryRequest = false)
        {
            var query = requestRepository
                .Where(d => d.EmployeeId == employeeId && d.IsPaid == false && d.IsCompleted == true && d.IsCanceled == false);
            if (ignoreRequestWithValidFawryRequest)
            {
                query = query.Where(d => d.FawryChargeRequestT
                .Count(t => t.Charge.ChargeStatus == App.Global.Enums.FawryRequestStatus.NEW.ToString() 
                || t.Charge.ChargeStatus == App.Global.Enums.FawryRequestStatus.UNPAID.ToString()) == 0);
            }
            return query.ToListAsync();
        }

        private async Task<Result<T>> SelectTechnician<T>(int requestId, bool isViaApp)
        {
            var request = await requestRepository.GetAsync(requestId);
            //return await SelectTechnician<T>(request, isViaApp);
            return null;
        }

        private async Task<Result<T>> SelectTechnician<T>(RequestT request, string employeeId, bool isViaApp, TechnicianSelectionType selectionType = TechnicianSelectionType.App)
        {
            if (selectionType == TechnicianSelectionType.App)
            {
                request.EmployeeId = employeeId;
            }
            else if (selectionType == TechnicianSelectionType.BroadcastAll)
            {
                var freeEmployeeIdLis = await employeeRequestService.GetFreeEmployeeIdListAsync(request.RequestTimestamp.Value, request.DepartmentId.Value, request.BranchId);
                if (freeEmployeeIdLis.IsEmpty())
                {
                    return ResultFactory<T>.CreateErrorResponseMessage("No technician available at this time, please select another time");
                }
            }
            //else if(selectionType == TechnicianSelectionType.)
            //if(isViaApp || string.IsNullOrEmpty(model.EmployeeId))
            //{
            //    var freeEmployeeIdLis = await employeeRequestService.GetFreeEmployeeIdListAsync(model.RequestTime.Value, departmentId, branch.BranchId);
            //    if (freeEmployeeIdLis.IsEmpty() && model.SkipCheckEmployeeStatus == false)
            //    {
            //        return ResultFactory<RequestT>.CreateErrorResponseMessage("No technician available at this time, please select another time");
            //    }
            //}
            return null;
        }

        private async Task<Result<RequestT>> ValidateRequestMainData(AddRequestDto model, int departmentId, bool isViaApp)
        {
            if (model.RequestTime < DateTime.Now.EgyptTimeNow())
            {
                return ResultFactory<RequestT>.CreateErrorResponseMessageFD("You can't place a request in the past, time " + model.RequestTime.ToString(), App.Global.Enums.ResultStatusCode.Failed);
            }
            var address = await addressRepository.GetAsync(model.AddressId);
            if (address.IsNull())
            {
                return ResultFactory<RequestT>.CreateErrorResponseMessage("Address not found", App.Global.Enums.ResultStatusCode.NotFound);
            }
            var branch = await cityService.GetCityBranchAsync(address.CityId.Value);
            if (branch.IsNull())
            {
                return ResultFactory<RequestT>.CreateErrorResponseMessage("Branch not found", App.Global.Enums.ResultStatusCode.NotFound);
            }
            
            if (string.IsNullOrEmpty(model.EmployeeId) == false && model.SkipCheckEmployeeStatus == false)
            {
                var result = await employeeRequestService.ValidateEmployeeForRequest(model.EmployeeId, model.RequestTime.Value, branch.BranchId, departmentId);
                if (result.IsFail)
                {
                    return result.Convert(new RequestT());
                }
            }
            return ResultFactory<RequestT>.CreateSuccessResponse();
        }

        private async Task<Result<RequestT>> ValidateRequestSubscription(ClientSubscriptionT clientSubscription, DateTime requestTime)
        {
            if (clientSubscription.IsNull())
            {
                return ResultFactory<RequestT>.CreateErrorResponseMessage("Client subscription not found", App.Global.Enums.ResultStatusCode.NotFound);
            }
            if (clientSubscription.IsCanceled)
            {
                return ResultFactory<RequestT>.CreateErrorResponseMessage("Client subscription is canceled", App.Global.Enums.ResultStatusCode.Failed);
            }
            bool isExpired = await subscriptionRequestService.IsExpiredAsync(clientSubscription.ClientSubscriptionId, requestTime);
            if (isExpired)
            {
                return ResultFactory<RequestT>.CreateErrorResponseMessage("Client subscription is expired", App.Global.Enums.ResultStatusCode.Failed);
            }
            if (requestTime < clientSubscription.CreationTime.Value)
            {
                return ResultFactory<RequestT>.CreateErrorResponseMessage($"Please select a date after subscription date", App.Global.Enums.ResultStatusCode.Failed);
            }
            if (clientSubscription.ExpireDate.HasValue && requestTime > clientSubscription.ExpireDate.Value)
            {
                return ResultFactory<RequestT>.CreateErrorResponseMessage($"Please select a date before the subscription end date", App.Global.Enums.ResultStatusCode.Failed);
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
            bool isContractSubscription = await subscriptionRequestService.IsContract(clientSubscription.ClientSubscriptionId);
            if (isContractSubscription)
            {
                var isExceed = await subscriptionRequestService.IsExceedContractSubscriptionLimitAsync(clientSubscription.ClientSubscriptionId, requestTime);
                if (isExceed)
                {
                    return ResultFactory<RequestT>.CreateErrorResponseMessage("Your have been reach the request count limit for this subscription", App.Global.Enums.ResultStatusCode.Failed);
                }
            }
            else
            {
                var isExceed = await subscriptionRequestService.IsExceedSubscriptionLimitAsync(clientSubscription.ClientSubscriptionId, requestTime);
                if (isExceed)
                {
                    return ResultFactory<RequestT>.CreateErrorResponseMessage("Your subscription is ended for this month", App.Global.Enums.ResultStatusCode.Failed);
                }
            }
            return ResultFactory<RequestT>.CreateSuccessResponse();

        }

        private async Task<Result<CartT>> GetRequestCart(ClientSubscriptionT clientSubscription, DateTime requestTime, int clientId, bool isViaApp)
        {
            CartT cart;
            if (clientSubscription.IsNotNull())
            {
                var subscriptionCustomCart = new CartT
                {
                    ClientId = clientId,
                    DepartmentId = clientSubscription.Subscription.DepartmentId,
                    CreationTime = DateTime.Now.EgyptTimeNow(),
                    IsViaApp = isViaApp,
                    UsePoint = false,
                    CartDetailsT = new List<CartDetailsT>
                    {
                        new CartDetailsT
                        {
                            ServiceQuantity = 1,
                            CreationTime = DateTime.Now.EgyptTimeNow(),
                            ServiceId = clientSubscription.SubscriptionService.ServiceId,
                        }
                    }
                };
                await cartService.AddAsync(subscriptionCustomCart);
                var affectedRow = await unitOfWork.SaveAsync();
                if (affectedRow <= 0)
                {
                    return ResultFactory<CartT>.CreateErrorResponseMessage("Can't create cart");
                }
                cart = subscriptionCustomCart;
            }
            else
            {
                cart = await cartService.GetCurrentByClientIdAsync(clientId, isViaApp);
                if (cart.IsNull())
                {
                    return ResultFactory<CartT>.CreateErrorResponseMessage("Cart not found", App.Global.Enums.ResultStatusCode.NotFound);
                }
            }
            return ResultFactory<CartT>.CreateSuccessResponse(cart);
        }

        public async Task<Result<RequestT>> AddAsync(AddRequestDto model, bool isViaApp, int systemUserId)
        {
            bool isRootTransaction = false;
            CartT cart;
            int? cityId;
            int branchId;
            ClientSubscriptionT clientSubscription = null;
            Result<RequestT> result;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                if (model.ClientSubscriptionId.HasValue)
                {
                    clientSubscription = await clientSubscriptionRepository.Where(d => d.ClientSubscriptionId == model.ClientSubscriptionId.Value)
                            .Include(d => d.Subscription)
                            .Include(d => d.SubscriptionService)
                            .FirstOrDefaultAsync();
                    result = await ValidateRequestSubscription(clientSubscription, model.RequestTime.Value);
                    if (result.IsFail)
                    {
                        return result;
                    }
                }
                var cartResult = await GetRequestCart(clientSubscription, model.RequestTime.Value, model.ClientId.Value, isViaApp);
                if (cartResult.IsFail)
                {
                    return cartResult.Convert(new RequestT());
                }
                cart = cartResult.Data;
                result = await ValidateRequestMainData(model, cart.DepartmentId, isViaApp);
                if (result.IsFail)
                {
                    return result;
                }
                cityId = await addressRepository.Where(d => d.AddressId == model.AddressId)
                   .Select(d => d.CityId).FirstOrDefaultAsync();
                var customCart = await cartService.GetCartForAppAsync(cart.CartId, cityId, model.ClientSubscriptionId, model.RequestTime.Value);
                if (customCart.IsNull())
                {
                    return ResultFactory<RequestT>.CreateErrorResponseMessage("Error with custom Cart", App.Global.Enums.ResultStatusCode.NotFound);
                }
                branchId = await cityService.GetCityBranchIdAsync(cityId.Value);
                var request = new RequestT
                {
                    RequestedAddressId = model.AddressId,
                    RequestedPhoneId = model.PhoneId,
                    EmployeeId = model.EmployeeId,
                    SystemUserId = systemUserId,
                    RequestTimestamp = model.RequestTime,
                    RequestCurrentTimestamp = DateTime.Now.ToEgyptTime(),
                    BranchId = branchId,
                    RequestStatus = string.IsNullOrEmpty(model.EmployeeId) ? ((sbyte)Domain.Enum.RequestStatus.NotSet) : ((sbyte)Domain.Enum.RequestStatus.Waiting),
                    SiteId = model.SiteId,
                    IsPaid = false,
                    IsReviewed = isViaApp ? false : true,
                    RequestStagesT = new RequestStagesT
                    {
                        PaymentFlag = false,
                        SentTimestamp = DateTime.Now.EgyptTimeNow()
                    },
                };
                if (clientSubscription.IsNotNull())
                {
                    request.ClientSubscriptionId = clientSubscription.ClientSubscriptionId;
                    request.SubscriptionId = clientSubscription.SubscriptionId;
                }
                request = ConvertCustomCartToRequest(customCart, request);
                request.RequestDiscountT.ToList().ForEach(d => d.SystemUserId = systemUserId);
                await AddAsync(request);
                cart.HaveRequest = true;
                await cartService.UpdateAsync(cart);
                if (request.UsedPoints > 0)
                {
                    await unitOfWork.SaveAsync();
                    await clientPointService.WithdrawAsync(new ClientPointT
                    {
                        ClientId = request.ClientId,
                        Points = request.UsedPoints,
                        Reason = translationService.Tranlate("Request") + $" #{request.RequestId}",
                        SystemUserId = systemUserId,
                        RequestId = request.RequestId
                    });
                }
                if (model.ClientSubscriptionId.HasValue)
                {
                    var requestCountAfter = await requestRepository.DbSet
                        .CountAsync(d => d.ClientSubscriptionId == model.ClientSubscriptionId && d.IsCanceled == false
                        && d.IsCompleted == false && d.RequestTimestamp >= model.RequestTime && d.RequestId != request.RequestId);
                    if(requestCountAfter > 0)
                    {
                        var resetResult = await ResetSubscriptionMonthRequestAsync(model.ClientSubscriptionId.Value, model.RequestTime.Value);
                        if (resetResult.IsFail)
                        {
                            return ResultFactory<RequestT>.CreateErrorResponseMessage("Error reset subscription request");
                        }
                    }
                }
                if ((isViaApp && (string.IsNullOrEmpty(model.EmployeeId))) || string.IsNullOrEmpty(model.EmployeeId))
                {
                    await unitOfWork.SaveAsync();
                    var broadCastResult = await operationService.BroadcastAsync(request.RequestId);
                    if (broadCastResult.IsFail)
                    {
                        return broadCastResult.Convert(request);
                    }
                }
                int affectedRows = 0;
                if (isRootTransaction)
                {
                    affectedRows = await unitOfWork.CommitAsync(false);
                }
                if (affectedRows >= 0)
                {
                    string title = "طلب جديد";
                    string body = $"تم حجز الطلب الخاص بكم بنجاح, رقم الطلب #{request.RequestId}";
                    try { await notificatonService.SendFirebaseNotificationAsync(AccountType.Client, request.ClientId.ToString(), title, body); } catch { }
                    return ResultFactory<RequestT>.CreateSuccessResponse(request);
                }
                else
                {
                    return ResultFactory<RequestT>.CreateErrorResponse();
                }
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return ResultFactory<RequestT>.CreateExceptionResponse(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
          
           
        }

        public async Task<List<AppRequestDto>> GetAppList(int? clientId = null, string employeeId = null, int? status = null)
        {
            bool getCanceled = false;
            var cancelStatusGroupId = GeneralSetting.GetRequestStatusGroupId(RequestStatus.Canceled);
            if(cancelStatusGroupId == status)
            {
                getCanceled = true;
            }
            var query = requestRepository.DbSet.AsQueryable();
            if (clientId.HasValue)
            {
                query = query.Where(d => d.ClientId == clientId.Value);
            }
            if(string.IsNullOrEmpty(employeeId) is false)
            {
                query = query.Where(d => d.EmployeeId == employeeId);
            }
            if (status.HasValue && getCanceled == false)
            {
                query = query.Where(d => d.RequestStatusNavigation.RequestStatusGroupId == status.Value && d.IsCanceled == false);
            }
            if (getCanceled)
            {
                query = query.Where(d => d.IsCanceled);
            }
            query = query.OrderByDescending(d => d.RequestTimestamp);
            var list = await query.Select(d => new AppRequestDto
            {
                Date = d.RequestTimestamp.Value.ToString("yyyy-MM-dd"),
                DayOfWeek = translationService.Tranlate(d.RequestTimestamp.Value.DayOfWeek.ToString()),
                Department = d.Department.DepartmentName,
                RequestCaption = translationService.Tranlate("Request") + " #" + d.RequestId,
                RequestId = d.RequestId,
                RequestStatus = translationService.Tranlate(d.RequestStatusNavigation.RequestStatusName),
                Time = d.RequestTimestamp.Value.ToString("tt hh:mm"),
                Services = string.Join(", ", d.RequestServicesT.Select(t => t.Service.ServiceName))
            }).ToListAsync();
            return list;
        }

        public async Task<AppRequestDetailsDto> GetAppDetails(int requestId)
        {
            var request = await requestRepository.Where(d => d.RequestId == requestId)
                   .Select(d => new AppRequestDetailsDto
                   {
                       CityName = d.RequestedAddress.City.CityName,
                       RequestId = d.RequestId,
                       FlatNumber = d.RequestedAddress.AddressFlatNum.HasValue ? d.RequestedAddress.AddressFlatNum.Value : 0,
                       Date = d.RequestTimestamp.Value.ToString("yyyy-MM-dd"),
                       DepartmentId = d.DepartmentId.Value,
                       FloorNumber = d.RequestedAddress.AddressBlockNum.HasValue ? d.RequestedAddress.AddressBlockNum.Value : 0,
                       DepartmentName = d.Department.DepartmentName,
                       StreetName = d.RequestedAddress.AddressStreet,
                       Phone = d.RequestedPhone.ClientPhone,
                       RegionName = d.RequestedAddress.Region.RegionName,
                       RequestServiceList = d.RequestServicesT.OrderBy(t => t.RequestServiceId).Select(t => new RequestServiceDto
                       {
                           Discount = t.ServiceDiscount,
                           NetPrice = t.ServicePrice - t.ServiceDiscount,
                           Price = t.ServicePrice,
                           Quantity = t.RequestServicesQuantity,
                           RequestServiceId = t.RequestServiceId,
                           ServiceId = t.ServiceId,
                           ServiceDescription = t.Service.ServiceDes,
                           ServiceName = t.Service.ServiceName
                       }).ToList(),
                       RequestCaption = translationService.Tranlate("Request") + " #" + d.RequestId,
                       Time = d.RequestTimestamp.Value.ToString("tt hh:mm"),
                       DayOfWeek = translationService.Tranlate(d.RequestTimestamp.Value.DayOfWeek.ToString()),
                       Employee = d.EmployeeId == null ? null : new Domain.OtherModels.AppEmployeeDto
                       {
                           DepartmentId = d.DepartmentId.Value,
                           Id = d.EmployeeId,
                           Image = d.Employee.EmployeeImageUrl,
                           Name = d.Employee.EmployeeName,
                           Title = d.Employee.Title,
                           Phone = d.Employee.EmployeePhone,
                           //Rate = d.EmployeeReviewT.Sum(e => e.Rate) / d.Employee.EmployeeReviewT.Count,
                           //IsFavourite = d.Client.FavouriteEmployeeT.Any(t => t.EmployeeId == d.EmployeeId),
                       },
                       InvoiceDetails = new Dictionary<string, decimal>
                       {
                           { "الاجمالى", Math.Round(d.TotalPrice, 2) },
                           { "انتقالات", Math.Round(d.DeliveryPrice, 2) },
                           { "الخصم", Math.Round(d.TotalDiscount, 2) },
                           { "المطلوب", Math.Round(d.CustomerPrice, 2) }
                       },
                       IsCanceled = d.IsCanceled,
                       IsCompleted = d.IsCompleted,
                       RequestTimestamp = d.RequestTimestamp.Value
                   }).FirstOrDefaultAsync();

            if (request.IsNotNull())
            {
                if (request.IsCanceled || request.IsCompleted)
                {
                    request.ShowCancelRequestButton = false;
                    request.ShowAddServiceButton = false;
                    request.ShowReAssignEmployeeButton = false;
                    request.ShowDelayRequestButton = false;
                }
                else
                {
                    request.ShowCancelRequestButton = true;
                    request.ShowAddServiceButton = true;
                    request.ShowReAssignEmployeeButton = true;
                    request.ShowDelayRequestButton = true;
                }
                if (request.Employee.IsNotNull() && DateTime.Now.EgyptTimeNow() > request.RequestTimestamp.AddHours(-1)
                    && request.IsCanceled == false && request.IsCompleted == false)
                {
                    request.Employee.ShowContact = true;
                    request.ShowReAssignEmployeeButton = false;
                    request.ShowDelayRequestButton = false;
                }
            }
            return request;
        }

        public async Task<EmpAppRequestDetailsDto> GetEmpAppDetails(int requestId)
        {
            var request = await requestRepository.Where(d => d.RequestId == requestId)
                .Select(d => new EmpAppRequestDetailsDto
                {
                    EmployeeId = d.EmployeeId,
                    City = d.RequestedAddress.City.CityName,
                    RequestId = d.RequestId,
                    FlatNumber = d.RequestedAddress.AddressFlatNum.HasValue ? d.RequestedAddress.AddressFlatNum.Value : 0,
                    Date = d.RequestTimestamp.Value.ToString("yyyy-MM-dd"),
                    DepartmentId = d.DepartmentId.Value,
                    BlockNumber = d.RequestedAddress.AddressBlockNum.HasValue ? d.RequestedAddress.AddressBlockNum.Value : 0,
                    Street = d.RequestedAddress.AddressStreet,
                    Phone = d.RequestedPhone.ClientPhone,
                    Region = d.RequestedAddress.Region.RegionName,
                    Note = d.RequestNote,
                    RequestServices = d.RequestServicesT.OrderBy(t => t.RequestServiceId).Select(t => new RequestServiceDto
                    {
                        Discount = t.ServiceDiscount,
                        NetPrice = t.ServicePrice - t.ServiceDiscount,
                        Price = t.ServicePrice,
                        Quantity = t.RequestServicesQuantity,
                        RequestServiceId = t.RequestServiceId,
                        ServiceId = t.ServiceId,
                        ServiceDescription = t.Service.ServiceDes,
                        ServiceName = t.Service.ServiceName
                    }).ToList(),
                    RequestCaption = translationService.Tranlate("Request") + " #" + d.RequestId,
                    Time = d.RequestTimestamp.Value.ToString("tt hh:mm"),
                    InvoiceDetails = new List<InvoiceDetailsDto>
                    {
                        new InvoiceDetailsDto {Name = "الاجمالى", Amount = Math.Round(d.TotalPrice, 2) },
                        new InvoiceDetailsDto {Name = "انتقالات", Amount = Math.Round(d.DeliveryPrice, 2) },
                        new InvoiceDetailsDto {Name = "الخصم", Amount = Math.Round(d.TotalDiscount, 2) },
                        new InvoiceDetailsDto {Name = "المطلوب", Amount = Math.Round(d.CustomerPrice, 2), Bold = true },
                    },
                    AddressDescription = d.RequestedAddress.AddressDes,
                    ClientName = d.Client.ClientName,
                    Location = d.RequestedAddress.Location,
                    Lat = d.RequestedAddress.Latitude,
                    Lng = d.RequestedAddress.Longitude,
                    Status = d.RequestStatusNavigation.RequestStatusName,
                    CartId = d.CartId.HasValue ? d.CartId.Value : 0
                }).FirstOrDefaultAsync();
            request.Attachments = await attachmentRepository.Where(a => a.AttachmentType == (int)Domain.Enum.AttachmentType.CartImage && a.ReferenceId == request.CartId.ToString())
                .Select(a => new AttachmentDto
                {
                    Id = a.AttachmentId,
                    Path = a.FilePath,
                    DomainUrl = helperService.GetHost(),
                    Url = helperService.GetHost() + a.FilePath
                }).ToListAsync();
            var otherDetails = await requestRepository.Where(d => d.RequestId == requestId)
                .Select(d => new { d.RequestStatus, d.RequestTimestamp, d.IsCanceled, d.IsCompleted }).FirstOrDefaultAsync();

            request.Tracking = await GetEmpAppRequestTrackingAsync(otherDetails.IsCanceled, requestId);
            request.ShowRejectRequestButton = true;
            if(otherDetails.RequestStatus == GeneralSetting.GetRequestStatusId(RequestStatus.Waiting)
                || otherDetails.RequestStatus == GeneralSetting.GetRequestStatusId(RequestStatus.Delayed))
            {
                request.AddressDescription = "-";
                request.FlatNumber =  0;
                request.BlockNumber = 0;
                request.ClientName = "-";
                request.Phone = "-";
                request.Location = "-";
                request.Lat = null;
                request.Lng =  null;
                if (DateTime.Now.EgyptTimeNow() >= otherDetails.RequestTimestamp.Value.AddHours(-3))
                {
                    request.ShowStartRequestButton = true;
                }
                request.Tracking.SelectedItemId = (int)EmployeeRequestTrackingItem.Waiting;
                return request;
            }
            
            if (otherDetails.IsCanceled || otherDetails.IsCompleted)
            {
                request.Tracking.SelectedItemId = (int)EmployeeRequestTrackingItem.DoneOrCanceled;
                request.ShowRejectRequestButton = false;
                return request;
            }
            if (otherDetails.RequestStatus == GeneralSetting.GetRequestStatusId(RequestStatus.InExcution))
            {
                request.Tracking.SelectedItemId = (int)EmployeeRequestTrackingItem.GoToClient;
                request.ShowCallClientButton = true;
                request.ShowChangeTimeButton = true;
                request.ShowArrivalButton = true;
                return request;
            }
            if (otherDetails.RequestStatus == GeneralSetting.GetRequestStatusId(RequestStatus.StartExcution))
            {
                request.Tracking.SelectedItemId = (int)EmployeeRequestTrackingItem.InExcution;
                request.ShowCallClientButton = true;
                request.ShowChangeTimeButton = true;
                request.ShowRejectRequestButton = false;
                request.CanAddOrUpdate = true;
                request.ShowEndRequestButton = true;
                return request;
            }
            return request;
        }

        private async Task<EmployeeAppTrackingDto> GetEmpAppRequestTrackingAsync(bool isCanceled, int requestId)
        {
            var requestDetalis = await requestRepository.Where(d => d.RequestId == requestId)
                .Select(d => new 
                { 
                    d.RequestStagesT.AcceptTimestamp,
                    d.RequestTimestamp, 
                    d.RequestStagesT.FinishTimestamp
                })
                .FirstOrDefaultAsync();
            EmployeeAppTrackingDto tracking = new EmployeeAppTrackingDto();
            tracking.TrackingItemList = new List<EmployeeAppTrackingItemDto>();
            tracking.TrackingItemList.Add(new EmployeeAppTrackingItemDto
            {
                Id = (int)EmployeeRequestTrackingItem.Waiting,
                Date = requestDetalis.AcceptTimestamp.HasValue ? requestDetalis.AcceptTimestamp.ToString() : "",
                Name = "تم اختيارك, الطلب قيد الانتظار"
            });
            tracking.TrackingItemList.Add(new EmployeeAppTrackingItemDto
            {
                Id = (int)EmployeeRequestTrackingItem.GoToClient,
                Date = requestDetalis.RequestTimestamp.Value.AddHours(-1).ToString(),
                Name = "عليك التوجة الى العميل الان"
            });
            tracking.TrackingItemList.Add(new EmployeeAppTrackingItemDto
            {
                Id = (int)EmployeeRequestTrackingItem.InExcution,
                Date = DateTime.Now.ToString(),
                Name = "جارى تنفيذ الطلب"
            });
            if (isCanceled)
            {
                var canceledTime = await cancelRequestRepository.Where(d => d.RequestId == requestId)
                    .Select(d => d.CancelRequestTimestamp)
                    .FirstOrDefaultAsync();
                tracking.TrackingItemList.Add(new EmployeeAppTrackingItemDto
                {
                    Id = (int)EmployeeRequestTrackingItem.DoneOrCanceled,
                    Date = canceledTime.ToString(),
                    Name = "تم الغاء الطلب"
                });
            }
            else
            {
                tracking.TrackingItemList.Add(new EmployeeAppTrackingItemDto
                {
                    Id = (int)EmployeeRequestTrackingItem.DoneOrCanceled,
                    Date = requestDetalis.FinishTimestamp.HasValue ? requestDetalis.FinishTimestamp.Value.ToString() : "",
                    Name = "تم انتهاء الطلب"
                });
            }
            return tracking;
        }

        public async Task<Result<RequestCanceledT>> CancelAsync(int requestId, string reason, int systemUserId, bool resetSubscriptionMonthRequest = true)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                if (string.IsNullOrEmpty(reason))
                {
                    return ResultFactory<RequestCanceledT>.CreateNotFoundResponse("Please fill the reason first");
                }
                var request = await requestRepository.Where(d => d.RequestId == requestId)
                    .Include(d => d.RequestCanceledT)
                    .FirstOrDefaultAsync();
                var requestValidation = helperService.ValidateRequest<RequestCanceledT>(request);
                if (requestValidation.IsFail) { return requestValidation; }
                var cancelRequest = new RequestCanceledT
                {
                    CancelRequestReason = translationService.Tranlate(reason),
                    CancelRequestTimestamp = DateTime.Now.EgyptTimeNow(),
                    RequestId = requestId,
                    SystemUserId = systemUserId
                };
                await cancelRequestRepository.AddAsync(cancelRequest);
                request.IsCanceled = true;
                request.RequestStatus = GeneralSetting.GetRequestStatusId(RequestStatus.Canceled);
                await UpdateAsync(request);
                if (request.UsedPoints > 0)
                {
                    await clientPointService.AddAsync(new ClientPointT
                    {
                        ClientId = request.ClientId,
                        Points = request.UsedPoints,
                        Reason = translationService.Tranlate("Cancel Request") + $" #{requestId}",
                        SystemUserId = systemUserId,
                        RequestId = request.RequestId
                    });
                }
                if(string.IsNullOrEmpty(request.EmployeeId) is false)
                {
                    string title = "الغاء طلب";
                    string body = string.Format("لقد تم الغاء الطلب رقم {0} من قبل العميل", requestId);
                    try { await notificatonService.SendFirebaseNotificationAsync(Domain.Enum.AccountType.Employee, request.EmployeeId, title, body); } catch { }
                    await messageRepository.AddAsync(new MessagesT
                    {
                        EmployeeId = request.EmployeeId,
                        MessageTimestamp = DateTime.Now.ToEgyptTime(),
                        IsRead = 0,
                        Title = title,
                        Body = body
                    });
                }
                if (request.ClientSubscriptionId.HasValue && resetSubscriptionMonthRequest)
                {
                    await unitOfWork.SaveAsync();
                    var result = await ResetSubscriptionMonthRequestAsync(request.ClientSubscriptionId.Value, request.RequestTimestamp.Value);
                    if (result.IsFail)
                    {
                        return ResultFactory<RequestCanceledT>.CreateErrorResponseMessage("Error while reset subscription month requests");
                    }
                }
                try
                {
                    string title = $"طلب #{request.RequestId}";
                    string body = $"عزيزى العميل, تم إلغاء الطلب الخاص بكم, فى حالة وجود أى شكاوى الرجاء الاتصال بنا";
                    await notificatonService.SendFirebaseNotificationAsync(AccountType.Client, request.ClientId.ToString(), title, body);
                }
                catch { }
                int affectedRows = 0;
                if (isRootTransaction)
                {
                    affectedRows = await unitOfWork.CommitAsync(false);
                }
                return ResultFactory<RequestCanceledT>.CreateAffectedRowsResult(affectedRows, data: cancelRequest);
            }
            catch(Exception ex)
            {
                unitOfWork.RollBack();
                return ResultFactory<RequestCanceledT>.CreateExceptionResponse(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
        }

        public async Task<Result<int>> ResetSubscriptionMonthRequestAsync(int clientSubscriptionId, DateTime dateTime)
        {
            int affectedRows = 0;
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
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
                if (isRootTransaction)
                {
                    affectedRows = await unitOfWork.CommitAsync(false);
                }
                return ResultFactory<int>.CreateAffectedRowsResult(affectedRows);
            }
            catch(Exception ex)
            {
                unitOfWork.RollBack();
                return ResultFactory<int>.CreateExceptionResponse(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
          
        }

        public async Task<int> UpdateAsync(RequestT request)
        {
            requestRepository.Update(request.RequestId, request);
            return await requestRepository.SaveAsync();
        }

        public async Task<int> UpdateWithCartAsync(RequestT request)
        {
            bool isRootTransaction = false;
            AddressT address;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
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
                    if (request.UsedPoints > 0)
                    {
                        await clientPointService.DeletetByRequestIdAsync(request.RequestId);
                        await unitOfWork.SaveAsync();
                    }
                    request.UsedPoints = 0;
                    var customCart = await cartService.GetCartForAppAsync(request.CartId.Value, address.CityId, request.ClientSubscriptionId, request.RequestTimestamp, request);
                    request = ConvertCustomCartToRequest(customCart, request);
                    if (request.UsedPoints > 0)
                    {
                        await clientPointService.WithdrawAsync(new ClientPointT
                        {
                            ClientId = request.ClientId,
                            Points = request.UsedPoints,
                            Reason = translationService.Tranlate("Request") + $" #{request.RequestId}",
                            SystemUserId = request.SystemUserId,
                            RequestId = request.RequestId
                        });
                    }
                }
                requestRepository.Update(request.RequestId, request);
                if (isRootTransaction)
                {
                    return await unitOfWork.CommitAsync(false);
                }
                return (int)App.Global.Enums.ResultStatusCode.Success;
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return (int)App.Global.Enums.ResultStatusCode.Exception;
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }

        }

        public async Task<Result<RequestDelayedT>> ChangeTimeAsync(int requestId, DateTime newTime, string reason, int systemUserId, bool skipCheckEmployee = false)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                if (newTime <= DateTime.Now.EgyptTimeNow())
                {
                    return ResultFactory<RequestDelayedT>.CreateErrorResponseMessageFD("The selected time is not valid");
                }
                var request = await GetAsync(requestId);
                var requestValidation = helperService.ValidateRequest<RequestDelayedT>(request);
                if (requestValidation.IsFail) { return requestValidation; }
                if (string.IsNullOrEmpty(request.EmployeeId) == false)
                {
                    if (skipCheckEmployee == false)
                    {
                        var employeeValidateResult = await employeeRequestService.ValidateEmployeeForRequest(request.EmployeeId, newTime, request.BranchId,  request.DepartmentId, requestId);
                        if (employeeValidateResult.IsFail)
                        {
                            return employeeValidateResult.Convert(new RequestDelayedT());
                        }
                    }
                    string title = "تأجبل طلب";
                    string body = string.Format("لقد تم تاجيل الطلب رقم {0} الى ميعاد {1}", requestId, newTime.ToString());
                    try { await notificatonService.SendFirebaseNotificationAsync(Domain.Enum.AccountType.Employee, request.EmployeeId, title, body); } catch { }
                    await messageRepository.AddAsync(new MessagesT
                    {
                        EmployeeId = request.EmployeeId,
                        MessageTimestamp = DateTime.Now.ToEgyptTime(),
                        IsRead = 0,
                        Title = title,
                        Body = body
                    });
                    try
                    {
                        title = $"طلب #{request.RequestId}";
                        body = $"عزيزى العميل, تم تغيير تنفيذ ميعاد الطلب الخاص بكم إلى {request.RequestTimestamp.ToString()}";
                        await notificatonService.SendFirebaseNotificationAsync(AccountType.Client, request.ClientId.ToString(), title, body);
                    }
                    catch { }
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
                request.RequestStatus = GeneralSetting.GetRequestStatusId(RequestStatus.Delayed);
                request.RequestTimestamp = newTime;
                await UpdateAsync(request);
                int affectedRows = 0;
                if (isRootTransaction)
                {
                    affectedRows = await unitOfWork.CommitAsync(false);
                }
                return ResultFactory<RequestDelayedT>.CreateAffectedRowsResult(affectedRows, data: delayRequest);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return ResultFactory<RequestDelayedT>.CreateExceptionResponse(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
           
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
            if(request.IsNull() || cart.IsNull()) { return; }
            if (request.RequestServicesT.IsNull())
            {
                request.RequestServicesT = new List<RequestServicesT>();
            }
            foreach (var item in cart.CartServiceDetails)
            {
                var requestService = request.RequestServicesT.FirstOrDefault(d => d.ServiceId == item.ServiceId);
                if(requestService.IsNotNull())
                {
                    requestService.ServicePrice = item.Price;
                    requestService.ServiceDiscount = item.Discount;
                    requestService.RequestServicesQuantity = item.ServiceQuantity;
                    requestService.ServiceMaterialCost = item.MaterialCost;
                    requestService.ServiceId = item.ServiceId;
                    requestService.ServicePoint = item.Points;
                }
                else
                {
                    request.RequestServicesT.Add(new RequestServicesT
                    {
                        AddTimestamp = DateTime.Now.EgyptTimeNow(),
                        ServicePrice = item.Price,
                        ServiceDiscount = item.Discount,
                        RequestServicesQuantity = item.ServiceQuantity,
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
                if (discount.IsNotNull())
                {
                    discount.DiscountValue = item.DiscountValue;
                }
                else
                {
                    request.RequestDiscountT.Add(new RequestDiscountT
                    {
                        CreationTime = DateTime.Now.EgyptTimeNow(),
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
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var request = await requestRepository.Where(d => d.RequestId == requestService.RequestId)
                    .Include(d => d.RequestServicesT)
                    .Include(d => d.RequestStagesT)
                    .Include(d => d.RequestDiscountT)
                    .FirstOrDefaultAsync();

                var requestValidation = helperService.ValidateRequest<RequestServicesT>(request);
                if (requestValidation.IsFail)
                {
                    return requestValidation;
                }
                if (request.RequestServicesT.Count <= 1 && requestService.RequestServicesQuantity <= 0)
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
                var requestServiceFound = request.RequestServicesT.FirstOrDefault(d => d.ServiceId == requestService.ServiceId);
                if (requestServiceFound.IsNotNull())
                {
                    if (requestService.RequestServicesQuantity <= 0)
                    {
                        //request.RequestServicesT.Remove(requestServiceFound);
                        await requestServiceRepository.DeleteAsync(requestServiceFound.RequestServiceId);
                    }
                    else
                    {
                        requestServiceFound.RequestServicesQuantity = requestService.RequestServicesQuantity;
                    }
                }
                else
                {
                    request.RequestServicesT.Add(requestService);
                }
                await unitOfWork.SaveAsync();
                await UpdateWithCartAsync(request);
                int affectedRows = 0;
                if (isRootTransaction)
                {
                    affectedRows = await unitOfWork.CommitAsync(false);
                }
                return ResultFactory<RequestServicesT>.CreateAffectedRowsResult(affectedRows);
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                return ResultFactory<RequestServicesT>.CreateExceptionResponse(ex);
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
            
        }

        public async Task<Result<RequestServicesT>> AddUpdateServiceOAsync(AddUpdateeRequestServiceODto model)
        {
            if (model.ServiceName.IsNull() || model.ServiceName.Contains("-") == false)
            {
                return ResultFactory<RequestServicesT>.CreateNotFoundResponse("Service not found");
            }
            var list = model.ServiceName.Split('-');
            var serviceId = Convert.ToInt32(list.LastOrDefault().Trim());
            var requestService = new RequestServicesT
            {
                RequestId = model.RequestId,
                AddTimestamp = DateTime.Now,
                RequestServicesQuantity = model.ServiceQuantity,
                ServiceId = serviceId
            };
            return await AddUpdateServiceAsync(requestService);
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
            var result = helperService.ValidateClientSubscription(clientSubscription);
            if (result.IsFail)
            {
                return result.Convert(new RequestT());
            }
            var time = new DateTime(requestTime.Year, requestTime.Month, requestTime.Day, clientSubscription.VisitTime.Value.Hour, 0, 0);
            return await AddAsync(new AddRequestDto
            {
                AddressId = clientSubscription.AddressId,
                ClientId = clientSubscription.ClientId,
                ClientSubscriptionId = clientSubscription.ClientSubscriptionId,
                EmployeeId = clientSubscription.EmployeeId,
                PhoneId = clientSubscription.PhoneId,
                RequestTime = time,
            }, isViaApp, systemUserId);
        }

        public async Task<int> UpdatePriceAsync(UpdateRequestPriceDto model)
        {
            var request = await requestRepository.GetAsync(model.RequestId);
            if (request.IsNull())
            {
                return (int)App.Global.Enums.ResultStatusCode.NotFound;
            }
            request.CustomerPrice = model.CustomerPrice;
            request.CompanyPercentageAmount = model.CompanyPercentage;
            request.EmployeePercentageAmount = model.EmployeePercentage;
            requestRepository.Update(request.RequestId, request);
            return await requestRepository.SaveAsync();
        }

        public async Task<int> UpdatePhoneAsync(UpdateRequestPhoneDto model)
        {
            var request = await requestRepository.GetAsync(model.RequestId);
            if (request.IsNull())
            {
                return (int)App.Global.Enums.ResultStatusCode.NotFound;
            }
            request.RequestedPhoneId = model.PhoneId;
            requestRepository.Update(request.RequestId, request);
            return await requestRepository.SaveAsync();
        }

        public async Task<int> UpdateAddressAsync(UpdateRequestAddressDto model)
        {
            var request = await requestRepository.GetAsync(model.RequestId);
            if (request.IsNull())
            {
                return (int)App.Global.Enums.ResultStatusCode.NotFound;
            }
            request.RequestedPhoneId = model.AddressId;
            requestRepository.Update(request.RequestId, request);
            return await requestRepository.SaveAsync();
        }

    }
}
