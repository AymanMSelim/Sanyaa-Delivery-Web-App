using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class InsurancePaymentT
{
    public int InsurancePaymentId { get; set; }

    public string EmployeeId { get; set; }

    public int? ReferenceType { get; set; }

    public int? ReferenceId { get; set; }

    public string Description { get; set; }

    public int? Amount { get; set; }

    public DateTime? CreationTime { get; set; }

    public int? SystemUserId { get; set; }

    public virtual EmployeeT Employee { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
