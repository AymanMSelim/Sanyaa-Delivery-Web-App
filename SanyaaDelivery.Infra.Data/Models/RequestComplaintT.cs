using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class RequestComplaintT
{
    public int RequestId { get; set; }

    public DateTime ComplaintTimestamp { get; set; }

    public string ComplaintDes { get; set; }

    public int? NewRequestId { get; set; }

    public string ComplaintIsSolved { get; set; }

    public ulong IsSolved { get; set; }

    public int SystemUserId { get; set; }

    public virtual RequestT Request { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
