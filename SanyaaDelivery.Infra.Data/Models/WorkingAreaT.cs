using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class WorkingAreaT
{
    public int WorkingAreaId { get; set; }

    public string WorkingAreaGov { get; set; }

    public string WorkingAreaCity { get; set; }

    public string WorkingAreaRegion { get; set; }

    public int BranchId { get; set; }

    public virtual BranchT Branch { get; set; }
}
