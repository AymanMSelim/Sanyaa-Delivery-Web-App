using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class FawryChargeRequestT
{
    public int ChargeId { get; set; }

    public int RequestId { get; set; }

    public int Id { get; set; }

    public virtual FawryChargeT Charge { get; set; }

    public virtual RequestT Request { get; set; }
}
