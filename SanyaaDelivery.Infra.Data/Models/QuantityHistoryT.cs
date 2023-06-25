using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class QuantityHistoryT
{
    public int ProductId { get; set; }

    public short? QuantityHistory { get; set; }

    public DateTime QuantityTimestamp { get; set; }

    public string SystemUsername { get; set; }

    public virtual ProductT Product { get; set; }
}
