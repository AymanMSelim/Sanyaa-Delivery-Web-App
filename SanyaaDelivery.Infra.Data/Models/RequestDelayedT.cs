using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class RequestDelayedT
{
    public int RequestDelayedId { get; set; }

    public int RequestId { get; set; }

    public DateTime DelayRequestTimestamp { get; set; }

    public string DelayRequestReason { get; set; }

    public DateTime DelayRequestNewTimestamp { get; set; }

    public int SystemUserId { get; set; }

    public virtual RequestT Request { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
