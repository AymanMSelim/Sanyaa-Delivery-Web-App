using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class RejectRequestT
{
    public int RejectRequestId { get; set; }

    public string EmployeeId { get; set; }

    public int RequestId { get; set; }

    public DateTime? RejectRequestTimestamp { get; set; }

    public virtual EmployeeT Employee { get; set; }

    public virtual RequestT Request { get; set; }
}
