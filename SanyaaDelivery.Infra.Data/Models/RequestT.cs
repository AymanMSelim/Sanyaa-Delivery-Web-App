using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class RequestT
{
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

    public ulong IsCanceled { get; set; }

    public ulong IsConfirmed { get; set; }

    public ulong IsCompleted { get; set; }

    public ulong IsFollowed { get; set; }

    public ulong? IsReviewed { get; set; }

    public ulong? IsPaid { get; set; }

    public decimal DeliveryPrice { get; set; }

    public int? CartId { get; set; }

    public int SystemUserId { get; set; }

    public virtual ICollection<BillNumberT> BillNumberT { get; set; } = new List<BillNumberT>();

    public virtual BranchT Branch { get; set; }

    public virtual ICollection<BroadcastRequestT> BroadcastRequestT { get; set; } = new List<BroadcastRequestT>();

    public virtual CartT Cart { get; set; }

    public virtual ClientT Client { get; set; }

    public virtual ClientSubscriptionT ClientSubscription { get; set; }

    public virtual DepartmentT Department { get; set; }

    public virtual EmployeeT Employee { get; set; }

    public virtual ICollection<EmployeeReviewT> EmployeeReviewT { get; set; } = new List<EmployeeReviewT>();

    public virtual ICollection<FawryChargeRequestT> FawryChargeRequestT { get; set; } = new List<FawryChargeRequestT>();

    public virtual ICollection<FollowUpT> FollowUpT { get; set; } = new List<FollowUpT>();

    public virtual ICollection<PartinerPaymentRequestT> PartinerPaymentRequestT { get; set; } = new List<PartinerPaymentRequestT>();

    public virtual PaymentT PaymentT { get; set; }

    public virtual PromocodeT Promocode { get; set; }

    public virtual ICollection<RejectRequestT> RejectRequestT { get; set; } = new List<RejectRequestT>();

    public virtual ICollection<RequestCanceledT> RequestCanceledT { get; set; } = new List<RequestCanceledT>();

    public virtual ICollection<RequestComplaintT> RequestComplaintT { get; set; } = new List<RequestComplaintT>();

    public virtual ICollection<RequestDelayedT> RequestDelayedT { get; set; } = new List<RequestDelayedT>();

    public virtual ICollection<RequestDiscountT> RequestDiscountT { get; set; } = new List<RequestDiscountT>();

    public virtual ICollection<RequestServicesT> RequestServicesT { get; set; } = new List<RequestServicesT>();

    public virtual RequestStagesT RequestStagesT { get; set; }

    public virtual RequestStatusT RequestStatusNavigation { get; set; }

    public virtual AddressT RequestedAddress { get; set; }

    public virtual ClientPhonesT RequestedPhone { get; set; }

    public virtual SiteT Site { get; set; }

    public virtual SubscriptionT Subscription { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
