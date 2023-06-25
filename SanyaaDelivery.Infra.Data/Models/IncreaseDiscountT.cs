using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class IncreaseDiscountT
{
    public string EmployeeId { get; set; }

    public DateTime Timestamp { get; set; }

    public sbyte IncreaseDiscountType { get; set; }

    public short IncreaseDiscountValue { get; set; }

    public string IncreaseDiscountReason { get; set; }

    public int SystemUserId { get; set; }

    public virtual EmployeeT Employee { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
