using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class BroadcastRequestT
{
    public int BroadcastRequestId { get; set; }

    public int RequestId { get; set; }

    public string EmployeeId { get; set; }

    public string Status { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime? ActionTime { get; set; }

    public ulong IsListed { get; set; }

    public ulong IsSeen { get; set; }

    public virtual EmployeeT Employee { get; set; }

    public virtual RequestT Request { get; set; }
}
