using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class SubscriptionServiceT
{
    public int SubscriptionServiceId { get; set; }

    public int SubscriptionId { get; set; }

    public int ServiceId { get; set; }

    public decimal TotalPricePerMonth { get; set; }

    public decimal Discount { get; set; }

    public string Info { get; set; }

    public virtual ICollection<ClientSubscriptionT> ClientSubscriptionT { get; set; } = new List<ClientSubscriptionT>();

    public virtual ServiceT Service { get; set; }

    public virtual SubscriptionT Subscription { get; set; }

    public virtual ICollection<SubscriptionSequenceT> SubscriptionSequenceT { get; set; } = new List<SubscriptionSequenceT>();
}
