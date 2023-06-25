using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class PartinerPaymentT
{
    public int Id { get; set; }

    public DateTime? RecordTimestamp { get; set; }

    public int SystemUserId { get; set; }

    public double? Amount { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public virtual ICollection<PartinerPaymentRequestT> PartinerPaymentRequestT { get; set; } = new List<PartinerPaymentRequestT>();

    public virtual SystemUserT SystemUser { get; set; }
}
