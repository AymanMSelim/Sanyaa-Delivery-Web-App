using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class ClientSubscriptionT
{
    public int ClientSubscriptionId { get; set; }

    public int ClientId { get; set; }

    public int SubscriptionId { get; set; }

    public int SubscriptionServiceId { get; set; }

    public int? AddressId { get; set; }

    public int? PhoneId { get; set; }

    public string EmployeeId { get; set; }

    public DateTime? ExpireDate { get; set; }

    public ulong AutoRenew { get; set; }

    public DateTime? VisitTime { get; set; }

    public ulong IsCanceled { get; set; }

    public ulong IsActive { get; set; }

    public DateTime? CreationTime { get; set; }

    public int SystemUserId { get; set; }

    public virtual AddressT Address { get; set; }

    public virtual ClientT Client { get; set; }

    public virtual EmployeeT Employee { get; set; }

    public virtual ClientPhonesT Phone { get; set; }

    public virtual ICollection<RequestT> RequestT { get; set; } = new List<RequestT>();

    public virtual SubscriptionT Subscription { get; set; }

    public virtual SubscriptionServiceT SubscriptionService { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
