using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class CartDetailsT
{
    public int CartDetailsId { get; set; }

    public int CartId { get; set; }

    public int ServiceId { get; set; }

    public int ServiceQuantity { get; set; }

    public DateTime? CreationTime { get; set; }

    public virtual CartT Cart { get; set; }

    public virtual ServiceT Service { get; set; }
}
