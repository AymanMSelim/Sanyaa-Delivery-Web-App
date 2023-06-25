using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class RequestCanceledT
{
    public int RequestId { get; set; }

    public DateTime CancelRequestTimestamp { get; set; }

    public string CancelRequestReason { get; set; }

    public int SystemUserId { get; set; }

    public virtual RequestT Request { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
