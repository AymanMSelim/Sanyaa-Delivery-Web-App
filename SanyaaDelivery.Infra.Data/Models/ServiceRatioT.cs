using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class ServiceRatioT
{
    public int ServiceRatioId { get; set; }

    public string Description { get; set; }

    public decimal? Ratio { get; set; }

    public ulong? IsActive { get; set; }

    public virtual ICollection<ServiceRatioDetailsT> ServiceRatioDetailsT { get; set; } = new List<ServiceRatioDetailsT>();
}
