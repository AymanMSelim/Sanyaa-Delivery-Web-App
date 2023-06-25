using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class EmployeeReviewT
{
    public int EmployeeReviewId { get; set; }

    public string EmployeeId { get; set; }

    public int ClientId { get; set; }

    public int? RequestId { get; set; }

    public int Rate { get; set; }

    public string Review { get; set; }

    public DateTime CreationTime { get; set; }

    public virtual ClientT Client { get; set; }

    public virtual EmployeeT Employee { get; set; }

    public virtual RequestT Request { get; set; }
}
