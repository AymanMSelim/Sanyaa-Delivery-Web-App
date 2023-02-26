using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Domain.Models
{
    public partial class RequestT
    {
        public RequestT()
        {
            BillNumberT = new HashSet<BillNumberT>();
            EmployeeReviewT = new HashSet<EmployeeReviewT>();
            FawryChargeRequestT = new HashSet<FawryChargeRequestT>();
            FollowUpT = new HashSet<FollowUpT>();
            PartinerPaymentRequestT = new HashSet<PartinerPaymentRequestT>();
            RejectRequestT = new HashSet<RejectRequestT>();
            RequestCanceledT = new HashSet<RequestCanceledT>();
            RequestComplaintT = new HashSet<RequestComplaintT>();
            RequestDelayedT = new HashSet<RequestDelayedT>();
            RequestDiscountT = new HashSet<RequestDiscountT>();
            RequestServicesT = new HashSet<RequestServicesT>();
        }

        public int RequestId { get; set; }
        public DateTime RequestCurrentTimestamp { get; set; }
        public DateTime? RequestTimestamp { get; set; }
        public string RequestNote { get; set; }
        public sbyte RequestStatus { get; set; }
        public int ClientId { get; set; }
        public string EmployeeId { get; set; }
        public int BranchId { get; set; }
        public int? RequestedAddressId { get; set; }
        public int? RequestedPhoneId { get; set; }
        public int? DepartmentId { get; set; }
        public int RequestPoints { get; set; }
        public int UsedPoints { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal CustomerPrice { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal NetPrice { get; set; }
        public decimal EmployeePercentageAmount { get; set; }
        public decimal CompanyPercentageAmount { get; set; }
        public int? PromocodeId { get; set; }
        public int? SubscriptionId { get; set; }
        public int? ClientSubscriptionId { get; set; }
        public int? SiteId { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsFollowed { get; set; }
        public bool? IsReviewed { get; set; }
        public bool? IsPaid { get; set; }
        public decimal DeliveryPrice { get; set; }
        public int? CartId { get; set; }
        public int SystemUserId { get; set; }

        public BranchT Branch { get; set; }
        public CartT Cart { get; set; }
        public ClientT Client { get; set; }
        public ClientSubscriptionT ClientSubscription { get; set; }
        public DepartmentT Department { get; set; }
        public EmployeeT Employee { get; set; }
        public PromocodeT Promocode { get; set; }
        public RequestStatusT RequestStatusNavigation { get; set; }
        public AddressT RequestedAddress { get; set; }
        public ClientPhonesT RequestedPhone { get; set; }
        public SiteT Site { get; set; }
        public SubscriptionT Subscription { get; set; }
        public SystemUserT SystemUser { get; set; }
        public PaymentT PaymentT { get; set; }
        public RequestStagesT RequestStagesT { get; set; }
        public ICollection<BillNumberT> BillNumberT { get; set; }
        public ICollection<EmployeeReviewT> EmployeeReviewT { get; set; }
        public ICollection<FawryChargeRequestT> FawryChargeRequestT { get; set; }
        public ICollection<FollowUpT> FollowUpT { get; set; }
        public ICollection<PartinerPaymentRequestT> PartinerPaymentRequestT { get; set; }
        public ICollection<RejectRequestT> RejectRequestT { get; set; }
        public ICollection<RequestCanceledT> RequestCanceledT { get; set; }
        public ICollection<RequestComplaintT> RequestComplaintT { get; set; }
        public ICollection<RequestDelayedT> RequestDelayedT { get; set; }
        public ICollection<RequestDiscountT> RequestDiscountT { get; set; }
        public ICollection<RequestServicesT> RequestServicesT { get; set; }
    }
}
