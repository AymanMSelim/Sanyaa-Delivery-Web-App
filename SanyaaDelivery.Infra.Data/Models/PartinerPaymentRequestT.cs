using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class PartinerPaymentRequestT
{
    public int PaymentId { get; set; }

    public int RequestId { get; set; }

    public int Id { get; set; }

    public virtual PartinerPaymentT Payment { get; set; }

    public virtual RequestT Request { get; set; }
}
