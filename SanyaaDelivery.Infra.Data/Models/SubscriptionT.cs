using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class SubscriptionT
{
    public int SubscriptionId { get; set; }

    public string SubscriptionName { get; set; }

    public string Description { get; set; }

    public int DepartmentId { get; set; }

    public sbyte? RequestNumberPerMonth { get; set; }

    public int NumberOfMonth { get; set; }

    public decimal? StartFromPrice { get; set; }

    public string Condition { get; set; }

    public ulong IsActive { get; set; }

    public ulong IsContract { get; set; }

    public ulong IgnoreServiceDiscount { get; set; }

    public virtual ICollection<ClientSubscriptionT> ClientSubscriptionT { get; set; } = new List<ClientSubscriptionT>();

    public virtual DepartmentT Department { get; set; }

    public virtual ICollection<RequestT> RequestT { get; set; } = new List<RequestT>();

    public virtual ICollection<SubscriptionServiceT> SubscriptionServiceT { get; set; } = new List<SubscriptionServiceT>();
}
