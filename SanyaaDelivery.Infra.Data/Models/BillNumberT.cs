using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class BillNumberT
{
    public string BillNumber { get; set; }

    public int RequestId { get; set; }

    public DateTime? BillTimestamp { get; set; }

    public int SystemUserId { get; set; }

    public virtual ICollection<BillDetailsT> BillDetailsT { get; set; } = new List<BillDetailsT>();

    public virtual RequestT Request { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
