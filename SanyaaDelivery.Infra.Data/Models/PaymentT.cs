using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class PaymentT
{
    public int RequestId { get; set; }

    public DateTime PaymentTimestamp { get; set; }

    public double Payment { get; set; }

    public int SystemUserId { get; set; }

    public virtual RequestT Request { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
