using System;
using System.Collections.Generic;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class FawryChargeT
{
    public int SystemId { get; set; }

    public long? FawryRefNumber { get; set; }

    public string ChargeStatus { get; set; }

    public decimal? ChargeAmount { get; set; }

    public DateTime? ChargeExpireDate { get; set; }

    public string EmployeeId { get; set; }

    public DateTime? RecordTimestamp { get; set; }

    public ulong? IsConfirmed { get; set; }

    public virtual EmployeeT Employee { get; set; }

    public virtual ICollection<FawryChargeRequestT> FawryChargeRequestT { get; set; } = new List<FawryChargeRequestT>();
}
