using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class SiteContractT
{
    public int ContractId { get; set; }

    public string ContractDesception { get; set; }

    public int SiteId { get; set; }

    public decimal Amount { get; set; }

    public decimal PaidAmount { get; set; }

    public decimal RemainAmount { get; set; }

    public DateTime? CreationTime { get; set; }

    public int SystemUserId { get; set; }

    public DateTime? ModificationTime { get; set; }

    public int? ModificationSystemUserId { get; set; }

    public virtual SystemUserT ModificationSystemUser { get; set; }

    public virtual SiteT Site { get; set; }

    public virtual SystemUserT SystemUser { get; set; }
}
