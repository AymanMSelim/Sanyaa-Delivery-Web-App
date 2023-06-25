using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class RequestStagesT
{
    public int RequestId { get; set; }

    public DateTime? SentTimestamp { get; set; }

    public DateTime? ReceiveTimestamp { get; set; }

    public DateTime? AcceptTimestamp { get; set; }

    public DateTime? FinishTimestamp { get; set; }

    public decimal? Cost { get; set; }

    public ulong? PaymentFlag { get; set; }

    public virtual RequestT Request { get; set; }
}
