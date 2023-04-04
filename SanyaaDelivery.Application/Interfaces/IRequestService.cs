using App.Global.DTOs;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IRequestService
    {
        Task<int> GetAllOrdersCountByEmployee(string employeeId, DateTime time);
        Task<int> GetComlpeteOrdersCountByEmployee(string employeeId, DateTime time);
        Task<int> GetWaitingOrdersCountByEmployee(string employeeId, DateTime time);
        Task<int> GetCanceledOrdersCountByEmployee(string employeeId, DateTime time);
        Task<int> GetOrdersExceptCanceledCountByEmployee(string employeeId, DateTime time);
        Task<RequestT> GetAsync(int requestId, bool includeService = false, bool includeAddress = false);
        Task<RequestT> GetDetails(int requestId);
        Task<RequestT> GetInfo(int requestId);
        Task<List<RequestT>> GetList(DateTime? startDate = null, DateTime? endDate = null, int? requestId = null, int? siteId = null,
            int? subscriptionId = null, int? clientSubscriptionId = null, int? clientId = null, string employeeId = null, int? systemUserId = null, int? requestStatus = null, int? requestStatusGroupId = null,
            bool? getCanceled = null, int? branchId = null, bool? isPaid = null, int? promocode = null, int? departmentId = null,
            bool? isCompleted = null, bool? isReviewed = null, bool? isFollowUp = null,
            bool includeRequestStage = false, bool includeClient = false, bool includeEmployee = false, bool includeStatus = false,
            bool includeRequestService = false, bool includeService = false, bool includeDiscounts = false, bool includeCancelT = false,
            bool includeDelayedT = false, bool includeFollowUpT = false, bool includeReviewT = false, bool includeSubscription = false,
            bool includePayment = false, bool includeComplaiment = false, bool includeSite = false, bool includeBill = false,
            bool includeFawryCharge = false, bool includeAddress = false, bool includePhone = false, bool includePromocode = false,
            bool includeDepartment = false, bool includeBranch = false, bool includeSystemUser = false, bool includeEmployeeLogin = false);

        Task<List<AppRequestDto>> GetAppList(int? clientId = null, string employeeId = null, int? status = null);
        Task<AppRequestDetailsDto> GetAppDetails(int requestId);
        Task<List<RequestT>> GetEmployeeOrdersList(string employeeId, DateTime time);
        Task<List<OrderDto>> GetEmployeeOrdersCustomList(string employeeId, DateTime day);
        Task<List<RequestT>> GetEmployeeOrdersExceptCanceledList(DateTime day);
        Task<List<DayOrderDto>> GetDayOrdersCustom(DateTime day);
        Task<List<RequestT>> GetEmployeeOrdersExceptCanceledList(string employeeId, DateTime day);
        Task<List<RequestT>> GetUnPaidAsync(string employeeId, bool ignoreRequestWithValidFawryRequest = false);
        Task<int> AddAsync(RequestT request);
        Task<int> UpdatePriceAsync(UpdateRequestPriceDto model);
        Task<int> UpdatePhoneAsync(UpdateRequestPhoneDto model);
        Task<int> UpdateAddressAsync(UpdateRequestAddressDto model);
        Task<Result<RequestServicesT>> AddUpdateServiceAsync(RequestServicesT requestService);
        Task<Result<RequestServicesT>> AddUpdateServiceOAsync(AddUpdateeRequestServiceODto model);
        Task<RequestServicesT> GetServiceAsync(int requestServiceId);
        Task<List<RequestServicesT>> GetServiceListAsync(int requestId);
        Task<int> UpdateAsync(RequestT request);
        Task<Result<RequestCanceledT>> CancelAsync(int requestId, string reason, int systemUserId, bool resetSubscriptionMonthRequest = true);
        Task<Result<RequestDelayedT>> ChangeTimeAsync(int requestId, DateTime newTime, string reason, int systemUserId, bool skipCheckEmployee = false);
        Task<Result<EmployeeT>> ReAssignEmployeeAsync(ReAssignEmployeeDto model);
        Task<int> AddComplaintAsync(RequestComplaintT requestComplaint);
        Task<int?> GetCartIdAsync(int requestId);
        Task<Result<RequestT>> AddAsync(AddRequestDto model, bool isViaApp, int systemUserId);
        Task<Result<RequestT>> AddAsync(int clientSubscriptionId, DateTime requestDate, int systemUserId, bool isViaApp);
    }


}
