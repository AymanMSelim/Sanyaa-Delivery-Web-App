using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class FollowUpT
{
    public int RequestId { get; set; }

    public DateTime Timestamp { get; set; }

    public float? Paid { get; set; }

    public string Prices { get; set; }

    public sbyte? Time { get; set; }

    public sbyte? Tps { get; set; }

    public string Reason { get; set; }

    public sbyte? Cleaness { get; set; }

    public sbyte? Rate { get; set; }

    public sbyte? Product { get; set; }

    public float? ProductCost { get; set; }

    public string Review { get; set; }

    public string Behavior { get; set; }

    public int? SystemUserId { get; set; }

    public virtual RequestT Request { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
