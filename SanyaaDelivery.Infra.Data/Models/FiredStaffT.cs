using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class FiredStaffT
{
    public string EmployeeId { get; set; }

    public DateTime FiredDate { get; set; }

    public string FiredReasons { get; set; }

    public virtual EmployeeT Employee { get; set; }
}
