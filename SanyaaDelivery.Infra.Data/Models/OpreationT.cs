using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class OpreationT
{
    public string EmployeeId { get; set; }

    public ulong IsActive { get; set; }

    public DateTime LastActiveTime { get; set; }

    public int? PreferredWorkingStartHour { get; set; }

    public int? PreferredWorkingEndHour { get; set; }

    public ulong OpenVacation { get; set; }

    public virtual EmployeeT Employee { get; set; }
}
