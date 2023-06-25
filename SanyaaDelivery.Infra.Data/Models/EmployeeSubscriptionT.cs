using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class EmployeeSubscriptionT
{
    public int SubscriptionId { get; set; }

    public string Description { get; set; }

    public int? InsuranceAmount { get; set; }

    public int? MaxRequestPrice { get; set; }

    public int? MaxRequestCount { get; set; }

    public virtual ICollection<EmployeeT> EmployeeT { get; set; } = new List<EmployeeT>();
}
