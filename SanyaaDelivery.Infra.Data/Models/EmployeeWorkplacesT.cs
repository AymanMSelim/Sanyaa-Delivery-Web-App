using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class EmployeeWorkplacesT
{
    public string EmployeeId { get; set; }

    public int BranchId { get; set; }

    public int EmployeeWorkplaceId { get; set; }

    public virtual BranchT Branch { get; set; }

    public virtual EmployeeT Employee { get; set; }
}
